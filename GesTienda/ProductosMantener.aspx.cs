using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

/*TODO:
    1- Al crear NUEVO producto, no tiene sentido que si cambias el TIPO de producto de la selectedList no se modifique la unidad y
        siga con la unidad para Alimentación.
    2- Al Cancelar durante Nuevo o Modificación no se "reinicie" la selección , que vuelva a al ultimo elemento seleccionado de la 
        GridView (con variables de sesión).
    3- Al eliminar productos que son apuntados por una FK desde la tabla DETALLES da error de eliminación; o no dejar eliminar o
        hacer una eliminación primero del registro de la FK.
    4- La parte común de Conexión y sentecia SQL hacerla en Func pasandole la cadena de conexión y string para resultado positivo.
    5- VALIDA campos antes de Insertar/Modificar
     */
namespace GesTienda
{
    public partial class ProductosMantener : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void FnDeshabilitarControles()
        {
            txtIdProducto.Enabled = false;
            txtDesPro.Enabled = false;
            txtPrePro.Enabled = false;
            ddlIdUnidad.Enabled = false;
            ddlIdTipo.Enabled = false;
        }
        protected void FnHabilitarControles()
        {
            txtIdProducto.Enabled = true;
            txtDesPro.Enabled = true;
            txtPrePro.Enabled = true;
            ddlIdUnidad.Enabled = true;
            ddlIdTipo.Enabled = true;
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            
        }

