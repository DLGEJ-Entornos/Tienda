<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GesTienda.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="./Estilos/Default.css" rel="stylesheet" />
    <title>Login GesTienda</title>
    <!--style>
        #topMsg{
            height:2em;
            text-align:right;
            
            background-color:darkblue;
            color: white;
        }
        #topMsg p{
            /*Alinear verticalmente abajo*/
            /*vertical-align:text-bottom;*/
        }
        h2 {
            text-align: left;
            color: white;
            margin:0;
            padding:1em;
            background-color:cadetblue;
        }
        #main{
            text-align:center;
            /*margin: 0auto;*/
        }
        #Login1{
            margin:auto;
            width:30%;
            text-align:center;
            /*border: 1px solid black;*/
        }
        #tit{
            margin-bottom:0.5em;
        }
    </style-->
</head>
<body>
    <form id="form1" runat="server">
        <div id="topMsg">
            <p>comerciodaw.com</p>
        </div>
        <h2>TIENDA ONLINE - SHOPPING DAW</h2>
        <div id="main">
            <h1>GesTienda</h1>
                <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate">
                    <LayoutTemplate>
                        <table style="border-collapse:collapse;">
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td id="tit" colspan="2">Iniciar sesión</td>
                                        </tr>
                                        <tr>
                                            <td style="height:10px" colspan="2"> </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nombre de usuario:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="El nombre de usuario es obligatorio." ToolTip="El nombre de usuario es obligatorio." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Contraseña:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBox ID="RememberMe" runat="server" Text="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="color:Red;">
                                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Inicio de sesión" ValidationGroup="Login1" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:Login>
            <asp:Label ID="lblMensajes" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
