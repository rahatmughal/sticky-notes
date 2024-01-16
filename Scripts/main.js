// your-script.js
$(document).ready(function () {
    // Load notes when the page is ready
    loadNotes();
});

function loadNotes() {
    // Call the C# function to load notes
    PageMethods.BindNotes(onBindNotesSuccess);
}

function onBindNotesSuccess(notes) {
    // Update the UI with the refreshed notesContainer
    var notesList = $('#notesList');
    var emptyNotesMessage = $('#emptyNotesMessage');

    // Clear existing notes
    notesList.empty();

    if (notes && notes.length > 0) {
        // If there are notes, display them
        emptyNotesMessage.hide();

        // Iterate through notes and append them to the list
        for (var i = 0; i < notes.length; i++) {
            var noteItem = $('<div class="note-item">' + notes[i].Description + '</div>');
            notesList.append(noteItem);
        }
    } else {
        // If there are no notes, display the empty notes message
        emptyNotesMessage.show();
    }
}

function addNote() {
    // Get the note description from the textarea
    var noteDescription = $('#txtNoteDescription').val();

    // Call the C# function to save the note
    PageMethods.SaveNote(noteDescription, onSaveNoteSuccess);
}

function onSaveNoteSuccess() {
    // Refresh the notes list after saving the note
    loadNotes();
    // Clear the textarea after saving
    $('#txtNoteDescription').val('');
}
