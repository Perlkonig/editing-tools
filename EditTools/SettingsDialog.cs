using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace EditTools
{
    public partial class SettingsDialog : Form
    {
        BindingSource bs = new BindingSource();

        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void SettingsDialog_Load(object sender, EventArgs e)
        {
        }

        private void SettingsDialog_Closing(object sender, FormClosingEventArgs e)
        {
        }

        private void btn_Close_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
