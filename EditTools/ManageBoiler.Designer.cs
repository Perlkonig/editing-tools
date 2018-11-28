namespace EditTools
{
    partial class ManageBoiler
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
            this.dgv_Boiler = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_BoilerSave = new System.Windows.Forms.Button();
            this.btn_BoilerExport = new System.Windows.Forms.Button();
            this.btn_BoilerImport = new System.Windows.Forms.Button();
            this.Label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Boiler)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgv_Boiler, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.66666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.333334F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(977, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgv_Boiler
            // 
            this.dgv_Boiler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Boiler.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_Boiler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Boiler.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Label,
            this.Comment});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Boiler.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Boiler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Boiler.Location = new System.Drawing.Point(3, 23);
            this.dgv_Boiler.Name = "dgv_Boiler";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Boiler.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Boiler.Size = new System.Drawing.Size(971, 392);
            this.dgv_Boiler.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_BoilerSave);
            this.flowLayoutPanel1.Controls.Add(this.btn_BoilerExport);
            this.flowLayoutPanel1.Controls.Add(this.btn_BoilerImport);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 421);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(971, 26);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btn_BoilerSave
            // 
            this.btn_BoilerSave.Location = new System.Drawing.Point(893, 3);
            this.btn_BoilerSave.Name = "btn_BoilerSave";
            this.btn_BoilerSave.Size = new System.Drawing.Size(75, 23);
            this.btn_BoilerSave.TabIndex = 0;
            this.btn_BoilerSave.Text = "Save";
            this.btn_BoilerSave.UseVisualStyleBackColor = true;
            this.btn_BoilerSave.Click += new System.EventHandler(this.btn_BoilerSave_Click);
            // 
            // btn_BoilerExport
            // 
            this.btn_BoilerExport.Location = new System.Drawing.Point(812, 3);
            this.btn_BoilerExport.Name = "btn_BoilerExport";
            this.btn_BoilerExport.Size = new System.Drawing.Size(75, 23);
            this.btn_BoilerExport.TabIndex = 1;
            this.btn_BoilerExport.Text = "Export";
            this.btn_BoilerExport.UseVisualStyleBackColor = true;
            this.btn_BoilerExport.Click += new System.EventHandler(this.btn_BoilerExport_Click);
            // 
            // btn_BoilerImport
            // 
            this.btn_BoilerImport.Location = new System.Drawing.Point(731, 3);
            this.btn_BoilerImport.Name = "btn_BoilerImport";
            this.btn_BoilerImport.Size = new System.Drawing.Size(75, 23);
            this.btn_BoilerImport.TabIndex = 2;
            this.btn_BoilerImport.Text = "Import";
            this.btn_BoilerImport.UseVisualStyleBackColor = true;
            this.btn_BoilerImport.Click += new System.EventHandler(this.btn_BoilerImport_Click);
            // 
            // Label
            // 
            this.Label.FillWeight = 35F;
            this.Label.HeaderText = "Label";
            this.Label.Name = "Label";
            this.Label.ToolTipText = "The name of the comment that will appear in the drop-down menu";
            // 
            // Comment
            // 
            this.Comment.HeaderText = "Comment";
            this.Comment.Name = "Comment";
            this.Comment.ToolTipText = "The comment that will be inserted";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(971, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "To create mutli-line comments, use Shift+Enter. Row heights will adjust once you " +
    "finish editing the cell.";
            // 
            // ManageBoiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ManageBoiler";
            this.Text = "Manage Boilerplate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManageBoiler_FormClosing);
            this.Load += new System.EventHandler(this.ManageBoiler_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Boiler)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgv_Boiler;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn_BoilerSave;
        private System.Windows.Forms.Button btn_BoilerExport;
        private System.Windows.Forms.Button btn_BoilerImport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Label;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.Label label1;
    }
}