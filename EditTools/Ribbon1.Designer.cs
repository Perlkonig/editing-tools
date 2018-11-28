namespace EditTools
{
    partial class Ribbon1 : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon1()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EditingTools = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.lbl_Version = this.Factory.CreateRibbonLabel();
            this.btn_Help = this.Factory.CreateRibbonButton();
            this.lbl_Spacer1 = this.Factory.CreateRibbonLabel();
            this.btn_Settings = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.dd_Langs = this.Factory.CreateRibbonDropDown();
            this.btn_FixLang = this.Factory.CreateRibbonButton();
            this.label1 = this.Factory.CreateRibbonLabel();
            this.eb_AuthorName = this.Factory.CreateRibbonEditBox();
            this.eb_AuthorInit = this.Factory.CreateRibbonEditBox();
            this.btn_ChangeOwner = this.Factory.CreateRibbonButton();
            this.btn_SingData = this.Factory.CreateRibbonButton();
            this.group3 = this.Factory.CreateRibbonGroup();
            this.btn_WordList = this.Factory.CreateRibbonButton();
            this.btn_WordFreq = this.Factory.CreateRibbonButton();
            this.btn_ProperNouns = this.Factory.CreateRibbonButton();
            this.edit_MinPhraseLen = this.Factory.CreateRibbonEditBox();
            this.edit_MaxPhraseLen = this.Factory.CreateRibbonEditBox();
            this.btn_PhraseFrequency = this.Factory.CreateRibbonButton();
            this.grp_Finishing = this.Factory.CreateRibbonGroup();
            this.btn_AcceptFormatting = this.Factory.CreateRibbonButton();
            this.btn_LinkText = this.Factory.CreateRibbonButton();
            this.group4 = this.Factory.CreateRibbonGroup();
            this.dd_Boilerplate = this.Factory.CreateRibbonDropDown();
            this.btn_ApplyBoilerplate = this.Factory.CreateRibbonButton();
            this.btn_ManageBoiler = this.Factory.CreateRibbonButton();
            this.group5 = this.Factory.CreateRibbonGroup();
            this.dd_Searches = this.Factory.CreateRibbonDropDown();
            this.btn_ExecuteSearch = this.Factory.CreateRibbonButton();
            this.btn_ManageSearches = this.Factory.CreateRibbonButton();
            this.EditingTools.SuspendLayout();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            this.group3.SuspendLayout();
            this.grp_Finishing.SuspendLayout();
            this.group4.SuspendLayout();
            this.group5.SuspendLayout();
            this.SuspendLayout();
            // 
            // EditingTools
            // 
            this.EditingTools.Groups.Add(this.group1);
            this.EditingTools.Groups.Add(this.group2);
            this.EditingTools.Groups.Add(this.group3);
            this.EditingTools.Groups.Add(this.grp_Finishing);
            this.EditingTools.Groups.Add(this.group4);
            this.EditingTools.Groups.Add(this.group5);
            this.EditingTools.Label = "Editing Tools";
            this.EditingTools.Name = "EditingTools";
            // 
            // group1
            // 
            this.group1.Items.Add(this.lbl_Version);
            this.group1.Items.Add(this.btn_Help);
            this.group1.Items.Add(this.lbl_Spacer1);
            this.group1.Items.Add(this.btn_Settings);
            this.group1.Label = "Settings";
            this.group1.Name = "group1";
            // 
            // lbl_Version
            // 
            this.lbl_Version.Label = "Version 2.0.0";
            this.lbl_Version.Name = "lbl_Version";
            // 
            // btn_Help
            // 
            this.btn_Help.Label = "Help!";
            this.btn_Help.Name = "btn_Help";
            this.btn_Help.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_Help_Click);
            // 
            // lbl_Spacer1
            // 
            this.lbl_Spacer1.Label = " ";
            this.lbl_Spacer1.Name = "lbl_Spacer1";
            // 
            // btn_Settings
            // 
            this.btn_Settings.Label = "Settings";
            this.btn_Settings.Name = "btn_Settings";
            this.btn_Settings.ScreenTip = "Add/remove/edit boilerplate and configure the edit distance for the proper noun c" +
    "hecker";
            this.btn_Settings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_Settings_Click);
            // 
            // group2
            // 
            this.group2.Items.Add(this.dd_Langs);
            this.group2.Items.Add(this.btn_FixLang);
            this.group2.Items.Add(this.label1);
            this.group2.Items.Add(this.eb_AuthorName);
            this.group2.Items.Add(this.eb_AuthorInit);
            this.group2.Items.Add(this.btn_ChangeOwner);
            this.group2.Items.Add(this.btn_SingData);
            this.group2.Label = "Editing";
            this.group2.Name = "group2";
            // 
            // dd_Langs
            // 
            this.dd_Langs.Label = "Languages";
            this.dd_Langs.Name = "dd_Langs";
            this.dd_Langs.ScreenTip = "Apply selected language to the entire  document, including notes, headers and foo" +
    "ters";
            this.dd_Langs.ShowLabel = false;
            this.dd_Langs.SizeString = "WWWWWWWWWWWWWWW";
            // 
            // btn_FixLang
            // 
            this.btn_FixLang.Label = "Apply Language";
            this.btn_FixLang.Name = "btn_FixLang";
            this.btn_FixLang.ScreenTip = "Apply selected language to the entire  document, including notes, headers and foo" +
    "ters";
            this.btn_FixLang.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_FixLang_Click);
            // 
            // label1
            // 
            this.label1.Label = " ";
            this.label1.Name = "label1";
            // 
            // eb_AuthorName
            // 
            this.eb_AuthorName.Label = "Author Name";
            this.eb_AuthorName.Name = "eb_AuthorName";
            this.eb_AuthorName.ScreenTip = "Full name of the author to own the comment";
            this.eb_AuthorName.SizeString = "WWWWWWWWWWWW";
            this.eb_AuthorName.Text = null;
            // 
            // eb_AuthorInit
            // 
            this.eb_AuthorInit.Label = "Author Initials";
            this.eb_AuthorInit.Name = "eb_AuthorInit";
            this.eb_AuthorInit.ScreenTip = "The initials of the author to own the comment";
            this.eb_AuthorInit.SizeString = "WWWWW";
            this.eb_AuthorInit.Text = null;
            // 
            // btn_ChangeOwner
            // 
            this.btn_ChangeOwner.Label = "Change Comment Owner";
            this.btn_ChangeOwner.Name = "btn_ChangeOwner";
            this.btn_ChangeOwner.ScreenTip = "Change the author name and initials assigned to the selected comment";
            this.btn_ChangeOwner.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_ChangeOwner_Click);
            // 
            // btn_SingData
            // 
            this.btn_SingData.Label = "";
            this.btn_SingData.Name = "btn_SingData";
            // 
            // group3
            // 
            this.group3.Items.Add(this.btn_WordList);
            this.group3.Items.Add(this.btn_WordFreq);
            this.group3.Items.Add(this.btn_ProperNouns);
            this.group3.Items.Add(this.edit_MinPhraseLen);
            this.group3.Items.Add(this.edit_MaxPhraseLen);
            this.group3.Items.Add(this.btn_PhraseFrequency);
            this.group3.Label = "Proofing";
            this.group3.Name = "group3";
            // 
            // btn_WordList
            // 
            this.btn_WordList.Label = "Word List";
            this.btn_WordList.Name = "btn_WordList";
            this.btn_WordList.ScreenTip = "Generate a list of all the unique words in the document";
            this.btn_WordList.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_WordList_Click);
            // 
            // btn_WordFreq
            // 
            this.btn_WordFreq.Label = "Word Frequency List";
            this.btn_WordFreq.Name = "btn_WordFreq";
            this.btn_WordFreq.ScreenTip = "List words in frequency order";
            this.btn_WordFreq.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_WordFreq_Click);
            // 
            // btn_ProperNouns
            // 
            this.btn_ProperNouns.Label = "Check Proper Nouns";
            this.btn_ProperNouns.Name = "btn_ProperNouns";
            this.btn_ProperNouns.ScreenTip = "Find proper nouns that sound alike or that differ by a configurable edit distance" +
    "";
            this.btn_ProperNouns.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_ProperNouns_Click);
            // 
            // edit_MinPhraseLen
            // 
            this.edit_MinPhraseLen.Label = "Min. Phrase Length";
            this.edit_MinPhraseLen.Name = "edit_MinPhraseLen";
            this.edit_MinPhraseLen.Text = null;
            // 
            // edit_MaxPhraseLen
            // 
            this.edit_MaxPhraseLen.Label = "Max. Phrase Length";
            this.edit_MaxPhraseLen.Name = "edit_MaxPhraseLen";
            this.edit_MaxPhraseLen.Text = null;
            // 
            // btn_PhraseFrequency
            // 
            this.btn_PhraseFrequency.Label = "Phrase Frequency List";
            this.btn_PhraseFrequency.Name = "btn_PhraseFrequency";
            this.btn_PhraseFrequency.ScreenTip = "List phrases of the given length in frequency order";
            this.btn_PhraseFrequency.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_PhraseFrequency_Click);
            // 
            // grp_Finishing
            // 
            this.grp_Finishing.Items.Add(this.btn_AcceptFormatting);
            this.grp_Finishing.Items.Add(this.btn_LinkText);
            this.grp_Finishing.Label = "Finishing";
            this.grp_Finishing.Name = "grp_Finishing";
            // 
            // btn_AcceptFormatting
            // 
            this.btn_AcceptFormatting.Label = "Accept Formatting Changes";
            this.btn_AcceptFormatting.Name = "btn_AcceptFormatting";
            this.btn_AcceptFormatting.ScreenTip = "Accept all formatting changes with one click";
            this.btn_AcceptFormatting.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_AcceptFormatting_Click);
            // 
            // btn_LinkText
            // 
            this.btn_LinkText.Label = "Link Text";
            this.btn_LinkText.Name = "btn_LinkText";
            this.btn_LinkText.ScreenTip = "Add hyperlinks to instances of the same text";
            this.btn_LinkText.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_LinkText_Click);
            // 
            // group4
            // 
            this.group4.Items.Add(this.dd_Boilerplate);
            this.group4.Items.Add(this.btn_ApplyBoilerplate);
            this.group4.Items.Add(this.btn_ManageBoiler);
            this.group4.Label = "Boilerplate";
            this.group4.Name = "group4";
            // 
            // dd_Boilerplate
            // 
            this.dd_Boilerplate.Label = "Comments";
            this.dd_Boilerplate.Name = "dd_Boilerplate";
            this.dd_Boilerplate.ScreenTip = "Use the \"Settings\" button to add/remove/edit boilerplate";
            this.dd_Boilerplate.ShowLabel = false;
            this.dd_Boilerplate.SizeString = "WWWWWWWWWWWWWWW";
            // 
            // btn_ApplyBoilerplate
            // 
            this.btn_ApplyBoilerplate.Label = "Apply Comment";
            this.btn_ApplyBoilerplate.Name = "btn_ApplyBoilerplate";
            this.btn_ApplyBoilerplate.ScreenTip = "Click to apply selected comment to the selected text; Use the \"Settings\" button t" +
    "o add/remove/edit boilerplate";
            this.btn_ApplyBoilerplate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_ApplyBoilerplate_Click);
            // 
            // btn_ManageBoiler
            // 
            this.btn_ManageBoiler.Label = "Manage Boilerplate";
            this.btn_ManageBoiler.Name = "btn_ManageBoiler";
            this.btn_ManageBoiler.ScreenTip = "Edit your saved comments";
            this.btn_ManageBoiler.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_ManageBoiler_Click);
            // 
            // group5
            // 
            this.group5.Items.Add(this.dd_Searches);
            this.group5.Items.Add(this.btn_ExecuteSearch);
            this.group5.Items.Add(this.btn_ManageSearches);
            this.group5.Label = "Search && Replace";
            this.group5.Name = "group5";
            // 
            // dd_Searches
            // 
            this.dd_Searches.Label = "Saved Searches";
            this.dd_Searches.Name = "dd_Searches";
            this.dd_Searches.ScreenTip = "Click the \"Manage\" button to change your saved searches";
            this.dd_Searches.ShowLabel = false;
            this.dd_Searches.SizeString = "WWWWWWWWWWWWWWW";
            // 
            // btn_ExecuteSearch
            // 
            this.btn_ExecuteSearch.Label = "Execute Search";
            this.btn_ExecuteSearch.Name = "btn_ExecuteSearch";
            this.btn_ExecuteSearch.ScreenTip = "Click to execute selected saved search";
            this.btn_ExecuteSearch.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_ExecuteSearch_Click);
            // 
            // btn_ManageSearches
            // 
            this.btn_ManageSearches.Label = "Manage Searches";
            this.btn_ManageSearches.Name = "btn_ManageSearches";
            this.btn_ManageSearches.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_ManageSearches_Click);
            // 
            // Ribbon1
            // 
            this.Name = "Ribbon1";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.EditingTools);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.EditingTools.ResumeLayout(false);
            this.EditingTools.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.grp_Finishing.ResumeLayout(false);
            this.grp_Finishing.PerformLayout();
            this.group4.ResumeLayout(false);
            this.group4.PerformLayout();
            this.group5.ResumeLayout(false);
            this.group5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab EditingTools;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_Settings;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group4;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_WordList;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_ProperNouns;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dd_Boilerplate;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_ApplyBoilerplate;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grp_Finishing;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_AcceptFormatting;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_WordFreq;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel lbl_Version;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_Help;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel lbl_Spacer1;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox edit_MinPhraseLen;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_PhraseFrequency;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox edit_MaxPhraseLen;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_LinkText;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dd_Langs;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_FixLang;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel label1;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox eb_AuthorName;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox eb_AuthorInit;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_ChangeOwner;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_SingData;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group5;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dd_Searches;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_ExecuteSearch;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_ManageSearches;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_ManageBoiler;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
