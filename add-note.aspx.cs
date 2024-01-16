using System;
using MySql.Data.MySqlClient;

namespace YourNamespace
{
    public partial class AddNote : System.Web.UI.Page
    {
        private static string connectionString = "Server=localhost;Database=sticky-notes;Uid=root;Pwd=;";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtNoteDescription.Rows = 5;
            txtNoteDescription.Columns = 20;

            // Your code for initializing the "/add-new" page, if needed
        }

        protected void btnAddNote_Click(object sender, EventArgs e)
        {
            string subject = txtSubject.Text;
            string noteDescription = txtNoteDescription.Text;

            if (!string.IsNullOrWhiteSpace(subject) && !string.IsNullOrWhiteSpace(noteDescription))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO notes (subjects, note) VALUES (@Subject, @Note)", connection))
                    {
                        cmd.Parameters.AddWithValue("@Subject", subject);
                        cmd.Parameters.AddWithValue("@Note", noteDescription);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Redirect back to the home page after adding a new note
                Response.Redirect("Default.aspx");
            }
        }
    }
}
