﻿using System;
using System.Windows.Forms;

namespace templatev1
{
    public partial class CAccManage : Form
    {
        public CAccManage()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            
        }
    }
}