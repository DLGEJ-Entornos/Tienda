<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlError.aspx.cs" Inherits="GesTienda.ControlError" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>errores de ejecución</title>
    <style>
        body{
            
        }
        div{
            text-align: center;

        }
        #outputs{
            width:50%;
            margin: 0 auto;
            border: 1px solid black;
        }
        #outputs .lbl{
            color:red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Aplicación Web GesTienda</h1>
            <h3>Error en tiempo de ejecución</h3>
        </div>
        <div id="outputs">
            <h4>Error ASP.NET</h4>
            <asp:Label ID="lblErrorASP" CssClass="lbl" runat="server" Text="lblErrorASP"></asp:Label>
            <h4>Error ADO.NET</h4>
            <asp:Label ID="lblErrorADO" CssClass="lbl" runat="server" Text="lblErrorADO"></asp:Label>
        </div>
    </form>
</body>
</html>
