Imports System.Data.SqlClient
Imports System.Configuration

Partial Class ThreatHazardForm
    Inherits System.Web.UI.Page

    Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString
    Private assessmentID As Integer = 1 ' Replace with session or querystring logic

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadHazards()
        End If
    End Sub

    Private Sub LoadHazards()
        Dim dt As New DataTable()
        Using conn As New SqlConnection(connString)
            Dim sql As String = "
                SELECT s.ID AS Subhazard_ID, s.Subhazard, s.Description,
                       t.rating AS Rating, t.Answer
                FROM Subhazard_LU s
                LEFT JOIN Threat t
                    ON s.ID = t.subhazard_LU_id AND t.assessment_id = @AssessmentID
                ORDER BY s.Subhazard"

            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@AssessmentID", assessmentID)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        gvHazards.DataSource = dt
        gvHazards.DataBind()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Using conn As New SqlConnection(connString)
            conn.Open()
            For Each row As GridViewRow In gvHazards.Rows
                Dim subhazardID As Integer = Convert.ToInt32(gvHazards.DataKeys(row.RowIndex).Value)
                Dim txtRating As TextBox = CType(row.FindControl("txtRating"), TextBox)
                Dim chkApplicable As CheckBox = CType(row.FindControl("chkApplicable"), CheckBox)

                Dim ratingVal As Decimal = 0
                Decimal.TryParse(txtRating.Text, ratingVal)
                Dim answerVal As Integer = If(chkApplicable.Checked, 1, 0)

                ' UPSERT logic: if exists, update; else insert
                Dim sql As String = "
                    MERGE Threat AS target
                    USING (SELECT @AssessmentID AS assessment_id, @SubhazardID AS subhazard_LU_id) AS source
                    ON target.assessment_id = source.assessment_id AND target.subhazard_LU_id = source.subhazard_LU_id
                    WHEN MATCHED THEN
                        UPDATE SET rating = @Rating, Answer = @Answer
                    WHEN NOT MATCHED THEN
                        INSERT (assessment_id, subhazard_LU_id, rating, Answer)
                        VALUES (@AssessmentID, @SubhazardID, @Rating, @Answer);"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@AssessmentID", assessmentID)
                    cmd.Parameters.AddWithValue("@SubhazardID", subhazardID)
                    cmd.Parameters.AddWithValue("@Rating", ratingVal)
                    cmd.Parameters.AddWithValue("@Answer", answerVal)
                    cmd.ExecuteNonQuery()
                End Using
            Next
        End Using

        ' Redirect or show confirmation
        Response.Redirect("AssessmentComplete.aspx")
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("Dashboard.aspx")
    End Sub

    Protected Sub gvHazards_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvHazards.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            gvHazards.DataKeyNames = New String() {"Subhazard_ID"}
        End If
    End Sub
End Class