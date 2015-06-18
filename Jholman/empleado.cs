using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;
using Entity;
using Model;
using MySql.Data.MySqlClient;


namespace Jholman
{
    public partial class empleado : Form
    {
        public empleado()
        {
            InitializeComponent();
        }

        public ClsEmpleado EmpleadoSeleccionado = null;
        public ClsEmpleado empleadoActual { get; set; }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            Habilitar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTipoDocumento.Text) || string.IsNullOrWhiteSpace(txtIdentificacion.Text) || string.IsNullOrWhiteSpace(txtNombres.Text) ||
                string.IsNullOrWhiteSpace(txtApellidos.Text) || string.IsNullOrWhiteSpace(cbxSexo.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) || string.IsNullOrWhiteSpace(txtCelular.Text) ||
                string.IsNullOrWhiteSpace(txtCargo.Text))
            {
                MessageBox.Show("¡Hay uno o más campos vacios!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CtrEmpleado pEmpleado = new CtrEmpleado();

                if (pEmpleado.EmpleadoRegistrado(Convert.ToInt32(txtIdentificacion.Text)) == 0)
                {
                    int resultado = pEmpleado.Guardar(txtTipoDocumento.Text.Trim(), Convert.ToInt32(txtIdentificacion.Text.Trim()), txtNombres.Text.Trim(), txtApellidos.Text.Trim(), cbxSexo.Text.Trim(),
                       txtTelefono.Text.Trim(), txtCelular.Text.Trim(), txtCargo.Text.Trim());

                    if (resultado > 0)
                    {
                        MessageBox.Show("¡Empleado guardado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CtrEmpleado cEmpleado = new CtrEmpleado();
                        dgvConsultar.DataSource = cEmpleado.cargarEmpleado();
                        Limpiar();
                        Deshabilitar();
                    }
                }
                else
                {
                    MessageBox.Show("¡El empleado ya existe!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Limpiar();
                    Deshabilitar();
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTipoDocumento.Text) || string.IsNullOrWhiteSpace(txtIdentificacion.Text) || string.IsNullOrWhiteSpace(txtNombres.Text) ||
                string.IsNullOrWhiteSpace(txtApellidos.Text) || string.IsNullOrWhiteSpace(cbxSexo.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) || string.IsNullOrWhiteSpace(txtCelular.Text) ||
                string.IsNullOrWhiteSpace(txtCargo.Text))
            {
                MessageBox.Show("¡Hay uno o más campos vacios!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CtrEmpleado pEmpleado = new CtrEmpleado();

                int resultado = pEmpleado.Actualizar(txtTipoDocumento.Text.Trim(), Convert.ToInt32(txtIdentificacion.Text.Trim()), txtNombres.Text.Trim(), txtApellidos.Text.Trim(), cbxSexo.Text.Trim(),
                   txtTelefono.Text.Trim(), txtCelular.Text.Trim(), txtCargo.Text.Trim());

                if (resultado > 0)
                {
                    MessageBox.Show("¡Empleado actualizado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CtrEmpleado cEmpleado = new CtrEmpleado();
                    dgvConsultar.DataSource = cEmpleado.cargarEmpleado();
                    Limpiar();
                    Deshabilitar();
                }
                else
                {
                    MessageBox.Show("¡No se pudo actualizar el empleado!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Eliminar empleado?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CtrEmpleado pEmpleado = new CtrEmpleado();

                int resultado = pEmpleado.Eliminar(Convert.ToInt32(txtIdentificacion.Text.Trim()));

                if (resultado > 0)
                {
                    MessageBox.Show("¡Empleado eliminado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CtrEmpleado cEmpleado = new CtrEmpleado();
                    dgvConsultar.DataSource = cEmpleado.cargarEmpleado();
                    Limpiar();
                    Deshabilitar();
                }
            }
            else
            {
                MessageBox.Show("¡No se pudo eliminar el empleado!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void empleado_Load(object sender, EventArgs e)
        {
            Deshabilitar();
            CtrEmpleado cEmpleado = new CtrEmpleado();
            dgvConsultar.DataSource = cEmpleado.cargarEmpleado();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvConsultar.SelectedRows.Count == 1)
            {
                string tipo = Convert.ToString(dgvConsultar.CurrentRow.Cells[0].Value);
                EmpleadoSeleccionado = ClsEmpleadosDAL.ObtenerEmpleado(tipo);

                if (EmpleadoSeleccionado != null)
                {
                    empleadoActual = EmpleadoSeleccionado;
                    txtTipoDocumento.Text = EmpleadoSeleccionado.TipoDocumento;
                    txtIdentificacion.Text = Convert.ToString(EmpleadoSeleccionado.Identificación);
                    txtNombres.Text = EmpleadoSeleccionado.Nombres;
                    txtApellidos.Text = EmpleadoSeleccionado.Apellidos;
                    cbxSexo.Text = EmpleadoSeleccionado.Sexo;
                    txtTelefono.Text = EmpleadoSeleccionado.Telefono;
                    txtCelular.Text = EmpleadoSeleccionado.Celular;
                    txtCargo.Text = EmpleadoSeleccionado.Cargo;

                    btnActualizar.Enabled = true;
                    btnEliminar.Enabled = true;
                    Habilitar();
                    btnGuardar.Enabled = false;
                    txtTipoDocumento.Enabled = false;
                    txtIdentificacion.Enabled = false;
                }
            }
            //else
            //{
            //    MessageBox.Show("¡Debe de seleccionar una fila!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string consulta = txtIdentificacion.Text;

            if (consulta == "")
            {
                MessageBox.Show("¡Debe ingresar una identificación!", "¡Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CtrEmpleado cEmpleado = new CtrEmpleado();
                dgvConsultar.DataSource = cEmpleado.Listar(Convert.ToInt32(txtIdentificacion.Text.Trim()));
            }
        }

        void Limpiar()
        {
            txtTipoDocumento.ResetText();
            txtIdentificacion.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            cbxSexo.ResetText();
            txtTelefono.Clear();
            txtCelular.Clear();
            txtCargo.ResetText();
        }

        void Habilitar()
        {
            txtTipoDocumento.Enabled = true;
            txtIdentificacion.Enabled = true;
            txtNombres.Enabled = true;
            txtApellidos.Enabled = true;
            cbxSexo.Enabled = true;
            txtTelefono.Enabled = true;
            txtCelular.Enabled = true;
            txtCargo.Enabled = true;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        void Deshabilitar()
        {
            txtTipoDocumento.Enabled = false;
            txtIdentificacion.Enabled = false;
            txtNombres.Enabled = false;
            txtApellidos.Enabled = false;
            cbxSexo.Enabled = false;
            txtTelefono.Enabled = false;
            txtCelular.Enabled = false;
            txtCargo.Enabled = false;
            btnGuardar.Enabled = false;
            btnEliminar.Enabled = false;
            btnActualizar.Enabled = false;
            btnNuevo.Enabled = true;
            btnCancelar.Enabled = true;
        }      

        private void dgvConsultar_MouseClick(object sender, MouseEventArgs e)
        {
            txtTipoDocumento.Text = dgvConsultar.SelectedRows[0].Cells[0].Value.ToString();
            txtIdentificacion.Text = dgvConsultar.SelectedRows[0].Cells[1].Value.ToString();
            txtNombres.Text = dgvConsultar.SelectedRows[0].Cells[2].Value.ToString();
            txtApellidos.Text = dgvConsultar.SelectedRows[0].Cells[3].Value.ToString();
            cbxSexo.Text = dgvConsultar.SelectedRows[0].Cells[4].Value.ToString();
            txtTelefono.Text = dgvConsultar.SelectedRows[0].Cells[5].Value.ToString();
            txtCelular.Text = dgvConsultar.SelectedRows[0].Cells[6].Value.ToString();
            txtCargo.Text = dgvConsultar.SelectedRows[0].Cells[7].Value.ToString();

        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            MySqlConnection conexion = BdConexion.ObtenerConexion();
            MySqlCommand cmd = conexion.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from empleados where identificacion like('" + txtBuscar.Text + "%') or nombres like('" + txtBuscar.Text + "%')";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            dgvConsultar.DataSource = dt;
            conexion.Close();
        }

        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void txtApellidos_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }
    }
}
