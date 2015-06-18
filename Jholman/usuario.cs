using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Controller;
using Entity;
using Model;
using MySql.Data.MySqlClient;

namespace Jholman
{
    public partial class usuario : Form
    {
        public usuario()
        {
            InitializeComponent();
        }
        public ClsUsuario usuarioActual { get; set; 
        }
        public ClsUsuario UsuarioSeleccionado = null;
        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text) ||
              string.IsNullOrWhiteSpace(txtPregunta.Text) || string.IsNullOrWhiteSpace(txtRespuesta.Text))
            {
                MessageBox.Show("¡Hay uno o más campos vacios!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CtrUsuario pUsuario = new CtrUsuario();

                if (pUsuario.UsuarioRegistrado(txtUsuario.Text) == 0)
                {
                    if (txtContraseña.Text == txtVerificar.Text)
                    {
                        int resultado = pUsuario.Crear(txtNombre.Text.Trim(), txtUsuario.Text.Trim(), txtContraseña.Text.Trim(), txtPregunta.Text.Trim(), txtRespuesta.Text.Trim());

                        if (resultado > 0)
                        {
                            MessageBox.Show("¡Usuario guardado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CtrUsuario cUsuario = new CtrUsuario();
                            dgvConsultar.DataSource = cUsuario.cargarUsuario();
                            Limpiar();
                            Deshabilitar();
                        }
                        else
                        {
                            MessageBox.Show("¡Verifique! ¡Las contraseñas no coinciden!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("¡El usuario ya existe!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Limpiar();
                    Deshabilitar();
                }
            }
        }  
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            Habilitar();
        }

        private void usuario_Load(object sender, EventArgs e)
        {
            Deshabilitar();
            CtrUsuario cUsuario = new CtrUsuario();
            dgvConsultar.DataSource = cUsuario.cargarUsuario();
        }        
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text) ||
             string.IsNullOrWhiteSpace(txtPregunta.Text) || string.IsNullOrWhiteSpace(txtRespuesta.Text))
            {
                MessageBox.Show("¡Hay uno o más campos vacios!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CtrUsuario pUsuario = new CtrUsuario();

                if (txtContraseña.Text == txtVerificar.Text)
                {
                    int resultado = pUsuario.Actualizar(txtNombre.Text.Trim(), txtUsuario.Text.Trim(), txtContraseña.Text.Trim(), txtPregunta.Text.Trim(), txtRespuesta.Text.Trim());
                    
                    if (resultado > 0)
                    {
                        MessageBox.Show("¡Usuario actualizado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CtrUsuario cUsuario = new CtrUsuario();
                        dgvConsultar.DataSource = cUsuario.cargarUsuario();
                        Limpiar();
                        Deshabilitar();
                    }
                    else
                    {
                        MessageBox.Show("¡El usuario no se pudo actualizar!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("¡Verifique! ¡Las contraseñas no coinciden!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string consulta = txtUsuario.Text;

            if (consulta == "")
            {
                MessageBox.Show("¡Debe ingresar un usuario!", "¡Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CtrUsuario cUsuario = new CtrUsuario();
                dgvConsultar.DataSource = cUsuario.Listar(txtUsuario.Text.Trim());
            }
        }
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvConsultar.SelectedRows.Count == 1)
            {
                string usuario = Convert.ToString(dgvConsultar.CurrentRow.Cells[0].Value);
                UsuarioSeleccionado = ClsUsuarioDAL.ObtenerUsuario(usuario);

                 if (UsuarioSeleccionado != null)
                 {
                     usuarioActual = UsuarioSeleccionado;
                     txtNombre.Text = UsuarioSeleccionado.Nombre;
                     txtUsuario.Text = UsuarioSeleccionado.Usuario;
                     txtContraseña.Text = UsuarioSeleccionado.Contraseña;
                     txtPregunta.Text = UsuarioSeleccionado.Pregunta;
                     txtRespuesta.Text = UsuarioSeleccionado.Respuesta;

                     btnActualizar.Enabled = true;
                     btnEliminar.Enabled = true;
                     Habilitar();
                     btnGuardar.Enabled = false;
                 }
            }
            //else
            //{
            //    MessageBox.Show("¡Debe de seleccionar una fila!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }
        void Limpiar()
        {
            txtNombre.Clear();
            txtUsuario.Clear();
            txtContraseña.Clear();
            txtVerificar.Clear();
            txtPregunta.ResetText();
            txtRespuesta.ResetText();
        }

        void Habilitar()
        {
            txtNombre.Enabled = true;
            txtUsuario.Enabled = true;
            txtContraseña.Enabled = true;
            txtPregunta.Enabled = true;
            txtRespuesta.Enabled = true;
            txtVerificar.Enabled = true;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        void Deshabilitar()
        {
            txtNombre.Enabled = false;
            txtUsuario.Enabled = false;
            txtContraseña.Enabled = false;
            txtPregunta.Enabled = false;
            txtRespuesta.Enabled = false;
            txtVerificar.Enabled = false;
            btnGuardar.Enabled = false;
            btnEliminar.Enabled = false;
            btnActualizar.Enabled = false;
            btnCancelar.Enabled = true;
            btnNuevo.Enabled = true;
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void dgvConsultar_MouseClick(object sender, MouseEventArgs e)
        {
            txtNombre.Text = dgvConsultar.SelectedRows[0].Cells[0].Value.ToString();
            txtUsuario.Text = dgvConsultar.SelectedRows[0].Cells[1].Value.ToString();
            txtContraseña.Text = dgvConsultar.SelectedRows[0].Cells[2].Value.ToString();
            txtPregunta.Text = dgvConsultar.SelectedRows[0].Cells[3].Value.ToString();
            txtRespuesta.Text = dgvConsultar.SelectedRows[0].Cells[4].Value.ToString();            
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            MySqlConnection conexion = BdConexion.ObtenerConexion();
            MySqlCommand cmd = conexion.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from usuarios where nombre like('" + txtBuscar.Text + "%') or usuario like('" + txtBuscar.Text + "%')";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            dgvConsultar.DataSource = dt;
            conexion.Close();
        }   
    }
}
