﻿using System;
using System.Windows.Forms;

namespace templatev1
{
    public partial class OnSaleAdd : Form
    {
        public OnSaleAdd()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
            lblOnShelveDate.Text = DateTime.Now.ToString("dd-MM-yy");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}