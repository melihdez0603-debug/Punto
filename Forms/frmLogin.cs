using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Punto.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            if (txtUsuario.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Complete todos los campos.");
                return;
            }

            try
            {
                Conexion con = new Conexion();

                MySqlConnection conexion = con.ObtenerConexion();

                string sql = "SELECT nombre_completo FROM usuarios WHERE username=@user AND password=@pass";

                MySqlCommand cmd = new MySqlCommand(sql, conexion);

                cmd.Parameters.AddWithValue("@user", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text);

                object resultado = cmd.ExecuteScalar();

                if (resultado != null)
                {
                    MessageBox.Show("Bienvenido " + resultado.ToString());

                    this.Hide();

                    frmPrincipal principal = new frmPrincipal();
                    principal.ShowDialog();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.");
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void txtUser_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void lblUser_Click(object sender, EventArgs e)
        {

        }
    }
}
