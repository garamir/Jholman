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
    public partial class materia_prima : Form
    {
        public materia_prima()
        {
            InitializeComponent();
        }
        public ClsMateriasPrimas materiaActual { get; set; }    

        public ClsMateriasPrimas MateriaSeleccionada = null;   
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtNombre.Text) || 
                string.IsNullOrWhiteSpace(txtStockMinimo.Text) || 
                string.IsNullOrWhiteSpace(txtStockMaximo.Text))
            {
                MessageBox.Show("¡Hay uno o más campos vacios!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CtrMateriaPrima pMateriaPrima = new CtrMateriaPrima();

                if (pMateriaPrima.MateriaRegistrada(Convert.ToString(txtNombre.Text)) == 0)
                {
                    int resultado = pMateriaPrima.Guardar(Convert.ToInt32(txtCodigo.Text.Trim()), txtNombre.Text.Trim(), Convert.ToInt32(txtStockMinimo.Text.Trim()), Convert.ToInt32(txtStockMaximo.Text.Trim()));

                    if (resultado > 0)
                    {
                        MessageBox.Show("¡Insumo guardado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CtrMateriaPrima cMateria = new CtrMateriaPrima();
                        dgvConsultar.DataSource = cMateria.cargarMaterias();
                        Limpiar();
                        Deshabilitar();
                    }
                }
                else
                {
                    MessageBox.Show("¡El insumo ya existe!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Limpiar();
                    Deshabilitar();
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtNombre.Text) ||
               string.IsNullOrWhiteSpace(txtStockMinimo.Text) ||
               string.IsNullOrWhiteSpace(txtStockMaximo.Text))
            {
                MessageBox.Show("¡Hay uno o más campos vacios!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CtrMateriaPrima pMateriaPrima = new CtrMateriaPrima();

                int resultado = pMateriaPrima.Actualizar(Convert.ToInt32(txtCodigo.Text.Trim()), txtNombre.Text.Trim(), Convert.ToInt32(txtStockMinimo.Text.Trim()), Convert.ToInt32(txtStockMaximo.Text.Trim()));

                if (resultado > 0)
                {
                    MessageBox.Show("¡Insumo actualizado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CtrMateriaPrima cMateria = new CtrMateriaPrima();
                    dgvConsultar.DataSource = cMateria.cargarMaterias();
                    Limpiar();
                    Deshabilitar();
                }
                else
                {
                    MessageBox.Show("¡No se pudo actualizar el insumo!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Eliminar insumo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CtrMateriaPrima pMateriaPrima = new CtrMateriaPrima();

                int resultado = pMateriaPrima.Eliminar(txtCodigo.Text.Trim());

                if (resultado > 0)
                {
                    MessageBox.Show("¡Insumo eliminado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CtrMateriaPrima cMateria = new CtrMateriaPrima();
                    dgvConsultar.DataSource = cMateria.cargarMaterias();
                    Limpiar();
                    Deshabilitar();
                }                
            }
            else
            {
                MessageBox.Show("¡No se pudo eliminar el insumo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Limpiar()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtStockMinimo.Clear();
            txtStockMaximo.Clear();            
        }

        void Habilitar()
        {
            txtCodigo.Enabled = true;
            txtNombre.Enabled = true;
            txtStockMinimo.Enabled = true;
            txtStockMaximo.Enabled = true;         
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnNuevo.Enabled = false;
        }

        void Deshabilitar()
        {
            txtCodigo.Enabled = false;
            txtNombre.Enabled = false;
            txtStockMinimo.Enabled = false;
            txtStockMaximo.Enabled = false;
            btnGuardar.Enabled = false;
            btnEliminar.Enabled = false;
            btnActualizar.Enabled = false;
            btnCancelar.Enabled = true;
            btnNuevo.Enabled = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            Habilitar();
        }

        private void materia_prima_Load(object sender, EventArgs e)
        {
            Deshabilitar();
            CtrMateriaPrima cMateria = new CtrMateriaPrima();
            dgvConsultar.DataSource = cMateria.cargarMaterias();                          
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvConsultar.SelectedRows.Count == 1)
            {
                int codigo = Convert.ToInt32(dgvConsultar.CurrentRow.Cells[0].Value);
                MateriaSeleccionada = ClsMateriasPrimasDAL.ObtenerMateria(codigo);

                if (MateriaSeleccionada != null)
                {
                    materiaActual = MateriaSeleccionada;
                    txtCodigo.Text = Convert.ToString(MateriaSeleccionada.Codigo);
                    txtNombre.Text = MateriaSeleccionada.Nombre;
                    txtStockMinimo.Text = Convert.ToString(MateriaSeleccionada.Stock_Minimo);
                    txtStockMaximo.Text = Convert.ToString(MateriaSeleccionada.Stock_Maximo);

                    btnActualizar.Enabled = true;
                    btnEliminar.Enabled = true;
                    //btnNuevo.Enabled = false;
                    Habilitar();
                    btnGuardar.Enabled = false;
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
                MessageBox.Show("¡Debe ingresar un codigo!", "¡Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CtrMateriaPrima cMateria = new CtrMateriaPrima();
                dgvConsultar.DataSource = cMateria.Listar(Convert.ToInt32(txtBuscar.Text.Trim()));
            }      
        }      

        private void dgvConsultar_MouseClick(object sender, MouseEventArgs e)
        {
            txtCodigo.Text = dgvConsultar.SelectedRows[0].Cells[0].Value.ToString();
            txtNombre.Text = dgvConsultar.SelectedRows[0].Cells[1].Value.ToString();
            txtStockMinimo.Text = dgvConsultar.SelectedRows[0].Cells[2].Value.ToString();
            txtStockMaximo.Text = dgvConsultar.SelectedRows[0].Cells[3].Value.ToString();              
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            MySqlConnection conexion = BdConexion.ObtenerConexion();
            MySqlCommand cmd = conexion.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from materiasprimas where codigo like('" + txtBuscar.Text + "%') or nombre like('" + txtBuscar.Text + "%')";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            dgvConsultar.DataSource = dt;
            conexion.Close();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void txtStockMinimo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtStockMaximo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }
    }
}
