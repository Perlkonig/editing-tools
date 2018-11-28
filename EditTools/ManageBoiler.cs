using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditTools
{
    public partial class ManageBoiler : Form
    {
        public ManageBoiler()
        {
            InitializeComponent();
        }

        private void ManageBoiler_Load(object sender, EventArgs e)
        {
            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            dgv_Boiler.Rows.Clear();
            Boilerplate bp = Properties.Settings.Default.newboiler;

            if (bp != null)
            {
                foreach (KeyValuePair<string, Comment> boiler in bp.Dict)
                {
                    int idx = dgv_Boiler.Rows.Add();
                    DataGridViewRow row = dgv_Boiler.Rows[idx];
                    row.Cells["Label"].Value = boiler.Key;
                    row.Cells["Comment"].Value = boiler.Value.Text;
                }
            }

        }

        private void btn_BoilerSave_Click(object sender, EventArgs e)
        {
            Boilerplate bp = ConvertRows();
            Properties.Settings.Default.newboiler = bp;
            Properties.Settings.Default.Save();
        }

        private Boilerplate ConvertRows()
        {
            Boilerplate bp = new Boilerplate();
            foreach (DataGridViewRow row in dgv_Boiler.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                string key = "";
                if (row.Cells["Label"].Value == null)
                {
                    throw new InvalidOperationException("Each row must have a unique name.");
                }
                else
                {
                    key = row.Cells["Label"].Value.ToString();
                }
                Comment comment = new Comment();
                if ((row.Cells["Comment"].Value == null) || (String.IsNullOrEmpty(row.Cells["Comment"].Value.ToString())))
                {
                    throw new InvalidOperationException("Each row must have something in the 'Comment' field.");
                }
                else
                {
                    comment.Text = row.Cells["Comment"].Value.ToString();
                }
                bp.Dict.Add(key, comment);
            }
            return bp;
        }

        private void btn_BoilerExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = "json";
            sfd.CheckPathExists = true;
            sfd.Filter = "JSON files (*.json)|*.json";
            sfd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sfd.OverwritePrompt = true;
            sfd.Title = "Export Boilerplate";
            sfd.FileName = "boilerplate.json";
            //sfd.RestoreDirectory = false;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string serialized = JsonConvert.SerializeObject(Properties.Settings.Default.newboiler.Dict, Formatting.Indented);
                Debug.WriteLine(serialized);
                File.WriteAllText(sfd.FileName, serialized);
                MessageBox.Show("Boilerplate exported to " + sfd.FileName + ".");
            }
            else
            {
                MessageBox.Show("Export cancelled.");
            }
        }

        private void btn_BoilerImport_Click(object sender, EventArgs e)
        {
            Boilerplate bp = Properties.Settings.Default.newboiler;
            ImportDialog id = new ImportDialog();
            var result = id.ShowDialog();

            if (result == DialogResult.OK)
            {
                string injson = File.ReadAllText(id.GetFileName());
                Boilerplate inbp = new Boilerplate();
                inbp.Dict = JsonConvert.DeserializeObject<Dictionary<string,Comment>>(injson);

                switch (id.GetMode())
                {
                    case ImportDialog.Modes.MERGE:
                        foreach (var kvpair in inbp.Dict)
                        {
                            if (bp.Dict.ContainsKey(kvpair.Key))
                            {
                                bp.Dict[kvpair.Key] = kvpair.Value;
                            }
                            else
                            {
                                bp.Dict.Add(kvpair.Key, kvpair.Value);
                            }
                        }
                        break;
                    case ImportDialog.Modes.NEW:
                        foreach (var kvpair in inbp.Dict)
                        {
                            if (bp.Dict.ContainsKey(kvpair.Key))
                            {
                                continue;
                            }
                            else
                            {
                                bp.Dict.Add(kvpair.Key, kvpair.Value);
                            }
                        }
                        break;
                    case ImportDialog.Modes.REPLACE:
                        bp = inbp;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Unrecognized import mode. Aborting.");
                }

                Properties.Settings.Default.newboiler = bp;
                Properties.Settings.Default.Save();
                RefreshDisplay();
                MessageBox.Show("Import complete.");

            }
            else
            {
                MessageBox.Show("Import cancelled.");
            }
        }

        private void ManageBoiler_FormClosing(object sender, FormClosingEventArgs e)
        {
            Boilerplate bp = ConvertRows();
            Properties.Settings.Default.newboiler = bp;
            Properties.Settings.Default.Save();
        }
    }
}
