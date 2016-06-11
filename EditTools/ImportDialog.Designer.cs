namespace EditTools
{
    partial class ImportDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ImportFile = new System.Windows.Forms.TextBox();
            this.btn_ImportSelect = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Import = new System.Windows.Forms.Button();
            this.radio_Replace = new System.Windows.Forms.RadioButton();
            this.radio_MergeNew = new System.Windows.Forms.RadioButton();
            this.radio_MergeReplace = new System.Windows.Forms.RadioButton();
            this.grp_Duplicates = new System.Windows.Forms.GroupBox();
            this.grp_Duplicates.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Settings file to import";
            // 
            // txt_ImportFile
            // 
            this.txt_ImportFile.Location = new System.Drawing.Point(16, 30);
            this.txt_ImportFile.Name = "txt_ImportFile";
            this.txt_ImportFile.Size = new System.Drawing.Size(175, 20);
            this.txt_ImportFile.TabIndex = 1;
            // 
            // btn_ImportSelect
            // 
            this.btn_ImportSelect.Location = new System.Drawing.Point(197, 27);
            this.btn_ImportSelect.Name = "btn_ImportSelect";
            this.btn_ImportSelect.Size = new System.Drawing.Size(75, 23);
            this.btn_ImportSelect.TabIndex = 2;
            this.btn_ImportSelect.Text = "Select";
            this.btn_ImportSelect.UseVisualStyleBackColor = true;
            this.btn_ImportSelect.Click += new System.EventHandler(this.btn_ImportSelect_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Close.Location = new System.Drawing.Point(196, 195);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 3;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Import
            // 
            this.btn_Import.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Import.Location = new System.Drawing.Point(115, 195);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(75, 23);
            this.btn_Import.TabIndex = 4;
            this.btn_Import.Text = "Import";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // radio_Replace
            // 
            this.radio_Replace.AutoSize = true;
            this.radio_Replace.Location = new System.Drawing.Point(6, 26);
            this.radio_Replace.Name = "radio_Replace";
            this.radio_Replace.Size = new System.Drawing.Size(238, 17);
            this.radio_Replace.TabIndex = 5;
            this.radio_Replace.TabStop = true;
            this.radio_Replace.Text = "Replace your boilerplate with the selected file";
            this.radio_Replace.UseVisualStyleBackColor = true;
            // 
            // radio_MergeNew
            // 
            this.radio_MergeNew.AutoSize = true;
            this.radio_MergeNew.Checked = true;
            this.radio_MergeNew.Location = new System.Drawing.Point(6, 49);
            this.radio_MergeNew.Name = "radio_MergeNew";
            this.radio_MergeNew.Size = new System.Drawing.Size(127, 17);
            this.radio_MergeNew.TabIndex = 6;
            this.radio_MergeNew.TabStop = true;
            this.radio_MergeNew.Text = "Add new records only";
            this.radio_MergeNew.UseVisualStyleBackColor = true;
            // 
            // radio_MergeReplace
            // 
            this.radio_MergeReplace.AutoSize = true;
            this.radio_MergeReplace.Location = new System.Drawing.Point(6, 72);
            this.radio_MergeReplace.Name = "radio_MergeReplace";
            this.radio_MergeReplace.Size = new System.Drawing.Size(185, 17);
            this.radio_MergeReplace.TabIndex = 7;
            this.radio_MergeReplace.TabStop = true;
            this.radio_MergeReplace.Text = "Add new and overwrite duplicates";
            this.radio_MergeReplace.UseVisualStyleBackColor = true;
            // 
            // grp_Duplicates
            // 
            this.grp_Duplicates.Controls.Add(this.radio_Replace);
            this.grp_Duplicates.Controls.Add(this.radio_MergeReplace);
            this.grp_Duplicates.Controls.Add(this.radio_MergeNew);
            this.grp_Duplicates.Location = new System.Drawing.Point(16, 72);
            this.grp_Duplicates.Name = "grp_Duplicates";
            this.grp_Duplicates.Size = new System.Drawing.Size(255, 100);
            this.grp_Duplicates.TabIndex = 8;
            this.grp_Duplicates.TabStop = false;
            this.grp_Duplicates.Text = "Duplicate Handling";
            // 
            // ImportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 229);
            this.Controls.Add(this.grp_Duplicates);
            this.Controls.Add(this.btn_Import);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_ImportSelect);
            this.Controls.Add(this.txt_ImportFile);
            this.Controls.Add(this.label1);
            this.Name = "ImportDialog";
            this.Text = "Import Boilerplate";
            this.grp_Duplicates.ResumeLayout(false);
            this.grp_Duplicates.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ImportFile;
        private System.Windows.Forms.Button btn_ImportSelect;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.RadioButton radio_Replace;
        private System.Windows.Forms.RadioButton radio_MergeNew;
        private System.Windows.Forms.RadioButton radio_MergeReplace;
        private System.Windows.Forms.GroupBox grp_Duplicates;
    }
}