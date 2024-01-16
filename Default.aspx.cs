using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace YourNamespace
{
    public partial class _Default : System.Web.UI.Page
    {
        private static string connectionString = "Server=localhost;Database=sticky-notes;Uid=root;Pwd=;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStickyNotes();
            }
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            // Redirect to the "/add-new" page
            Response.Redirect("add-note.aspx");
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            LoadStickyNotes();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditNote" && e.CommandArgument != null)
            {
                int noteId;
                if (int.TryParse(e.CommandArgument.ToString(), out noteId))
                {
                    // Redirect to the dynamic update page with the note ID
                    Response.Redirect($"update-note/{noteId}");
                }
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            LoadStickyNotes();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int noteId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            string updatedSubject = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtEditSubject")).Text;

            UpdateStickyNote(noteId, updatedSubject);

            GridView1.EditIndex = -1;
            LoadStickyNotes();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int noteId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

            DeleteStickyNote(noteId);

            LoadStickyNotes();
        }

        private void UpdateStickyNote(int noteId, string updatedSubject)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("UPDATE notes SET subjects = @Subject WHERE id = @NoteId", connection))
                {
                    cmd.Parameters.AddWithValue("@Subject", updatedSubject);
                    cmd.Parameters.AddWithValue("@NoteId", noteId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DeleteStickyNote(int noteId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM notes WHERE id = @NoteId", connection))
                {
                    cmd.Parameters.AddWithValue("@NoteId", noteId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void LoadStickyNotes()
        {
            GridView1.DataSource = GetStickyNotesFromDatabase();
            GridView1.DataBind();
        }

        private List<StickyNote> GetStickyNotesFromDatabase()
        {
            List<StickyNote> stickyNotes = new List<StickyNote>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("SELECT id, subjects, note FROM notes", connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StickyNote stickyNote = new StickyNote
                            {
                                NoteId = Convert.ToInt32(reader["id"]),
                                Subject = reader["subjects"].ToString(),
                                Note = reader["note"].ToString()
                            };

                            stickyNotes.Add(stickyNote);
                        }
                    }
                }
            }

            return stickyNotes;
        }
    }

    public class StickyNote
    {
        public int NoteId { get; set; }
        public string Subject { get; set; }
        public string Note { get; set; }
    }
}
