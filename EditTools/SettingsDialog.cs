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
            dgv_Boilerplate.Columns.Add("col_Name", "Name");
            dgv_Boilerplate.Columns.Add("col_Value", "Value");
            dgv_Boilerplate.Columns["col_Value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv_Boilerplate.Columns["col_Value"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv_Boilerplate.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            StringCollection strings = Properties.Settings.Default.boilerplate;
            Dictionary<string, string> dict = new Dictionary<string, string>();

            for (int i = 0; i < strings.Count - 1; i+=2)
            {
                if ((strings[i] != null) && (strings[i + 1] != null))
                {
                    dict.Add(strings[i], strings[i + 1]);
                }
            }

            foreach (string key in dict.Keys)
            {
                string[] row = new string[] {key, dict[key]};
                dgv_Boilerplate.Rows.Add(row);
            }
        }

        private void SettingsDialog_Closing(object sender, FormClosingEventArgs e)
        {
            StringCollection strings = new StringCollection();

            foreach (DataGridViewRow row in dgv_Boilerplate.Rows)
            {
                if (((string)row.Cells[0].Value != null) && ((string)row.Cells[1].Value != null))
                {
                    strings.Add((string)row.Cells[0].Value);
                    strings.Add((string)row.Cells[1].Value);
                }
            }
            Properties.Settings.Default.boilerplate.Clear();
            Properties.Settings.Default.boilerplate = strings;

            Properties.Settings.Default.Save();

            Globals.Ribbons.Ribbon1.loadBoilerplate();
        }

        private void btn_Close_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
