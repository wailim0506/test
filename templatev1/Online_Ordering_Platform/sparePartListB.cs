﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1.Online_Ordering_Platform
{
    public partial class sparePartListB : Form
    {
        public sparePartListB()
        {
            InitializeComponent();
        }

        private void sparePartListB_Load(object sender, EventArgs e)
        {
            controller.SparePartListController controller = new controller.SparePartListController(); //create controller object
            List<string> name = controller.getName("B");
            lblB1Name.Text += name.ElementAt(0);
            lblB2Name.Text += name.ElementAt(1);
            lblB3Name.Text += name.ElementAt(2);
            lblB4Name.Text += name.ElementAt(3);
            lblB5Name.Text += name.ElementAt(4);




            lblB1Category.Text += "B";
            lblB2Category.Text += "B";
            lblB3Category.Text += "B";
            lblB4Category.Text += "B";
            lblB5Category.Text += "B";

            List<string> num = controller.getNum("B");
            lblB1Num.Text += num.ElementAt(0);
            lblB2Num.Text += num.ElementAt(1);
            lblB3Num.Text += num.ElementAt(2);
            lblB4Num.Text += num.ElementAt(3);
            lblB5Num.Text += num.ElementAt(4);

            List<int> price = controller.getPrice("B");
            lblB1Price.Text += price.ElementAt(0).ToString();
            lblB2Price.Text += price.ElementAt(1).ToString();
            lblB3Price.Text += price.ElementAt(2).ToString();
            lblB4Price.Text += price.ElementAt(3).ToString();
            lblB5Price.Text += price.ElementAt(4).ToString();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategory.Text == "Category A") //if user want to view spare part category A
            {
                Form sparePartListA = new sparePartListA();
                this.Hide();
                //Swap the current form to another.
                sparePartListA.StartPosition = FormStartPosition.Manual;
                sparePartListA.Location = this.Location;
                sparePartListA.Size = this.Size;
                sparePartListA.ShowDialog();
                this.Close();
            }

            if (cmbCategory.Text == "Category C") //if user want to view spare part category C
            {
                Form sparePartListC = new sparePartListC();
                this.Hide();
                //Swap the current form to another.
                sparePartListC.StartPosition = FormStartPosition.Manual;
                sparePartListC.Location = this.Location;
                sparePartListC.Size = this.Size;
                sparePartListC.ShowDialog();
                this.Close();
            }

            if (cmbCategory.Text == "Category D") //if user want to view spare part category D
            {
                Form sparePartListD = new sparePartListD();
                this.Hide();
                //Swap the current form to another.
                sparePartListD.StartPosition = FormStartPosition.Manual;
                sparePartListD.Location = this.Location;
                sparePartListD.Size = this.Size;
                sparePartListD.ShowDialog();
                this.Close();
            }
        }
    }
}