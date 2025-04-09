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
            LoadThreatQuestion(stepKey.ToLower(), Convert.ToInt32(assessmentId))
        End If
    End Sub

    Private Sub LoadAssessmentDetails(assessmentId As Integer)
        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("
                SELECT a.ID, a.assessor, a.assessor_first, a.assessor_last, a.assessor_phone, a.assessor_email,
                       a.assessment_start, f.full_name
                FROM Assessment a
                INNER JOIN Facility f ON a.facility_id = f.ID
                WHERE a.ID = @ID", conn)
            cmd.Parameters.AddWithValue("@ID", assessmentId)

            conn.Open()
            Dim reader = cmd.ExecuteReader()
            If reader.Read() Then
                litAssessmentID.Text = reader("ID").ToString()
                litFacility.Text = reader("full_name").ToString()
                litAssessor.Text = $"{reader("assessor_first")} {reader("assessor_last")}"
                litContact.Text = $"{reader("assessor_phone")} / {reader("assessor_email")}"
                litStartDate.Text = Convert.ToDateTime(reader("assessment_start")).ToString("g")
            End If
        End Using
    End Sub

    Private Sub LoadThreatQuestion(stepKey As String, assessmentId As Integer)
        Dim subhazardId As Integer = -1

        Using conn As New SqlConnection(connString)
            conn.Open()

            ' Step 1: Get Subhazard ID and name from keyword
            Dim cmd1 As New SqlCommand("
            SELECT TOP 1 Subhazard_ID, Subhazard 
            FROM threat_haz_rating_description 
            WHERE LOWER(Subhazard) = @Name", conn)
            cmd1.Parameters.AddWithValue("@Name", stepKey)

            Using reader = cmd1.ExecuteReader()
                If reader.Read() Then
                    subhazardId = Convert.ToInt32(reader("Subhazard_ID"))
                    hfSubhazardID.Value = subhazardId
                    litSubhazard.Text = reader("Subhazard").ToString()
                Else
                    Response.Redirect("AssessmentDashboard.aspx")
                    Exit Sub
                End If
            End Using

            ' Step 2: Load Level 4 Description
            Dim cmd2 As New SqlCommand("
            SELECT Description 
            FROM threat_haz_rating_description 
            WHERE Threat_Rating = 4 AND Subhazard_ID = @ID", conn)
            cmd2.Parameters.AddWithValue("@ID", subhazardId)
            Dim descResult = cmd2.ExecuteScalar()
            If descResult IsNot Nothing Then
                litExtraInfo.Text = $"<div class='mb-2 text-muted'><em>{descResult.ToString()}</em></div>"
            End If

            ' Step 3: Load existing answer if any
            Dim selectedRating As String = ""
            Dim cmd3 As New SqlCommand("
            SELECT rating FROM Threat 
            WHERE assessment_id = @AID AND subhazard_LU_id = @SID", conn)
            cmd3.Parameters.AddWithValue("@AID", assessmentId)
            cmd3.Parameters.AddWithValue("@SID", subhazardId)
            Dim ratingObj = cmd3.ExecuteScalar()
            If ratingObj IsNot Nothing Then
                selectedRating = ratingObj.ToString()
            End If

            ' Step 4: Populate radio options
            rblRatings.Items.Clear()

            If stepKey.ToLower() = "terrorism" Then
                rblRatings.Items.Add(New ListItem("MSA Threat Level 1 OR State/Territory Threat Level 1", "1"))
                rblRatings.Items.Add(New ListItem("MSA Threat Level 2 OR State/Territory Threat Level 2", "2"))
                rblRatings.Items.Add(New ListItem("MSA Threat Level 3 OR State/Territory Threat Level 3", "3"))
                rblRatings.Items.Add(New ListItem("MSA Threat Level 4 OR State/Territory Threat Level N/A", "4"))

            Else
                Dim cmd4 As New SqlCommand("
                SELECT Threat_Rating, Rating_Category 
                FROM threat_haz_rating_lu 
                ORDER BY Threat_Rating", conn)
                Dim reader4 = cmd4.ExecuteReader()

                While reader4.Read()
                    Dim value = reader4("Threat_Rating").ToString()
                    Dim label = reader4("Rating_Category").ToString()
                    rblRatings.Items.Add(New ListItem(label, value))
                End While
            End If

            ' Pre-select if value already stored
            If Not String.IsNullOrEmpty(selectedRating) Then
                Dim item = rblRatings.Items.FindByValue(selectedRating)
                If item IsNot Nothing Then item.Selected = True
            End If
        End Using
    End Sub


    Protected Sub btnNext_Click(sender As Object, e As EventArgs)
        If rblRatings.SelectedValue <> "" Then
            Using conn As New SqlConnection(connString)
                Dim cmd As New SqlCommand("
                    MERGE Threat AS target
                    USING (SELECT @AID AS assessment_id, @SID AS subhazard_LU_id) AS source
                    ON target.assessment_id = source.assessment_id AND target.subhazard_LU_id = source.subhazard_LU_id
                    WHEN MATCHED THEN
                        UPDATE SET rating = @Rating, Answer = 1
                    WHEN NOT MATCHED THEN
                        INSERT (assessment_id, subhazard_LU_id, rating, Answer)
                        VALUES (@AID, @SID, @Rating, 1);", conn)

                cmd.Parameters.AddWithValue("@AID", hfAssessmentID.Value)
                cmd.Parameters.AddWithValue("@SID", hfSubhazardID.Value)
                cmd.Parameters.AddWithValue("@Rating", rblRatings.SelectedValue)

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using

            ' Redirect to overview or next question
            Response.Redirect("AssessmentOverview.aspx?id=" & hfAssessmentID.Value)
        End If
    End Sub
End Class
