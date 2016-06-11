using System;
using System.Collections.Generic;
using System.Xml;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace EditTools
{
    public partial class ImportDialog : Form
    {
        public ImportDialog()
        {
            InitializeComponent();
        }

        private void btn_ImportSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.DefaultExt = "xml";
            ofd.Filter = "XML files (*.xml)|*.xml|All files|*.*";
            ofd.Title = "Settings file to import";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txt_ImportFile.Text = ofd.FileName;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            StringCollection sc = Properties.Settings.Default.boilerplate;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < sc.Count - 1; i += 2)
            {
                dict.Add(sc[i], sc[i + 1]);
            }
            if (radio_Replace.Checked == true)
            {
                dict = new Dictionary<string, string>();
            }

            using (XmlReader reader = XmlReader.Create(txt_ImportFile.Text))
            {
                while (reader.Read())
                {
                    if ((reader.IsStartElement()) && (reader.Name == "entry"))
                    {
                        string key = reader["key"];
                        string value = "";
                        if (reader.Read())
                        {
                            value = reader.Value.Trim();
                        }
                        if (dict.ContainsKey(key))
                        {
                            if (radio_MergeReplace.Checked == true)
                            {
                                dict[key] = value;
                            }
                        }
                        else
                        {
                            dict.Add(key, value);
                        }
                    }
                }
            }

            StringCollection newsc = new StringCollection();
            foreach (string key in dict.Keys)
            {
                newsc.Add(key);
                newsc.Add(dict[key]);
            }
            Properties.Settings.Default.boilerplate = newsc;
            Properties.Settings.Default.Save();
            MessageBox.Show("Import complete.");
        }
    }
}
