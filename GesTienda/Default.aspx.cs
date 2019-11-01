using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace GesTienda
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string StrCadenaConexion =
                ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string StrComandoSql = "SELECT Login, Rol FROM USUARIO ";
            StrComandoSql = StrComandoSql + " WHERE Login='" + Login1.UserName + "' ";
            StrComandoSql = StrComandoSql + "AND Password='" + Login1.Password + "';";
            using (SqlConnection conexion = new SqlConnection(StrCadenaConexion))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = conexion.CreateCommand();
                    comando.CommandText = StrComandoSql;
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        Session.Add("Nombre", reader.GetString(0));
                        Session.Add("Rol", reader.GetString(1));
                        e.Authenticated = true;
                        if (Convert.ToString(Session["Rol"]) == "A")
                            Response.Redirect("~/InicioAdmin.aspx");
                        if (Convert.ToString(Session["Rol"]) == "U")
                            Response.Redirect("~/InicioUsuario.aspx");
                    }
                    else
                    {
                        e.Authenticated = false;
                    }
                }
                catch (SqlException ex)
                {
                    string StrError = "<p>Se han producido errores en el acceso a la base de datos.</p>";
                    StrError = StrError + "<div>Código: " + ex.Number + "</div>";
                    StrError = StrError + "<div>Descripción: " + ex.Message + "</div>";
                    lblMensajes.Text = StrError;
                    return;
                }
            }
        }
    }
}