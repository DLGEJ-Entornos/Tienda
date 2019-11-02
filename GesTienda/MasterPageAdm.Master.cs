using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Prueba
{
    public partial class MasterPageAdm : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Rol"]) != "A")
            {
                Response.Redirect("~/Default.aspx");
            }
            lblDatosUsuario.Text = Convert.ToString(Session["Nombre"]);
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx", false);
        }

    }
}