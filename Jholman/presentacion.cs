﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jholman
{
    public partial class presentacion : Form
    {
        public presentacion()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);

            if (progressBar1.Value == 30)
            {
                timer1.Stop();
            }
        }
    }
}
