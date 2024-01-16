using System;
using MySql.Data.MySqlClient;

namespace Sticky_Notes_App
{
    public partial class update_note : System.Web.UI.Page
    {
        private static string connectionString = "Server=localhost;Database=sticky-notes;Uid=root;Pwd=;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the ID parameter is present in the query string
                if (Request.QueryString["id"] != null)
                {
                    int noteId;
                    if (int.TryParse(Request.QueryString["id"], out noteId))
                    {
                        // Load the existing note data for the given ID
                        LoadNoteData(noteId);
                    }
                    else
                    {
                        // Handle invalid ID parameter
                        Response.Redirect("Default.aspx");
                    }
                }
                else
                {
                    // Handle missing ID parameter
                    Response.Redirect("Default.aspx");
                }
            }
        }

        public void btnUpdateNote_Click(object sender, EventArgs e)
        {
            // Get the note ID from the query string
            if (Request.QueryString["id"] != null)
            {
                int noteId;
                if (int.TryParse(Request.QueryString["id"], out noteId))
                {
                    // Get the updated data from the form
                    string updatedSubject = txtSubject.Text;
                    string updatedNoteDescription = txtNoteDescription.Text;

                    // Update the note in the database
                    UpdateStickyNote(noteId, updatedSubject, updatedNoteDescription);

                    // Redirect back to the home page after updating the note
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    // Handle invalid ID parameter
                    Response.Redirect("Default.aspx");
                }
            }
            else
            {
                // Handle missing ID parameter
                Response.Redirect("Default.aspx");
            }
        }

        private void LoadNoteData(int noteId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("SELECT subjects, note FROM notes WHERE id = @NoteId", connection))
                {
                    cmd.Parameters.AddWithValue("@NoteId", noteId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set the data in the form controls
                            txtSubject.Text = reader["subjects"].ToString();
                            txtNoteDescription.Text = reader["note"].ToString();
                        }
                        else
                        {
                            // Handle non-existent note ID
                            Response.Redirect("Default.aspx");
                        }
                    }
                }
            }
        }

        private void UpdateStickyNote(int noteId, string updatedSubject, string updatedNoteDescription)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("UPDATE notes SET subjects = @Subject, note = @Note WHERE id = @NoteId", connection))
                {
                    cmd.Parameters.AddWithValue("@Subject", updatedSubject);
                    cmd.Parameters.AddWithValue("@Note", updatedNoteDescription);
                    cmd.Parameters.AddWithValue("@NoteId", noteId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Successful update
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        // Update failed
                        Response.Write("Update operation failed.");
                    }
                }
            }
        }
    }
}
