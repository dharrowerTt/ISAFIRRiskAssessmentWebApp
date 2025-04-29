Imports System.Data.SqlClient
Imports System.Configuration

Partial Class ThreatAssessment
    Inherits System.Web.UI.Page

    Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim stepKey As String = Request.QueryString("step")
            Dim assessmentId As String = Request.QueryString("assessment_id")

            If String.IsNullOrEmpty(stepKey) OrElse String.IsNullOrEmpty(assessmentId) Then
                Response.Redirect("AssessmentDashboard.aspx")
            End If

            hfAssessmentID.Value = assessmentId

            LoadAssessmentDetails(Convert.ToInt32(assessmentId))

            ' Determine whether the stepKey corresponds to a Single or Matrix question
            Dim displayMode As String = GetDisplayModeForStep(stepKey)

            If displayMode = "Matrix" Then
                mvThreatView.SetActiveView(ViewMatrix)
                LoadMatrixQuestions(Convert.ToInt32(assessmentId))
            ElseIf displayMode = "Single" Then
                mvThreatView.SetActiveView(ViewSingle)
                LoadSingleThreatQuestion(stepKey.ToLower(), Convert.ToInt32(assessmentId))
            Else
                ' Unknown step, just redirect safely
                Response.Redirect("AssessmentDashboard.aspx")
            End If
        End If
    End Sub

    ' Helper to get whether the step is Single or Matrix based on DB lookup
    Private Function GetDisplayModeForStep(stepKey As String) As String
        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("
            SELECT DisplayMode 
            FROM ThreatAssessmentQuestions
            WHERE LOWER(StepKey) = @StepKey", conn)
            cmd.Parameters.AddWithValue("@StepKey", stepKey.ToLower())

            conn.Open()
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing Then
                Return result.ToString()
            Else
                ' If no match, check if stepKey is 'internalthreats' (legacy compatibility)
                If stepKey.ToLower() = "internalthreats" Then
                    Return "Matrix"
                End If
                Return ""
            End If
        End Using
    End Function



    Private Sub LoadSingleThreatQuestion(stepKey As String, assessmentId As Integer)
        Using conn As New SqlConnection(connString)
            conn.Open()

            ' 1. Get question details based on StepKey
            Dim cmd1 As New SqlCommand("
            SELECT TOP 1 QuestionID, Heading, Content, GraphicUrl 
            FROM ThreatAssessmentQuestions
            WHERE LOWER(StepKey) = @StepKey", conn)
            cmd1.Parameters.AddWithValue("@StepKey", stepKey.ToLower())

            Dim reader = cmd1.ExecuteReader()
            If Not reader.Read() Then
                Response.Redirect("AssessmentDashboard.aspx")
                Exit Sub
            End If

            Dim questionID = Convert.ToInt32(reader("QuestionID"))
            hfSubhazardID.Value = questionID
            litSubhazard.Text = reader("Heading").ToString()

            ' Content or Graphic
            Dim contentText As String = reader("Content").ToString()
            Dim graphicUrl As String = If(reader("GraphicUrl") IsNot DBNull.Value, reader("GraphicUrl").ToString(), "")

            ' If GraphicURL exists, show it alongside text
            Dim extraHtml As String = $"<div class='mb-2 text-muted'><em>{contentText}</em></div>"
            If Not String.IsNullOrEmpty(graphicUrl) Then
                extraHtml &= $"<div class='text-center'><img src='{graphicUrl}' alt='Graphic' class='img-fluid rounded shadow' style='max-height: 300px;' /></div>"
            End If
            litExtraInfo.Text = extraHtml

            reader.Close()

            ' 2. Load previously selected rating (if any)
            Dim selectedRating As String = ""
            Dim cmd2 As New SqlCommand("
            SELECT rating FROM Threat 
            WHERE assessment_id = @AID AND subhazard_LU_id = @QID", conn)
            cmd2.Parameters.AddWithValue("@AID", assessmentId)
            cmd2.Parameters.AddWithValue("@QID", questionID)
            selectedRating = Convert.ToString(cmd2.ExecuteScalar())

            ' 3. Load options for the question
            Dim cmd3 As New SqlCommand("
            SELECT DisplayText, Value 
            FROM ThreatAssessmentOptions 
            WHERE QuestionID = @QID 
            ORDER BY SortOrder", conn)
            cmd3.Parameters.AddWithValue("@QID", questionID)

            reader = cmd3.ExecuteReader()
            rblRatings.Items.Clear()
            While reader.Read()
                rblRatings.Items.Add(New ListItem(reader("DisplayText").ToString(), reader("Value").ToString()))
            End While

            ' 4. Pre-select previously saved answer if any
            If Not String.IsNullOrEmpty(selectedRating) Then
                Dim item = rblRatings.Items.FindByValue(selectedRating)
                If item IsNot Nothing Then item.Selected = True
            End If
        End Using
    End Sub




    Private Sub LoadAssessmentDetails(assessmentId As Integer)
        Using conn As New SqlConnection(connString)
            conn.Open()

            Dim cmd As New SqlCommand("
            SELECT a.ID, a.assessor_first, a.assessor_last, a.assessor_phone, a.assessor_email,
                   a.assessment_start, f.full_name
            FROM Assessment a
            INNER JOIN Facility f ON a.facility_id = f.ID
            WHERE a.ID = @ID", conn)
            cmd.Parameters.AddWithValue("@ID", assessmentId)

            Dim reader = cmd.ExecuteReader()
            If reader.Read() Then
                litAssessmentID.Text = reader("ID").ToString()
                litFacility.Text = reader("full_name").ToString()
                litAssessor.Text = $"{reader("assessor_first")} {reader("assessor_last")}".Trim()
                litContact.Text = $"{reader("assessor_phone")} / {reader("assessor_email")}".Trim(" /".ToCharArray())
                litStartDate.Text = If(IsDBNull(reader("assessment_start")), "", Convert.ToDateTime(reader("assessment_start")).ToString("g"))
            End If
        End Using
    End Sub


    Private Sub LoadMatrixQuestions(assessmentId As Integer)
        Dim dt As New DataTable()
        dt.Columns.Add("QuestionID", GetType(Integer))
        dt.Columns.Add("Heading", GetType(String))
        dt.Columns.Add("Content", GetType(String))
        dt.Columns.Add("HelpText", GetType(String))
        dt.Columns.Add("SelectedValue", GetType(String))

        Using conn As New SqlConnection(connString)
            conn.Open()

            ' Load all matrix-style questions
            Dim cmd As New SqlCommand("
            SELECT QuestionID, Heading, Content, HelpTooltip
            FROM ThreatAssessmentQuestions
            WHERE DisplayMode = 'Matrix'
            ORDER BY QuestionID", conn)

            Using reader = cmd.ExecuteReader()
                While reader.Read()
                    Dim row = dt.NewRow()
                    row("QuestionID") = reader("QuestionID")
                    row("Heading") = reader("Heading").ToString()
                    row("Content") = reader("Content").ToString()
                    row("HelpText") = reader("HelpTooltip").ToString()
                    row("SelectedValue") = "" ' Default empty, will populate later
                    dt.Rows.Add(row)
                End While
            End Using

            ' Load saved ratings (answers)
            For Each row As DataRow In dt.Rows
                Dim cmd2 As New SqlCommand("
                SELECT rating 
                FROM Threat 
                WHERE assessment_id = @AID AND subhazard_LU_id = @QID", conn)

                cmd2.Parameters.AddWithValue("@AID", assessmentId)
                cmd2.Parameters.AddWithValue("@QID", row("QuestionID"))

                Dim savedRating = cmd2.ExecuteScalar()
                If savedRating IsNot Nothing Then
                    row("SelectedValue") = savedRating.ToString()
                End If
            Next

            ' Bind to Repeater
            rptMatrix.DataSource = dt
            rptMatrix.DataBind()
        End Using
    End Sub



    Protected Sub btnNext_Click(sender As Object, e As EventArgs)
        If String.IsNullOrEmpty(rblRatings.SelectedValue) Then Exit Sub

        Using conn As New SqlConnection(connString)
            conn.Open()

            Dim cmd As New SqlCommand("
            MERGE Threat AS target
            USING (SELECT @AID AS assessment_id, @SID AS subhazard_LU_id) AS source
            ON target.assessment_id = source.assessment_id AND target.subhazard_LU_id = source.subhazard_LU_id
            WHEN MATCHED THEN
                UPDATE SET rating = @Rating, Answer = 1
            WHEN NOT MATCHED THEN
                INSERT (assessment_id, subhazard_LU_id, rating, Answer)
                VALUES (@AID, @SID, @Rating, 1);", conn)

            cmd.Parameters.AddWithValue("@AID", Convert.ToInt32(hfAssessmentID.Value))
            cmd.Parameters.AddWithValue("@SID", Convert.ToInt32(hfSubhazardID.Value))
            cmd.Parameters.AddWithValue("@Rating", rblRatings.SelectedValue)

            cmd.ExecuteNonQuery()
        End Using

        ' Navigate to the next step
        Dim currentStep As String = Request.QueryString("step")?.ToLower()
        Dim nextStep As String = GetNextStep(currentStep)

        If String.IsNullOrEmpty(nextStep) Then
            Response.Redirect("AssessmentOverview.aspx?id=" & hfAssessmentID.Value)
        Else
            Response.Redirect($"ThreatAssessment.aspx?assessment_id={hfAssessmentID.Value}&step={nextStep}")
        End If
    End Sub


    Private Function GetNextStep(currentStep As String) As String
        Dim steps As New List(Of String)()

        Using conn As New SqlConnection(connString)
            conn.Open()

            ' Get ordered list of StepKeys for single questions
            Dim cmd As New SqlCommand("
            SELECT StepKey 
            FROM ThreatAssessmentQuestions
            WHERE DisplayMode = 'Single' 
              AND StepKey IS NOT NULL 
            ORDER BY SortOrder, QuestionID;", conn)

            Dim reader = cmd.ExecuteReader()
            While reader.Read()
                steps.Add(reader("StepKey").ToString().ToLower())
            End While
        End Using

        ' Find the index of the current step
        Dim index = steps.IndexOf(currentStep.ToLower())

        ' Return the next step
        If index >= 0 AndAlso index + 1 < steps.Count Then
            Return steps(index + 1)
        ElseIf index = steps.Count - 1 Then
            ' After all singles, move to internal threats matrix
            Return "internalthreats"
        Else
            ' Invalid or not found
            Return Nothing
        End If
    End Function


    Protected Sub btnMatrixSubmit_Click(sender As Object, e As EventArgs)
        Dim assessmentId As Integer = Convert.ToInt32(hfAssessmentID.Value)

        Using conn As New SqlConnection(connString)
            conn.Open()

            For Each item As RepeaterItem In rptMatrix.Items
                Dim hfQuestionID As HiddenField = TryCast(item.FindControl("hfQuestionID"), HiddenField)
                Dim rblOptions As RadioButtonList = TryCast(item.FindControl("rblMatrixOptions"), RadioButtonList)

                If hfQuestionID IsNot Nothing AndAlso rblOptions IsNot Nothing AndAlso Not String.IsNullOrEmpty(rblOptions.SelectedValue) Then
                    Dim cmd As New SqlCommand("
                    MERGE Threat AS target
                    USING (SELECT @AID AS assessment_id, @QID AS subhazard_LU_id) AS source
                    ON target.assessment_id = source.assessment_id AND target.subhazard_LU_id = source.subhazard_LU_id
                    WHEN MATCHED THEN
                        UPDATE SET rating = @Rating, Answer = 1
                    WHEN NOT MATCHED THEN
                        INSERT (assessment_id, subhazard_LU_id, rating, Answer)
                        VALUES (@AID, @QID, @Rating, 1);", conn)

                    cmd.Parameters.AddWithValue("@AID", assessmentId)
                    cmd.Parameters.AddWithValue("@QID", Convert.ToInt32(hfQuestionID.Value))
                    cmd.Parameters.AddWithValue("@Rating", rblOptions.SelectedValue)

                    cmd.ExecuteNonQuery()
                End If
            Next
        End Using

        ' ✅ After matrix is done, go cleanly to the overview page
        Response.Redirect($"AssessmentOverview.aspx?id={assessmentId}")
    End Sub


    Protected Sub rptMatrix_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptMatrix.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim hfQuestionID As HiddenField = TryCast(e.Item.FindControl("hfQuestionID"), HiddenField)
            Dim rbl As RadioButtonList = TryCast(e.Item.FindControl("rblMatrixOptions"), RadioButtonList)

            If hfQuestionID IsNot Nothing AndAlso rbl IsNot Nothing Then
                Dim qid As Integer = Convert.ToInt32(hfQuestionID.Value)

                Using conn As New SqlConnection(connString)
                    conn.Open()

                    Dim cmd As New SqlCommand("
                    SELECT DisplayText, Value 
                    FROM ThreatAssessmentOptions 
                    WHERE QuestionID = @QID 
                    ORDER BY SortOrder;", conn)
                    cmd.Parameters.AddWithValue("@QID", qid)

                    Using reader = cmd.ExecuteReader()
                        rbl.Items.Clear()
                        While reader.Read()
                            rbl.Items.Add(New ListItem(reader("DisplayText").ToString(), reader("Value").ToString()))
                        End While
                    End Using
                End Using

                ' Preselect saved value if one exists
                Dim selectedValue As String = DataBinder.Eval(e.Item.DataItem, "SelectedValue")?.ToString()
                If Not String.IsNullOrEmpty(selectedValue) Then
                    Dim selectedItem = rbl.Items.FindByValue(selectedValue)
                    If selectedItem IsNot Nothing Then selectedItem.Selected = True
                End If
            End If
        End If
    End Sub



End Class
