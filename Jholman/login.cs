using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Model;
using Entity;
using Controller;

namespace Jholman
{
    public partial class login : Form
    {
        public login()
        {
            //Thread tiempo = new Thread(new ThreadStart(screen1));
            //tiempo.Start();
            //Thread.Sleep(7000);
            InitializeComponent();
            //tiempo.Abort();
        }

        public void screen1()
        {
            Application.Run(new presentacion());
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MessageBox.Show("¡Acceso denegado, ingrese todos los datos!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CtrLogin pLogin = new CtrLogin();

                int resultado = pLogin.Validar(txtUsuario.Text.Trim(), txtContraseña.Text.Trim());

                if (resultado > 0)
                {
                    MessageBox.Show("¡Bienvenido a Inventario JHOLMAN!", "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    menu menu = new menu();
                    menu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("¡Usuario o contraseña incorrectos!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsuario.Text = "";
                    txtContraseña.Text = "";
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            usuario user = new usuario();
            user.ShowDialog();
        }

        private void btnRecuperar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {            
            recuperarcontraseña recu = new recuperarcontraseña();
            recu.ShowDialog();
        }

        private void login_Load(object sender, EventArgs e)
        {
           
        }
    }
}
