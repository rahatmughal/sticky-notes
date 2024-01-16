<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="update-note.aspx.cs" Inherits="Sticky_Notes_App.update_note" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />

    <title>Update Note</title>
    <style>
        * {
            color: white;
            border-color: white;
            font-family: sans-serif;
            padding: 0;
            margin: 0;
            background-color: transparent;
        }

        body {
            width: 100%;
            height: 100vh;
        }

        .add-new {
            min-height: 100vh;
            background-color: black;
            padding: 50px 80px;
        }

        h1 {
            font-weight: 600;
        }

        .btn {
            color: white;
        }
        /* Subject box styles */
        #txtSubject {
            width: 80%;
            margin: 20px 0px;
            padding: 10px;
            border-radius: 30px;
            border: 1px solid;
            background-color: rgb(255, 255, 255,0.1);
            color: white;
        }

        /* Description box styles */
        #txtNoteDescription {
            width: 80%;
            margin: 20px 0px;
            padding: 10px;
            border: 1px solid;
            border-radius: 30px;
            background-color: rgb(255, 255, 255,0.1);
            resize: vertical;
            color: white;
        }

        .btn {
            color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="add-new">
            <h1>Add Notes</h1>

            <!-- Subject box -->
            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" placeholder="Subject"></asp:TextBox>

            <!-- Description box -->
            <asp:TextBox ID="txtNoteDescription" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="Note"></asp:TextBox>
            <br />
            <!-- Plus button for adding a new note -->
            <asp:Button ID="btnUpdateNote" runat="server" CssClass="btn btn-warning" Text="Update Note" OnClick="btnUpdateNote_Click" />
        </div>

    </form>
</body>
</html>
