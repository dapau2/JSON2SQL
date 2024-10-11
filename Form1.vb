Imports System.Data.SqlClient
Imports Newtonsoft.Json
Imports System.IO

Public Class Form1


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        ' Ange sökvägen till din JSON-fil
        Dim jsonFilePath As String = "C:\Temp\TRAIN2DII\riktig.json"

        ' Läs in JSON-filen
        Dim jsonData As String = File.ReadAllText(jsonFilePath)

        ' Deserialisera JSON till en DataTable
        Dim dataTable As DataTable = JsonConvert.DeserializeObject(Of DataTable)(jsonData, )

        ' Ange din SQL Server-anslutningssträng
        Dim connectionString As String = "Server=HPTSQATDB05\INSTANCE_01;Database=JSON2DB;User Id=sa;Password=Hogia6969;"

        ' Anslut till SQL Server och importera data
        Using connection As New SqlConnection(connectionString)
            connection.Open()

            For Each row As DataRow In dataTable.Rows
                Dim query As String = "INSERT INTO YourTable (id, name, age) VALUES (@id, @name, @age)"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@id", row("id"))
                    command.Parameters.AddWithValue("@name", row("name"))
                    command.Parameters.AddWithValue("@age", row("age"))
                    command.ExecuteNonQuery()
                End Using
            Next
        End Using

        Console.WriteLine("Data har importerats framgångsrikt!")
    End Sub

End Class
