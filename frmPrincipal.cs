using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestorInventario
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            CargaCmbBuscarPor();
            ActualizarListado(0);
        }
        //cmb
        public void CargaCmbBuscarPor()
        {
            cmbBuscarPor.Items.Clear();
            cmbBuscarPor.Items.Insert(0, "Todos");
            cmbBuscarPor.Items.Insert(1, "Nombre");
            cmbBuscarPor.Items.Insert(2, "Codigo");
            cmbBuscarPor.Items.Insert(3, "Categoria");
            cmbBuscarPor.SelectedIndex = 0;
        }

        //dgv
        public void ActualizarListado(int Index)
        {
            dgv.Rows.Clear();
            switch (Index)
            {
                case 0:
                    ActualizarListadoCompleto();
                    break;
                case 1:
                    ActualizarListadoNombre();
                    break;
                case 2:
                    ActualizarListadoCodigo();
                    break;
                case 3:
                    ActualizarListadoCategoria();
                    break;
            }
            PintarStockBajo();
        }
        public void PintarStockBajo()
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (Convert.ToInt32(row.Cells[4].Value) <= 10)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    if (Convert.ToInt32(row.Cells[4].Value) <= 30)
                    {
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
            }
                
        }
        public void ActualizarListadoCompleto()
        {
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    string strQuery = "select p.Codigo, p.Nombre, p.Descripcion, p.Precio, p.Stock, c.Nombre as Categoria from Productos p join Categorias c on p.CategoriaId = c.Id";

                    SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                    using (SqlDataReader reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dgv.Rows.Add(reader["Codigo"].ToString(),
                                         reader["Nombre"].ToString(),
                                         reader["Descripcion"].ToString(),
                                         reader["Precio"].ToString(),
                                         reader["Stock"].ToString(),
                                         reader["Categoria"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " +  ex.Message, "Error al actualizar listado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }
        public void ActualizarListadoNombre()
        {
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    string strQuery = "select p.Codigo, p.Nombre, p.Descripcion, p.Precio, p.Stock, c.Nombre as Categoria from Productos p join Categorias c on p.CategoriaId = c.Id where p.Nombre like '%" + txtBuscar.Text + "%'";

                    SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                    using (SqlDataReader reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dgv.Rows.Add(reader["Codigo"].ToString(),
                                         reader["Nombre"].ToString(),
                                         reader["Descripcion"].ToString(),
                                         reader["Precio"].ToString(),
                                         reader["Stock"].ToString(),
                                         reader["Categoria"].ToString());
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al actualizar listado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }
        public void ActualizarListadoCodigo()
        {
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    string strQuery = "select p.Codigo, p.Nombre, p.Descripcion, p.Precio, p.Stock, c.Nombre as Categoria from Productos p join Categorias c on p.CategoriaId = c.Id where p.Codigo = " + txtBuscar.Text;

                    SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                    using (SqlDataReader reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dgv.Rows.Add(reader["Codigo"].ToString(),
                                         reader["Nombre"].ToString(),
                                         reader["Descripcion"].ToString(),
                                         reader["Precio"].ToString(),
                                         reader["Stock"].ToString(),
                                         reader["Categoria"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al actualizar listado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }
        public void ActualizarListadoCategoria()
        {
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    string strQuery = "select p.Codigo, p.Nombre, p.Descripcion, p.Precio, p.Stock, c.Nombre as Categoria from Productos p join Categorias c on p.CategoriaId = c.Id where c.Nombre like '%" + txtBuscar.Text + "%'";

                    SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                    using (SqlDataReader reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dgv.Rows.Add(reader["Codigo"].ToString(),
                                         reader["Nombre"].ToString(),
                                         reader["Descripcion"].ToString(),
                                         reader["Precio"].ToString(),
                                         reader["Stock"].ToString(),
                                         reader["Categoria"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al actualizar listado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(),"Error al conectar",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarListado(cmbBuscarPor.SelectedIndex);
            txtBuscar.Text = "";
        }

        //Buttons
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                int Codigo = Convert.ToInt32(dgv.SelectedRows[0].Cells[0].Value.ToString());

                frmAMCProd frmConsulta = new frmAMCProd();
                frmConsulta.Abrir(0, Codigo);
                ActualizarListado(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al abrir el formulario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                frmAMCProd frmAgregar = new frmAMCProd();
                frmAgregar.Abrir(1, 0);
                ActualizarListado(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al abrir el formulario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                int Codigo = Convert.ToInt32(dgv.SelectedRows[0].Cells[0].Value.ToString());

                frmAMCProd frmModificar = new frmAMCProd();
                frmModificar.Abrir(2, Codigo);
                ActualizarListado(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al abrir el formulario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    if (MessageBox.Show("Cuando borra un producto es permantente, Quiere eliminar el producto?", "Confirmacion Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        string strQuery = "DELETE FROM Productos where Codigo = @codigo";

                        SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                        objCommand.Parameters.AddWithValue("@codigo", Convert.ToInt32(dgv.SelectedRows[0].Cells[0].Value.ToString()));
                        objCommand.ExecuteNonQuery();
                        MessageBox.Show("Se elimino el producto con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ActualizarListado(0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al borrar el producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
