namespace EditTools
{
    partial class ManageSearches
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgv_Searches = new System.Windows.Forms.DataGridView();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatchCase = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Wildcards = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Find = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HandleReplace = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Replace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReplaceAll = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_SearchesSave = new System.Windows.Forms.Button();
            this.btn_SearchesExport = new System.Windows.Forms.Button();
            this.btn_SearchesImport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Searches)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgv_Searches, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.66666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.333333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(977, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgv_Searches
            // 
            this.dgv_Searches.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Searches.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgv_Searches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Searches.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Key,
            this.MatchCase,
            this.Wildcards,
            this.Find,
            this.HandleReplace,
            this.Replace,
            this.ReplaceAll});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Searches.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Searches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Searches.Location = new System.Drawing.Point(3, 23);
            this.dgv_Searches.Name = "dgv_Searches";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Searches.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Searches.Size = new System.Drawing.Size(971, 392);
            this.dgv_Searches.TabIndex = 1;
            this.dgv_Searches.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Searches_CellValueChanged);
            // 
            // Key
            // 
            this.Key.HeaderText = "Name";
            this.Key.Name = "Key";
            this.Key.ToolTipText = "The name of the search as it will appear in the drop-down menu";
            // 
            // MatchCase
            // 
            this.MatchCase.FillWeight = 35F;
            this.MatchCase.HeaderText = "Match Case?";
            this.MatchCase.Name = "MatchCase";
            this.MatchCase.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MatchCase.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.MatchCase.ToolTipText = "Is the search case sensitive?";
            // 
            // Wildcards
            // 
            this.Wildcards.FillWeight = 35F;
            this.Wildcards.HeaderText = "Wildcards?";
            this.Wildcards.Name = "Wildcards";
            this.Wildcards.ToolTipText = "Check if this search uses Word\'s wildcard features";
            // 
            // Find
            // 
            this.Find.HeaderText = "Find";
            this.Find.Name = "Find";
            this.Find.ToolTipText = "The text you\'re searching for";
            // 
            // HandleReplace
            // 
            this.HandleReplace.FillWeight = 35F;
            this.HandleReplace.HeaderText = "Replace?";
            this.HandleReplace.Name = "HandleReplace";
            this.HandleReplace.ToolTipText = "Check if you want to replace text";
            // 
            // Replace
            // 
            this.Replace.HeaderText = "Replace";
            this.Replace.Name = "Replace";
            this.Replace.ReadOnly = true;
            this.Replace.ToolTipText = "The text you want to replace the found text with";
            // 
            // ReplaceAll
            // 
            this.ReplaceAll.FillWeight = 35F;
            this.ReplaceAll.HeaderText = "Replace All?";
            this.ReplaceAll.Name = "ReplaceAll";
            this.ReplaceAll.ReadOnly = true;
            this.ReplaceAll.ToolTipText = "If checked, executing this search will replace all instances at once";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_SearchesSave);
            this.flowLayoutPanel1.Controls.Add(this.btn_SearchesExport);
            this.flowLayoutPanel1.Controls.Add(this.btn_SearchesImport);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 421);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(971, 26);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btn_SearchesSave
            // 
            this.btn_SearchesSave.Location = new System.Drawing.Point(893, 3);
            this.btn_SearchesSave.Name = "btn_SearchesSave";
            this.btn_SearchesSave.Size = new System.Drawing.Size(75, 23);
            this.btn_SearchesSave.TabIndex = 0;
            this.btn_SearchesSave.Text = "Save";
            this.btn_SearchesSave.UseVisualStyleBackColor = true;
            this.btn_SearchesSave.Click += new System.EventHandler(this.btn_SearchesSave_Click);
            // 
            // btn_SearchesExport
            // 
            this.btn_SearchesExport.Location = new System.Drawing.Point(812, 3);
            this.btn_SearchesExport.Name = "btn_SearchesExport";
            this.btn_SearchesExport.Size = new System.Drawing.Size(75, 23);
            this.btn_SearchesExport.TabIndex = 1;
            this.btn_SearchesExport.Text = "Export";
            this.btn_SearchesExport.UseVisualStyleBackColor = true;
            this.btn_SearchesExport.Click += new System.EventHandler(this.btn_SearchesExport_Click);
            // 
            // btn_SearchesImport
            // 
            this.btn_SearchesImport.Location = new System.Drawing.Point(731, 3);
            this.btn_SearchesImport.Name = "btn_SearchesImport";
            this.btn_SearchesImport.Size = new System.Drawing.Size(75, 23);
            this.btn_SearchesImport.TabIndex = 2;
            this.btn_SearchesImport.Text = "Import";
            this.btn_SearchesImport.UseVisualStyleBackColor = true;
            this.btn_SearchesImport.Click += new System.EventHandler(this.btn_SearchesImport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(971, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Hover over a column for more information.";
            // 
            // ManageSearches
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ManageSearches";
            this.Text = "Manage Searches";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManageSearches_FormClosing);
            this.Load += new System.EventHandler(this.ManageSearches_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Searches)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgv_Searches;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn_SearchesSave;
        private System.Windows.Forms.Button btn_SearchesExport;
        private System.Windows.Forms.Button btn_SearchesImport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MatchCase;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Wildcards;
        private System.Windows.Forms.DataGridViewTextBoxColumn Find;
        private System.Windows.Forms.DataGridViewCheckBoxColumn HandleReplace;
        private System.Windows.Forms.DataGridViewTextBoxColumn Replace;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ReplaceAll;
        private System.Windows.Forms.Label label1;
    }
}