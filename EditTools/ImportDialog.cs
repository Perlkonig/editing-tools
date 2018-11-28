using System;
using System.Collections.Generic;
using System.Xml;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace EditTools
{
    public partial class ImportDialog : Form
    {
        public enum Modes { REPLACE, MERGE, NEW };

        public ImportDialog()
        {
            InitializeComponent();
        }

        private void btn_ImportSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.DefaultExt = "json";
            ofd.Filter = "JSON files (*.json)|*.json|All files|*.*";
            ofd.Title = "Settings file to import";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txt_ImportFile.Text = ofd.FileName;
            }
        }

        public string GetFileName()
        {
            return txt_ImportFile.Text;
        }

        public Modes GetMode()
        {
            if (radio_Replace.Checked)
            {
                return Modes.REPLACE;
            }
            else if (radio_MergeReplace.Checked)
            {
                return Modes.MERGE;
            }
            else if (radio_MergeNew.Checked)
            {
                return Modes.NEW;
            }
            else
            {
                throw new InvalidOperationException("None of the radio buttons were selected. This should *never* happen!");
            }
        }
    }
}
