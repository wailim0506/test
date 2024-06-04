﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1
{
    public partial class giveFeedback : Form
    {
        private string uName, UID;
        controller.accountController accountController;
        controller.UIController UIController;
        public giveFeedback()
        {
            InitializeComponent();
        }

        public giveFeedback(controller.accountController accountController, controller.UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            UID = accountController.getUID();
            lblUid.Text = $"Uid: {UID}";
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string feedback = tbFB.Text.ToString();
            int wordCount = CountWords(feedback);
            if (wordCount > 100)
            {
                MessageBox.Show("Word Amount Exceed.","Too Many Words", MessageBoxButtons.OK, MessageBoxIcon.Error); //alert the user not exceed word count 
            }else if(wordCount <= 0)
            {
                MessageBox.Show("Please enter something","Empty Feedback", MessageBoxButtons.OK, MessageBoxIcon.Error); //alert the user the textbox is empty
            }
            else
            {
                controller.feedbackController controller = new controller.feedbackController(); //create controller object
                Boolean addFeedback = controller.addFeedback("LMC00001",feedback);
                if (addFeedback == true)
                {
                    tbFB.Text = "";
                    lblWordCount.Text = $"Word Count: 0";
                    MessageBox.Show("Feedback Sent.");
                }
            }        
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbFB.Text = "";
            lblWordCount.Text = $"Word Count: 0";
        }


        private void tbFB_TextChanged(object sender, EventArgs e) //show word count instantly after typeing a new word
        {
            string feedback = tbFB.Text.ToString();
            int wordCount = CountWords(feedback);
            lblWordCount.Text = $"Word Count: {wordCount}";
        }

        public static int CountWords(string text)
        {
            // Check if the input text is null or empty
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0;
            }

            // Split the text into words based on delimiters
            string[] words = text.Split(new char[] { ' ', '\t', '\n', '\r', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            // Return the number of words
            return words.Length;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss"); //timer
        }

        private void giveFeedback_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true; //timer
            //lblUid.Text = $"Uid: {accountController.getUID()}";  //not linked yet
        }
    }
}
