<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GesTienda.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="./Estilos/Default.css" rel="stylesheet" />
    <title>Login GesTienda</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="topMsg">
            <p>comerciodaw.com</p>
        </div>
        <h2>TIENDA ONLINE - SHOPPING DAW</h2>
        <div id="main">
            <h1>GesTienda</h1>
        </div>
        <div id="borde">
            <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate">
                <LayoutTemplate>
                    <div class="filaCampo">
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nombre de usuario:</asp:Label>
                        <asp:TextBox ID="UserName" CssClass="tbox" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="El nombre de usuario es obligatorio." ToolTip="El nombre de usuario es obligatorio." ValidationGroup="Login1">*</asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="filaCampo">
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Contraseña:</asp:Label>
                        <asp:TextBox ID="Password" CssClass="tbox" runat="server" TextMode="Password"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria." ValidationGroup="Login1">*</asp:RequiredFieldValidator>--%>
                    </div>

                    <asp:Button ID="LoginButton" CssClass="center" runat="server" CommandName="Login" Text="Inicio de sesión" ValidationGroup="Login1" />
                    <div id="checkb">
                        <asp:CheckBox ID="RememberMe" runat="server" Text="" />
                        <asp:Label ID="lblrem" runat="server" AssociatedControlID="RememberMe" Text="Recordar Credenciales"></asp:Label>
                    </div>
                    <div id="avisos">
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="El nombre de usuario es obligatorio." ToolTip="El nombre de usuario es obligatorio." ValidationGroup="Login1">Se requiere nombre de usuario</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria." ValidationGroup="Login1">Se requiere contraseña</asp:RequiredFieldValidator>
                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                    </div>
                </LayoutTemplate>
            </asp:Login>
            <asp:Label ID="lblMensajes" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
