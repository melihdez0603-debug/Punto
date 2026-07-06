using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System;

namespace Punto.Forms
{
    public partial class frmProductos : Form
    {
        Conexion con = new Conexion();
        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, System.EventArgs e)
        {
            CargarProductos();
        }
        private void CargarProductos()
        {
            // Código para llenar el DataGridView
            try
            {
                MySqlConnection conexion = con.ObtenerConexion();

                string sql = "SELECT * FROM productos";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, conexion);

                DataTable dt = new DataTable();

                da.Fill(dt);

                dgvProductos.DataSource = dt;

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void btnNuevo_Click(object sender, System.EventArgs e)
        {
            decimal precio;
            int stock;

            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Precio inválido");
                return;
            }

            if (!int.TryParse(txtStock.Text, out stock))
            {
                MessageBox.Show("Stock inválido");
                return;
            }

            try
            {
                Conexion con = new Conexion();

                MySqlConnection conexion = con.ObtenerConexion();

                string sql = @"INSERT INTO productos
                (codigo,descripcion,precio,stock)
                VALUES
                (@codigo,@descripcion,@precio,@stock)";
                MySqlCommand cmd = new MySqlCommand(sql, conexion);

                cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                cmd.Parameters.AddWithValue("@descripcion", txtNombre.Text);   
                cmd.Parameters.AddWithValue("@precio", precio);
                cmd.Parameters.AddWithValue("@stock", stock);
                
                cmd.ExecuteNonQuery();

                conexion.Close();

                MessageBox.Show("Producto guardado.");

                CargarProductos();
                Limpiar();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                txtId.Text = fila.Cells["producto_id"].Value.ToString();
                txtCodigo.Text = fila.Cells["codigo"].Value.ToString();
                txtNombre.Text = fila.Cells["descripcion"].Value.ToString();
                txtPrecio.Text = fila.Cells["precio"].Value.ToString();
                txtStock.Text = fila.Cells["stock"].Value.ToString();
            }
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnEditar_Click(object sender, System.EventArgs e)
        {
            try
            {
                Conexion con = new Conexion();

                MySqlConnection conexion = con.ObtenerConexion();

                string sql = @"UPDATE productos
                SET codigo=@codigo,
                descripcion=@descripcion,
                precio=@precio,
                stock=@stock
                WHERE producto_id=@id";

                MySqlCommand cmd = new MySqlCommand(sql, conexion);

                cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                cmd.Parameters.AddWithValue("@descripcion", txtNombre.Text);
                cmd.Parameters.AddWithValue("@precio", decimal.Parse(txtPrecio.Text));
                cmd.Parameters.AddWithValue("@stock", int.Parse(txtStock.Text));
                cmd.Parameters.AddWithValue("@id", txtId.Text);

                cmd.ExecuteNonQuery();

                conexion.Close();

                MessageBox.Show("Registro actualizado.");

                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }      
           
        }

        private void btnEliminar_Click(object sender, System.EventArgs e)
        {
            DialogResult r = MessageBox.Show(
                    "¿Desea eliminar este producto?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                try
                {
                    Conexion con = new Conexion();

                    MySqlConnection conexion = con.ObtenerConexion();

                    string sql = "DELETE FROM productos WHERE producto_id=@id";

                    MySqlCommand cmd = new MySqlCommand(sql, conexion);

                    cmd.Parameters.AddWithValue("@id", txtId.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Producto eliminado.");

                    CargarProductos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }
        private void Limpiar()
        {
            txtId.Clear();
            txtCodigo.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            txtCodigo.Focus();
        }
    }
}
