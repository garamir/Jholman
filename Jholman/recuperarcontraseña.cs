﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;


namespace Jholman
{
    public partial class recuperarcontraseña : Form
    {
        public recuperarcontraseña()
        {
            InitializeComponent();
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("¡Ingrese el usuario!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CtrLogin pLogin = new CtrLogin();

                int resultado = pLogin.Recuperar(txtUsuario.Text.Trim());

                if (resultado > 0)
                {
                    Habilitar();
                    Deshabilitar();
                }
                else
                {
                    MessageBox.Show("¡Usuario no encontrado!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsuario.Text = "";
                }
            }
        }

        void Habilitar()
        {
            txtPregunta.Enabled = true;
            txtRespuesta.Enabled = true;
            btnEnviar.Enabled = true;
        }

        void Deshabilitar()
        {
            txtUsuario.Enabled = false;            
            btnValidar.Enabled = false;          
        }

        void Habilitar2()
        {
            txtConfirmarContraseña.Enabled = true;
            txtContraseñaNueva.Enabled = true;
            btnAceptar.Enabled = true;
        }

        void Deshabilitar2()
        {
            txtPregunta.Enabled = false;
            txtRespuesta.Enabled = false;
            btnEnviar.Enabled = false;
            txtUsuario.Enabled = false;
            btnValidar.Enabled = false;  
        }


        void Limpiar()
        {
            txtUsuario.Clear();            
            txtPregunta.ResetText();
            txtRespuesta.Clear();
            txtContraseñaNueva.Clear();
            txtConfirmarContraseña.Clear();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPregunta.Text) || string.IsNullOrWhiteSpace(txtRespuesta.Text))
            {
                MessageBox.Show("¡Hay uno o mas campos vacios!", "¡Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CtrLogin pLogin = new CtrLogin();

                int resultado = pLogin.Seguridad(txtPregunta.Text.Trim(), txtRespuesta.Text.Trim());

                if (resultado > 0)
                {
                    Habilitar2();
                    Deshabilitar2();                    
                }
                else
                {
                    MessageBox.Show("¡Datos incorrectos, intente nuevamente!", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPregunta.Text = "";
                    txtRespuesta.Text = "";
                }
            }
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContraseñaNueva.Text) || string.IsNullOrWhiteSpace(txtConfirmarContraseña.Text))
            {
                MessageBox.Show("¡Hay uno o mas campos vacios!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CtrUsuario pUsuario = new CtrUsuario();

                //password = txtContraseña.Text;
                //MD5 md5 = MD5.Create();
                //byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
                //byte[] hash = md5.ComputeHash(inputBytes);
                //password = BitConverter.ToString(hash).Replace("_", "");

                if (txtContraseñaNueva.Text == txtConfirmarContraseña.Text)
                {

                    int resultado = 0;
                    
                        resultado = pUsuario.ContraseñaNueva(txtUsuario.Text.Trim(), txtContraseñaNueva.Text.Trim());
                    
                    if (resultado > 0)
                    {
                        MessageBox.Show("¡Contraseña nueva guardada correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    //else if (resultado == 3)
                    {
                        MessageBox.Show("¡No se pudo guardar la nueva contraseña!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtContraseñaNueva.Text = "";
                        txtConfirmarContraseña.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("¡Verifique! ¡Las contraseñas no coinciden!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContraseñaNueva.Text = "";
                    txtConfirmarContraseña.Text = "";
                }
            }
        }
    }
}
