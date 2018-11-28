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
    public partial class ManageSearches : Form
    {
        public ManageSearches()
        {
            InitializeComponent();
        }

        private void ManageSearches_Load(object sender, EventArgs e)
        {
            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            dgv_Searches.Rows.Clear();
            Searches searches = Properties.Settings.Default.searches;

            if (searches != null)
            {
                foreach (KeyValuePair<string, Search> search in searches.Dict)
                {
                    int idx = dgv_Searches.Rows.Add();
                    DataGridViewRow row = dgv_Searches.Rows[idx];
                    row.Cells["Key"].Value = search.Key;
                    row.Cells["MatchCase"].Value = search.Value.CaseSensitive;
                    row.Cells["Wildcards"].Value = search.Value.Wildcards;
                    row.Cells["Find"].Value = search.Value.Find;
                    if (search.Value.Replace == null)
                    {
                        row.Cells["HandleReplace"].Value = false;
                    }
                    else
                    {
                        row.Cells["HandleReplace"].Value = true;
                        row.Cells["Replace"].Value = search.Value.Replace;
                        row.Cells["ReplaceAll"].Value = search.Value.ReplaceAll;
                    }
                }
            }

        }

        private void dgv_Searches_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ( (e.RowIndex >= 0) && (e.ColumnIndex == 4) )
            {
                bool ischecked = (bool)dgv_Searches.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (!ischecked)
                {
                    dgv_Searches.Rows[e.RowIndex].Cells["Replace"].Value = "";
                    dgv_Searches.Rows[e.RowIndex].Cells["Replace"].ReadOnly = true;
                    dgv_Searches.Rows[e.RowIndex].Cells["ReplaceAll"].Value = false;
                    dgv_Searches.Rows[e.RowIndex].Cells["ReplaceAll"].ReadOnly = true;
                }
                else
                {
                    dgv_Searches.Rows[e.RowIndex].Cells["Replace"].ReadOnly = false;
                    dgv_Searches.Rows[e.RowIndex].Cells["ReplaceAll"].ReadOnly = false;
                }
            }
        }

        private void btn_SearchesSave_Click(object sender, EventArgs e)
        {
            Searches searches = ConvertRows();
            Properties.Settings.Default.searches = searches;
            Properties.Settings.Default.Save();
        }

        private Searches ConvertRows()
        {
            Searches searches = new Searches();
            foreach (DataGridViewRow row in dgv_Searches.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                string key = "";
                if (row.Cells["Key"].Value == null)
                {
                    throw new InvalidOperationException("Each row must have a unique name.");
                }
                else
                {
                    key = row.Cells["Key"].Value.ToString();
                }
                Search search = new Search();
                if (row.Cells["MatchCase"].Value == null)
                {
                    search.CaseSensitive = false;
                }
                else
                {
                    search.CaseSensitive = (bool)row.Cells["MatchCase"].Value;
                }
                if (row.Cells["Wildcards"].Value == null)
                {
                    search.Wildcards = false;
                }
                else
                {
                    search.Wildcards = (bool)row.Cells["Wildcards"].Value;
                }
                if ((row.Cells["Find"].Value == null) || (String.IsNullOrEmpty(row.Cells["Find"].Value.ToString())))
                {
                    throw new InvalidOperationException("Each row must have something in the 'Find' column.");
                }
                else
                {
                    search.Find = row.Cells["Find"].Value.ToString();
                }
                bool replace = false;
                if (row.Cells["HandleReplace"].Value != null)
                {
                    replace = (bool)row.Cells["HandleReplace"].Value;
                }
                if (replace)
                {
                    if (row.Cells["Replace"].Value == null)
                    {
                        search.Replace = String.Empty;
                    }
                    else
                    {
                        search.Replace = row.Cells["Replace"].Value.ToString();
                    }
                    if (row.Cells["ReplaceAll"].Value == null)
                    {
                        search.ReplaceAll = false;
                    }
                    else
                    {
                        search.ReplaceAll = (bool)row.Cells["ReplaceAll"].Value;
                    }
                }
                searches.Dict.Add(key, search);
            }
            return searches;
        }

        private void btn_SearchesExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = "json";
            sfd.CheckPathExists = true;
            sfd.Filter = "JSON files (*.json)|*.json";
            sfd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sfd.OverwritePrompt = true;
            sfd.Title = "Export Saved Searches";
            sfd.FileName = "searches.json";
            //sfd.RestoreDirectory = false;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string serialized = JsonConvert.SerializeObject(Properties.Settings.Default.searches.Dict, Formatting.Indented);
                Debug.WriteLine(serialized);
                File.WriteAllText(sfd.FileName, serialized);
                MessageBox.Show("Saved searches exported to " + sfd.FileName + ".");
            }
            else
            {
                MessageBox.Show("Export cancelled.");
            }
        }

        private void btn_SearchesImport_Click(object sender, EventArgs e)
        {
            Searches searches = Properties.Settings.Default.searches;
            ImportDialog id = new ImportDialog();
            var result = id.ShowDialog();

            if (result == DialogResult.OK)
            {
                string injson = File.ReadAllText(id.GetFileName());
                Searches insearches = new Searches();
                insearches.Dict = JsonConvert.DeserializeObject<Dictionary<string,Search>>(injson);

                switch (id.GetMode())
                {
                    case ImportDialog.Modes.MERGE:
                        foreach (var kvpair in insearches.Dict)
                        {
                            if (searches.Dict.ContainsKey(kvpair.Key))
                            {
                                searches.Dict[kvpair.Key] = kvpair.Value;
                            }
                            else
                            {
                                searches.Dict.Add(kvpair.Key, kvpair.Value);
                            }
                        }
                        break;
                    case ImportDialog.Modes.NEW:
                        foreach (var kvpair in insearches.Dict)
                        {
                            if (searches.Dict.ContainsKey(kvpair.Key))
                            {
                                continue;
                            }
                            else
                            {
                                searches.Dict.Add(kvpair.Key, kvpair.Value);
                            }
                        }
                        break;
                    case ImportDialog.Modes.REPLACE:
                        searches = insearches;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Unrecognized import mode. Aborting.");
                }

                Properties.Settings.Default.searches = searches;
                Properties.Settings.Default.Save();
                RefreshDisplay();
                MessageBox.Show("Import complete.");

            }
            else
            {
                MessageBox.Show("Import cancelled.");
            }
        }

        private void ManageSearches_FormClosing(object sender, FormClosingEventArgs e)
        {
            Searches searches = ConvertRows();
            Properties.Settings.Default.searches = searches;
            Properties.Settings.Default.Save();
        }
    }
}
