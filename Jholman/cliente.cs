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
    public partial class cliente : Form
    {
        public cliente()
        {

            InitializeComponent();
        }
        public Clscliente clienteActual { get; set; }

        public Clscliente ClienteSeleccionado = null;         
          
        private void cliente_Load(object sender, EventArgs e)
        {
            Deshabilitar();
            CtrCliente cCliente = new CtrCliente();
            dgvConsultar.DataSource = cCliente.cargarClientes();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTipoDocumento.Text) ||string.IsNullOrWhiteSpace(txtIdentificacion.Text) || string.IsNullOrWhiteSpace(txtNombres.Text) ||
              string.IsNullOrWhiteSpace(txtApellidos.Text) || string.IsNullOrWhiteSpace(cbxSexo.Text) ||
              string.IsNullOrWhiteSpace(txtDireccion.Text) || string.IsNullOrWhiteSpace(txtTelefono.Text) ||
              string.IsNullOrWhiteSpace(txtCelular.Text) || string.IsNullOrWhiteSpace(txtNombreEmpresa.Text))
            {
                MessageBox.Show("¡Hay uno o más campos vacios!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CtrCliente pCliente = new CtrCliente();

                if (pCliente.ClienteRegistrado(Convert.ToInt32(txtIdentificacion.Text)) == 0)
                {
                    int resultado = pCliente.Guardar(txtTipoDocumento.Text.Trim(), Convert.ToInt32(txtIdentificacion.Text.Trim()), txtNombres.Text.Trim(), txtApellidos.Text.Trim(), cbxSexo.Text.Trim(),
                    txtDireccion.Text.Trim(), txtTelefono.Text.Trim(), txtCelular.Text.Trim(), txtNombreEmpresa.Text.Trim());

                    if (resultado > 0)
                    {
                        MessageBox.Show("¡Cliente guardado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CtrCliente cCliente = new CtrCliente();
                        dgvConsultar.DataSource = cCliente.cargarClientes();
                        Limpiar();
                        Deshabilitar();
                    }                   
                }
                else
                {
                    MessageBox.Show("¡El cliente ya existe!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Limpiar();
                    Deshabilitar();
                }
            }
        }        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }        

        //private void btnProbarConexion_Click(object sender, EventArgs e)
        //{
        //    BdConexion.ObtenerConexion();
        //    MessageBox.Show("Conectado a MySQL", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            Habilitar();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTipoDocumento.Text) || string.IsNullOrWhiteSpace(txtIdentificacion.Text) || string.IsNullOrWhiteSpace(txtNombres.Text) ||
              string.IsNullOrWhiteSpace(txtApellidos.Text) || string.IsNullOrWhiteSpace(cbxSexo.Text) ||
              string.IsNullOrWhiteSpace(txtDireccion.Text) || string.IsNullOrWhiteSpace(txtTelefono.Text) ||
              string.IsNullOrWhiteSpace(txtCelular.Text) || string.IsNullOrWhiteSpace(txtNombreEmpresa.Text))
            {
                MessageBox.Show("¡Hay uno o más campos vacios!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {                
                CtrCliente pCliente = new CtrCliente();
                
                int resultado = pCliente.Actualizar(txtTipoDocumento.Text.Trim(), Convert.ToInt32(txtIdentificacion.Text.Trim()), txtNombres.Text.Trim(), txtApellidos.Text.Trim(), cbxSexo.Text.Trim(),
                    txtDireccion.Text.Trim(), txtTelefono.Text.Trim(), txtCelular.Text.Trim(), txtNombreEmpresa.Text.Trim());

                if (resultado > 0)
                {
                    MessageBox.Show("¡Cliente actualizado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CtrCliente cCliente = new CtrCliente();
                    dgvConsultar.DataSource = cCliente.cargarClientes();
                    Limpiar();
                    Deshabilitar();
                }
                else
                {
                    MessageBox.Show("¡No se pudo actualizar el cliente!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Eliminar cliente?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CtrCliente pCliente = new CtrCliente();

                int resultado = pCliente.Eliminar(Convert.ToInt32(txtIdentificacion.Text.Trim()));

                if (resultado > 0)
                {
                    MessageBox.Show("¡Cliente eliminado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CtrCliente cCliente = new CtrCliente();
                    dgvConsultar.DataSource = cCliente.cargarClientes();
                    Limpiar();
                    Deshabilitar();
                }                
            }
            else
            {
                MessageBox.Show("¡No se pudo eliminar el cliente!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvConsultar.SelectedRows.Count == 1)
            {
                string tipo = Convert.ToString(dgvConsultar.CurrentRow.Cells[0].Value);
                ClienteSeleccionado = ClsClientesDAL.ObtenerCliente(tipo);

                if (ClienteSeleccionado != null)
                {
                    clienteActual = ClienteSeleccionado;
                    txtTipoDocumento.Text = ClienteSeleccionado.Tipo_documento;
                    txtIdentificacion.Text = Convert.ToString(ClienteSeleccionado.Identificación);
                    txtNombres.Text = ClienteSeleccionado.Nombres;
                    txtApellidos.Text = ClienteSeleccionado.Apellidos;
                    cbxSexo.Text = ClienteSeleccionado.Género;
                    txtDireccion.Text = ClienteSeleccionado.Dirección;
                    txtTelefono.Text = ClienteSeleccionado.Telefono;
                    txtCelular.Text = ClienteSeleccionado.Celular;
                    txtNombreEmpresa.Text = ClienteSeleccionado.Empresa;

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
            string consulta = txtBuscar.Text;

            if (consulta == "")
            {
                MessageBox.Show("¡Debe ingresar un parametro!", "¡Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CtrCliente cCliente = new CtrCliente();
                dgvConsultar.DataSource = cCliente.Listar(Convert.ToInt32(txtBuscar.Text.Trim()));
                txtBuscar.Text = "";
            }
        }      
        void Limpiar()
        {
            txtTipoDocumento.ResetText();
            txtIdentificacion.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            cbxSexo.ResetText();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtCelular.Clear();
            txtNombreEmpresa.Clear();
        }

        void Habilitar()
        {
            txtTipoDocumento.Enabled = true;
            txtIdentificacion.Enabled = true;
            txtNombres.Enabled = true;
            txtApellidos.Enabled = true;
            cbxSexo.Enabled = true;
            txtDireccion.Enabled = true;
            txtTelefono.Enabled = true;
            txtCelular.Enabled = true;
            txtNombreEmpresa.Enabled = true;
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
            txtDireccion.Enabled = false;
            txtTelefono.Enabled = false;
            txtCelular.Enabled = false;
            txtNombreEmpresa.Enabled = false;
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
            txtDireccion.Text = dgvConsultar.SelectedRows[0].Cells[5].Value.ToString();
            txtTelefono.Text = dgvConsultar.SelectedRows[0].Cells[6].Value.ToString();
            txtCelular.Text = dgvConsultar.SelectedRows[0].Cells[7].Value.ToString();
            txtNombreEmpresa.Text = dgvConsultar.SelectedRows[0].Cells[8].Value.ToString();
        }
        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            MySqlConnection conexion = BdConexion.ObtenerConexion();
            MySqlCommand cmd = conexion.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from clientes where identificacion like('" + txtBuscar.Text + "%') or nombres like('" + txtBuscar.Text + "%')";
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

        private void txtNombreEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void dgvConsultar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
    }
}
