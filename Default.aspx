<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="YourNamespace._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sticky Notes App</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    <style>
        *{
            color: white;
            border-color: white;
        }
        .sticky-notes-list{
             padding: 50px;
             width: 100%;
             min-height:100vh;
             background-color: black;
        }
        .btn{
            color: white;
        }
        a{
            color: yellow;
            text-decoration: none;
        }
        a:hover{
            color: lightyellow;
        }
        /* Flex container styles */
        .flex-container {
            width: 100%;
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            align-items: stretch;
            margin-bottom: 20px;
        }

        .styled-template-field {
            width: 100%;
            border: none;
            border-radius: 20px;
            margin: 20px 0px;
        }

        /* Note card styles */
        .note-card {
            display: flex;
            justify-content: center;
            flex-direction: column;
            gap: 20px;
            border: 1px solid #ddd;
            border-radius: 10px;
            width: 900px;
            height: 200px;
            padding: 10px;
            margin-bottom: 10px;
            background-color: rgb(255, 255, 255,0.1);
        }

        /* Edit and Delete button styles */
        .edit-delete-buttons {
            align-self: end;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="sticky-notes-list">
            <div class="flex-container">
                <h2>Sticky Notes App</h2>
                <asp:Button ID="btnAddItem" runat="server" CssClass="btn btn-warning" Text="Add Item" OnClick="btnAddItem_Click" />
            </div>
            <!-- GridView to display sticky notes -->
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="NoteId"
    OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"
    OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting" ShowHeader="False" RepeatColumns="2" RepeatDirection="Horizontal" Border="0">
                <Columns>
                    <asp:TemplateField ItemStyle-CssClass="styled-template-field">
                        <ItemTemplate>
                            <div class="note-card">
                                <h4><%# Eval("Subject") %></h4>
                                <p><%# Eval("Note") %></p>
                                <!-- Edit and Delete buttons -->
                                <div class="edit-delete-buttons">
                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"
                                        PostBackUrl='<%# "/update-note.aspx?id=" + Eval("NoteId") %>'></asp:LinkButton>
                                    &nbsp;|&nbsp; 
                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
