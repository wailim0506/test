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
    public partial class sparePartListC : Form
    {
        public sparePartListC()
        {
            InitializeComponent();
        }

        private void sparePartListC_Load(object sender, EventArgs e)
        {
            controller.SparePartListController controller = new controller.SparePartListController(); //create controller object
            List<string> name = controller.getName("C");
            lblC1Name.Text += name.ElementAt(0);
            lblC2Name.Text += name.ElementAt(1);
            lblC3Name.Text += name.ElementAt(2);
            lblC4Name.Text += name.ElementAt(3);
            lblC5Name.Text += name.ElementAt(4);




            lblC1Category.Text += "C";
            lblC2Category.Text += "C";
            lblC3Category.Text += "C";
            lblC4Category.Text += "C";
            lblC5Category.Text += "C";

            List<string> num = controller.getNum("C");
            lblC1Num.Text += num.ElementAt(0);
            lblC2Num.Text += num.ElementAt(1);
            lblC3Num.Text += num.ElementAt(2);
            lblC4Num.Text += num.ElementAt(3);
            lblC5Num.Text += num.ElementAt(4);

            List<int> price = controller.getPrice("C");
            lblC1Price.Text += price.ElementAt(0).ToString();
            lblC2Price.Text += price.ElementAt(1).ToString();
            lblC3Price.Text += price.ElementAt(2).ToString();
            lblC4Price.Text += price.ElementAt(3).ToString();
            lblC5Price.Text += price.ElementAt(4).ToString();

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

            if (cmbCategory.Text == "Category B") //if user want to view spare part category B
            {
                Form sparePartListB = new sparePartListB();
                this.Hide();
                //Swap the current form to another.
                sparePartListB.StartPosition = FormStartPosition.Manual;
                sparePartListB.Location = this.Location;
                sparePartListB.Size = this.Size;
                sparePartListB.ShowDialog();
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