using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestorInventario
{
    public partial class frmAMCProd : Form
    {

        public frmAMCProd()
        {
            InitializeComponent();
            CargarCmbCategorias();
        }
        public void Abrir(int Modo, int Codigo)
        {
            switch (Modo)
            {
                //Consultar
                case 0:
                    this.Text = "Consultar";

                    btnGrabar.Visible = false;
                    txtCodigo.Enabled = false;
                    txtNombre.Enabled = false;
                    txtDescripcion.Enabled = false;
                    txtPrecio.Enabled = false;
                    txtStock.Enabled = false;
                    cmbCategoria.Enabled = false;

                    AbrirConsulta(Codigo);

                    break;
                //Agregar
                case 1:
                    this.Text = "Agregar";

                    txtCodigo.Visible = false;
                    label1.Text = "El codigo del producto se autoasigna al crearse";

                    break;
                //Modificar
                case 2:
                    this.Text = "Modificar";
                    txtCodigo.Enabled = false;

                    AbrirModificar(Codigo);

                    break;
            }
            this.ShowDialog();
        }
        public bool Validacion()
        {
            bool validado = true;
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Faltan completar el nombre", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                validado = false;
            }
            if (txtDescripcion.Text == "")
            {
                MessageBox.Show("Faltan completar la descripcion", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                validado = false;
            }
            if (txtPrecio.Text == "")
            {
                MessageBox.Show("Faltan completar el precio", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                validado = false;
            }
            if (txtStock.Text == "")
            {
                MessageBox.Show("Faltan completar el stock", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                validado = false;
            }
            if (cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("No selecciono una categoria", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                validado = false;
            }
            return validado;
        }
        public void AbrirConsulta(int Codigo)
        {
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    string strQuery = "select * from Productos p where p.Codigo = " + Codigo;

                    SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                    using (SqlDataReader reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txtCodigo.Text = reader["Codigo"].ToString();
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtDescripcion.Text = reader["Descripcion"].ToString();
                            txtPrecio.Text = reader["Precio"].ToString();
                            txtStock.Text = reader["Stock"].ToString();
                            cmbCategoria.SelectedIndex = Convert.ToInt32(reader["CategoriaId"]) - 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al consultar el producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }
        public void AbrirModificar(int Codigo)
        {
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    string strQuery = "select * from Productos p where p.Codigo = " + Codigo;

                    SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                    using (SqlDataReader reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txtCodigo.Text = reader["Codigo"].ToString();
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtDescripcion.Text = reader["Descripcion"].ToString();
                            txtPrecio.Text = reader["Precio"].ToString();
                            txtStock.Text = reader["Stock"].ToString();
                            cmbCategoria.SelectedIndex = Convert.ToInt32(reader["CategoriaId"]) - 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al cargar el producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }
        public void CargarCmbCategorias()
        {
            cmbCategoria.Items.Clear();
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    string strQuery = "select * from Categorias";

                    SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                    using (SqlDataReader reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbCategoria.Items.Insert(Convert.ToInt32(reader["Id"]) - 1, reader["Nombre"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al cargar las categorias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }

        //Buttons
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (this.Text == "Agregar")
            {
                clsConexionBD objConnection = new clsConexionBD();
                if (objConnection.GetError() == "")
                {
                    try
                    {
                        string strQuery = "INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, CategoriaId) VALUES (@nombre, @descripcion, @precio, @stock, @categoriaId)";

                        SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                        objCommand.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        objCommand.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                        objCommand.Parameters.AddWithValue("@precio", Convert.ToDecimal(txtPrecio.Text));
                        objCommand.Parameters.AddWithValue("@stock", Convert.ToInt32(txtStock.Text));
                        objCommand.Parameters.AddWithValue("@categoriaId", Convert.ToInt32(cmbCategoria.SelectedIndex) + 1);
                        objCommand.ExecuteNonQuery();
                        MessageBox.Show("Se agrego con exito el producto", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al grabar el producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                objConnection.CloseConnection();
            }
            else
            {
                if (this.Text == "Modificar") {
                    clsConexionBD objConnection = new clsConexionBD();
                    if (objConnection.GetError() == "")
                    {
                        try
                        {
                            string strQuery = "UPDATE Productos Set Nombre = @nombre, Descripcion = @descripcion, precio = @precio, Stock = @stock, CategoriaId = @categoriaid where Codigo = @codigo";

                            SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                            objCommand.Parameters.AddWithValue("@codigo", Convert.ToInt32(txtCodigo.Text));
                            objCommand.Parameters.AddWithValue("@nombre", txtNombre.Text);
                            objCommand.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                            objCommand.Parameters.AddWithValue("@precio", Convert.ToDecimal(txtPrecio.Text));
                            objCommand.Parameters.AddWithValue("@stock", Convert.ToInt32(txtStock.Text));
                            objCommand.Parameters.AddWithValue("@categoriaId", Convert.ToInt32(cmbCategoria.SelectedIndex) + 1);
                            objCommand.ExecuteNonQuery();
                            MessageBox.Show("Se modifico con exito el producto", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al modificar el producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    objConnection.CloseConnection();
                }
            }
        }
    }
}