        protected void grdProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMensajes.Text = "";
            FnDeshabilitarControles();
            string StrIdProducto = grdProductos.SelectedRow.Cells[1].Text;
            string StrCadenaConexion =
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string StrComandoSql = "SELECT IdProducto,DesPro,PrePro,IdUnidad,PRODUCTO.IdTipo,DesTip " +
            " FROM PRODUCTO INNER JOIN TIPO ON PRODUCTO.IdTipo = TIPO.IdTipo " +
            "WHERE PRODUCTO.IdProducto = '" + StrIdProducto + "';";
            using (SqlConnection conexion = new SqlConnection(StrCadenaConexion))
            {
                
                try
                {
                    conexion.Open();
                    SqlCommand comando = conexion.CreateCommand();
                    comando.CommandText = StrComandoSql;
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        txtIdProducto.Text = reader.GetString(0);
                        txtDesPro.Text = reader.GetString(1);
                        txtPrePro.Text = string.Format("{0:C}", reader.GetDecimal(2));
                        ddlIdUnidad.SelectedItem.Selected = false;
                        ddlIdUnidad.SelectedItem.Text = reader.GetString(3);
                        ddlIdTipo.SelectedItem.Selected = false;
                        ddlIdTipo.SelectedItem.Text = reader.GetString(5);
                    }
                    else
                    {
                        lblMensajes.Text = "No existen registros resultantes de la consulta";
                    }
                    reader.Close();
                    btnNuevo.Visible = true;
                    btnEditar.Visible = true;
                    btnEliminar.Visible = true;
                    btnInsertar.Visible = false;
                    btnModificar.Visible = false;
                    btnBorrar.Visible = false;
                    btnCancelar.Visible = false;
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
        protected void btnNuevo_Click1(object sender, EventArgs e)
        {
            lblMensajes.Text = "";
            btnNuevo.Visible = false;
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
            btnInsertar.Visible = true;
            btnModificar.Visible = false;
            btnBorrar.Visible = false;
            btnCancelar.Visible = true;
            txtIdProducto.Text = "";
            txtDesPro.Text = "";
            txtPrePro.Text = Convert.ToString(0);
            ddlIdUnidad.DataBind();
            ddlIdTipo.DataBind();
            grdProductos.SelectedIndex = -1;
            FnHabilitarControles();
            txtIdProducto.Focus();
        }

        protected string FnComaPorPunto(decimal Numero)
        {
            string StrNumero = Convert.ToString(Numero);
            string stNumeroConPunto = String.Format("{0}", StrNumero.Replace(',', '.'));
            return (stNumeroConPunto);
        }

        private bool valEspecifPre(string precio)
        {
            bool valido = false;
            //Valida que sea decimal o int positivo teniendo en cuenta , por . y €
            if (!string.IsNullOrEmpty(precio) && !Char.IsSymbol(precio[0]) && !Char.IsSymbol(precio[precio.Length - 1]))
            {
                //precio solo puede tener digitos y 1 punto o coma
                int signoDecimal = 0;
                for (int i = 0; i < precio.Length && signoDecimal <= 1; i++)
                {
                    if (precio[i] == '.' || precio[i] == ',')
                    {
                        signoDecimal++;
                    }
                }
                if (signoDecimal <= 1)
                {
                    string precioSinSym = precio.Trim(new Char[] { ',', ' ', '.' }); //le meto aqui TRIM € ??
                    bool soloDigitosPos = true;
                    for (int i = 0; i < precioSinSym.Length && soloDigitosPos; i++)
                    {
                        if (!Char.IsDigit(precioSinSym[i]))
                        {
                            soloDigitosPos = false;
                        }
                    }
                    if (soloDigitosPos && Convert.ToDecimal(precioSinSym) >= 0) //Conv.ToDec es adecuado? mejor 'toNumber'?
                        valido = true;
                }
            }
            return valido;
        }
        private bool validos (string idProd, string des, string precio)
        {   //idProd: 1234-123  des:no simbol  precio:Enteros,Decimales Positivos

            bool todosValidos = false, valP = false, valD = false, valPre = false;
            if (idProd.Length == 8 && idProd[4] == '-')
            {
                string preGuion = idProd.Substring(0, 4);
                string postGuion = idProd.Substring(5, 3);

                if (!string.IsNullOrEmpty(preGuion) && preGuion.All(Char.IsDigit)
                    && !string.IsNullOrEmpty(postGuion) && postGuion.All(Char.IsDigit))
                {
                    valP = true;
                }
            }
            if (!string.IsNullOrEmpty(des) && des.All(Char.IsSymbol)) //MAL comprobación sin Symbolos, usa .contains
            {
                valD = true;
            }
            valPre = valEspecifPre(precio); //ME FALTA TRIM(€) ?!

            if (valP && valD && valPre)
            {
                todosValidos = true;
            }
            return todosValidos;
        }
        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            lblMensajes.Text = "";
            String strIdProducto, strDescripcion, strIdUnidad, strIdTipo, strPrecio;
            Decimal dcPrecio;

            //Validación de campos (para Insertar y Modificar)
            strIdProducto = txtIdProducto.Text;
            strDescripcion = txtDesPro.Text;
            strPrecio = txtPrePro.Text;

            if (validos(strIdProducto, strDescripcion, strPrecio))
            {
                // PRODECER A LA INSERCIÓN
            }
            else
            {
                //LANZAR ERROR DE VALIDACIÓN DE INPUTS
            }

            dcPrecio = Convert.ToDecimal(txtPrePro.Text);
            strIdUnidad = ddlIdUnidad.SelectedItem.Text;
            strIdTipo = ddlIdTipo.SelectedItem.Value;
            string StrCadenaConexion = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string StrComandoSql = "INSERT PRODUCTO " +
                "(IdProducto,DesPro,PrePro,IdUnidad,IdTipo) VALUES (" +
                "'" + strIdProducto + "','" + strDescripcion +
                "'," + FnComaPorPunto(dcPrecio) +
                ",'" + strIdUnidad + "','" + strIdTipo + "');";

            using (SqlConnection conexion = new SqlConnection(StrCadenaConexion))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = conexion.CreateCommand();
                    comando.CommandText = StrComandoSql;
                    int inRegistrosAfectados = comando.ExecuteNonQuery();
                    if (inRegistrosAfectados == 1)
                        lblMensajes.Text = "Registro insertado correctamente";
                    else
                        lblMensajes.Text = "Error al insertar el registro";
                    btnNuevo.Visible = true;
                    btnEditar.Visible = false;
                    btnEliminar.Visible = false;
                    btnInsertar.Visible = false;
                    btnModificar.Visible = false;
                    btnBorrar.Visible = false;
                    btnCancelar.Visible = false;
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
            grdProductos.DataBind(); // Vuelve a enlazar el GridVIew para que se actualicen los datos
            grdProductos.SelectedIndex = -1;
            FnDeshabilitarControles();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblMensajes.Text = "";
            btnNuevo.Visible = true;
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
            btnInsertar.Visible = false;
            btnModificar.Visible = false;
            btnBorrar.Visible = false;
            btnCancelar.Visible = false;
            txtIdProducto.Text = "";
            txtDesPro.Text = "";
            txtPrePro.Text = Convert.ToString(0);
            ddlIdUnidad.DataBind();
            ddlIdTipo.DataBind();
            grdProductos.SelectedIndex = -1;
            FnDeshabilitarControles();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            lblMensajes.Text = "";
            //habilitar todos controles de campos menos txtIdProducto
            txtDesPro.Enabled = true;
            txtPrePro.Enabled = true;
            ddlIdUnidad.Enabled = true;
            ddlIdTipo.Enabled = true;
            //hacer visible botones MODIFICAR y CANCELAR
            btnModificar.Visible = true;
            btnCancelar.Visible = true;
            btnEditar.Visible = false;
            btnNuevo.Visible = false;
            btnEliminar.Visible = false;
            //codigo para sentencia UPDATE (en BOTON MODIFICAR)


        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //eliminar registro selec en gridview de la tabla PRODUCTO para ello:
            // Hacer visibles botones : BORRAR Y CANCELAR
            lblMensajes.Text = "";
            btnBorrar.Visible = true;
            btnCancelar.Visible = true;
            btnEditar.Visible = false;
            btnModificar.Visible = false;
            btnNuevo.Visible = false;
            btnEliminar.Visible = false;
            //en BORRAR_OnCLICK: sentencia DELETE 
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string StrIdProducto, StrDesc, StrIdUnidad, StrIdTipo;
            decimal dcPrecio;
            StrIdProducto = txtIdProducto.Text;
            StrDesc = txtDesPro.Text;

            string PrecioConSim = txtPrePro.Text.Trim(new Char[] { '€' });
            dcPrecio = Convert.ToDecimal(PrecioConSim);
            StrIdUnidad = ddlIdUnidad.SelectedItem.Text;
            StrIdTipo = ddlIdTipo.SelectedItem.Value;

            string StrCadenaConexion = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string StrComandoSql = "UPDATE PRODUCTO " +
                    "SET DesPro = '" + StrDesc + "', PrePro = '" + FnComaPorPunto(dcPrecio) + "', IdUnidad = '" + StrIdUnidad +
                    "', IdTipo = '" + StrIdTipo + "'" +
                    " WHERE PRODUCTO.IdProducto = '" + StrIdProducto + "';";

            using (SqlConnection conexion = new SqlConnection(StrCadenaConexion))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = conexion.CreateCommand();
                    comando.CommandText = StrComandoSql;
                    int inRegistrosAfectados = comando.ExecuteNonQuery();
                    if (inRegistrosAfectados == 1)
                        lblMensajes.Text = "Registro modificado correctamente";
                    else
                        lblMensajes.Text = "Error al insertar el registro";
                    btnNuevo.Visible = true;
                    btnEditar.Visible = false;
                    btnEliminar.Visible = false;
                    btnInsertar.Visible = false;
                    btnModificar.Visible = false;
                    btnBorrar.Visible = false;
                    btnCancelar.Visible = false;
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
            grdProductos.DataBind(); // Vuelve a enlazar el GridVIew para que se actualicen los datos
            grdProductos.SelectedIndex = -1;
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            string StrIdProducto = txtIdProducto.Text;

            string StrCadenaConexion = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string StrComandoSql = "DELETE FROM PRODUCTO " +
                    "WHERE IdProducto = '" + StrIdProducto + "';";

            using (SqlConnection conexion = new SqlConnection(StrCadenaConexion))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = conexion.CreateCommand();
                    comando.CommandText = StrComandoSql;
                    int inRegistrosAfectados = comando.ExecuteNonQuery();
                    if (inRegistrosAfectados == 1)
                        lblMensajes.Text = "Eliminado correctamente";
                    else
                        lblMensajes.Text = "Error al insertar el registro";
                    btnNuevo.Visible = true;
                    btnEditar.Visible = false;
                    btnEliminar.Visible = false;
                    btnInsertar.Visible = false;
                    btnModificar.Visible = false;
                    btnBorrar.Visible = false;
                    btnCancelar.Visible = false;
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
            grdProductos.DataBind(); // Vuelve a enlazar el GridVIew para que se actualicen los datos
            grdProductos.SelectedIndex = -1;
        }
    }
}
