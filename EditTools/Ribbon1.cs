using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Xml;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;

namespace EditTools
{
    public partial class Ribbon1
    {

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            loadBoilerplate();

            //Languages
            Word.Languages langs = Globals.ThisAddIn.Application.Languages;
            List<Tuple<string, Word.Language>> langlist = new List<Tuple<string, Word.Language>>();
            foreach (Word.Language lang in langs)
            {
                langlist.Add(new Tuple<string, Word.Language>(lang.NameLocal, lang));
            }
            langlist.Sort((x,y) => x.Item1.CompareTo(y.Item1));
            foreach (Tuple<string, Word.Language> lang in langlist)
            {
                RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                item.Label = lang.Item1;
                item.Tag = lang.Item2.ID;
                dd_Langs.Items.Add(item);
                if (lang.Item1 == Properties.Settings.Default.lastlang)
                {
                    dd_Langs.SelectedItem = item;
                }
            }
            uint minlen = Properties.Settings.Default.minphraselen;
            uint maxlen = Properties.Settings.Default.maxphraselen;
            edit_MinPhraseLen.Text = minlen.ToString();
            edit_MaxPhraseLen.Text = maxlen.ToString();
        }

        public void loadBoilerplate()
        {
            //Boilerplate
            dd_Boilerplate.Items.Clear();
            StringCollection bps = Properties.Settings.Default.boilerplate;
            List<string> keys = new List<string>(); ;
            for (int i = 0; i < bps.Count; i++)
            {
                if (i % 2 == 0)
                {
                    keys.Add(bps[i]);
                }
            }
            keys.Sort();
            foreach (string key in keys)
            {
                RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                item.Label = key;
                dd_Boilerplate.Items.Add(item);
            }
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            ImportDialog id = new ImportDialog();
            id.ShowDialog();
            loadBoilerplate();
        }

        private void btn_Settings_Click(object sender, RibbonControlEventArgs e)
        {
            SettingsDialog win = new SettingsDialog();
            win.ShowDialog();
        }

        private void btn_FixLang_Click(object sender, RibbonControlEventArgs e)
        {
            Word.Document doc = Globals.ThisAddIn.Application.ActiveDocument;

            RibbonDropDownItem item = dd_Langs.SelectedItem;
            Properties.Settings.Default.lastlang = item.Label;
            Properties.Settings.Default.Save();
            foreach (Word.Range rng in TextHelpers.GetText(doc))
            {
                rng.LanguageID = (Word.WdLanguageID) item.Tag;
                rng.NoProofing = 0;
            }

            MessageBox.Show("All text marked as '"+ item.Label +"'.");
        }

        private void btn_Export_Click(object sender, RibbonControlEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = "xml";
            sfd.CheckPathExists = true;
            sfd.Filter = "XML files (*.xml)|*.xml";
            sfd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sfd.OverwritePrompt = true;
            sfd.Title = "Export Boilerplate Text";
            sfd.FileName = "";
            //sfd.RestoreDirectory = false;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (XmlWriter writer = XmlWriter.Create(sfd.FileName))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("boilerplate");
                    StringCollection sc = Properties.Settings.Default.boilerplate;
                    for (int i = 0; i < sc.Count - 1; i += 2)
                    {
                        writer.WriteStartElement("entry");
                        writer.WriteAttributeString("key", sc[i]);
                        writer.WriteString(sc[i + 1]);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                MessageBox.Show("Settings exported to " + sfd.FileName + ".");
            }
            else
            {
                MessageBox.Show("Export cancelled.");
            }
        }

        private void btn_WordList_Click(object sender, RibbonControlEventArgs e)
        {
            Word.Document doc = Globals.ThisAddIn.Application.ActiveDocument;
            HashSet<string> wordlist = new HashSet<string>();

            foreach (Word.Range rng in TextHelpers.GetText(doc))
            {
                string txt = rng.Text;

                //strip punctuation
                txt = TextHelpers.StripPunctuation(txt);

                //get word list
                HashSet<string> newwords = TextHelpers.ToWords(txt);
                wordlist.UnionWith(newwords);
            }

            //strip words that are all numbers
            wordlist = TextHelpers.RemoveNumbers(wordlist);

            //Create new document
            Word.Document newdoc = Globals.ThisAddIn.Application.Documents.Add();
            Word.Paragraph pgraph;

            //Intro text
            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.set_Style(newdoc.Styles["Heading 1"]);
            pgraph.Range.Text = "Word List\n";
            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.set_Style(newdoc.Styles["Normal"]);
            pgraph.Range.Text = "This is a proofreading tool. It takes every word in the document, strips the punctuation, removes words that consist only of numbers, and then presents them all in alphabetical order. This is a great way to find typos and inconsistencies.\n";
            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.Range.Text = "Capitalization is retained as is. That means that words that appear at the beginning of a sentence will appear capitalized.\n";

            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.Range.InsertBreak(Word.WdBreakType.wdSectionBreakContinuous);
            Word.Section sec = newdoc.Sections[2];
            sec.PageSetup.TextColumns.SetCount(3);

            string[] words = wordlist.ToArray();
            Array.Sort(words);
            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.Range.Text = string.Join("\n", words) + "\n";

            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.Range.InsertBreak(Word.WdBreakType.wdSectionBreakContinuous);
            newdoc.GrammarChecked = true;
        }

        private void btn_ProperNouns_Click(object sender, RibbonControlEventArgs e)
        {
            Word.Document doc = Globals.ThisAddIn.Application.ActiveDocument;
            HashSet<string> wordlist = new HashSet<string>();

            foreach (Word.Range rng in TextHelpers.GetText(doc))
            {
                string txt = rng.Text;
                Word.Style style = rng.get_Style();
                if (style != null)
                {
                    Regex re_heading = new Regex(@"(?i)(heading|title|date|toc)");
                    Match m = re_heading.Match(style.NameLocal);
                    if (m.Success)
                    {
                        continue;
                    }
                }

                HashSet<string> propers = new HashSet<string>();
                propers = TextHelpers.ProperNouns(txt);
                wordlist.UnionWith(propers);
            }

            //Produce the groupings
            HashSet<string> capped = TextHelpers.KeepCaps(wordlist);

            //DoubleMetaphone
            Dictionary<ushort, List<string>> mpgroups = new Dictionary<ushort, List<string>>();
            //Dictionary<string, List<string>> mpgroups = new Dictionary<string, List<string>>();
            ShortDoubleMetaphone sdm = new ShortDoubleMetaphone();
            //HashSet<string> tested = new HashSet<string>();

            foreach (string word in capped)
            {
                /*
                if (tested.Contains(word))
                {
                    continue;
                }
                else
                {
                    tested.Add(word);
                }
                */
                sdm.computeKeys(word);
                ushort pri = sdm.PrimaryShortKey;
                ushort alt = sdm.AlternateShortKey;
                if (mpgroups.ContainsKey(pri))
                {
                    mpgroups[pri].Add(word);
                }
                else
                {
                    List<string> node = new List<string>();
                    node.Add(word);
                    mpgroups[pri] = node;
                }
                if (mpgroups.ContainsKey(alt))
                {
                    mpgroups[alt].Add(word);
                }
                else
                {
                    List<string> node = new List<string>();
                    node.Add(word);
                    mpgroups[alt] = node;
                }
            }

            //Edit Distance
            List<string> dtested = new List<string>();
            int mindist;
            int.TryParse(Properties.Settings.Default.mindist, out mindist);
            if (mindist == 0)
            {
                mindist = 2;
            }
            Dictionary<string, List<string>> distgroups = new Dictionary<string, List<string>>();

            foreach (string word1 in capped)
            {
                if (dtested.Contains(word1))
                {
                    continue;
                }
                else
                {
                    dtested.Add(word1);
                }

                if (word1.Length <= mindist)
                {
                    continue;
                }

                foreach (string word2 in capped)
                {
                    if (word2.Length <= mindist)
                    {
                        continue;
                    }
                    int dist = TextHelpers.EditDistance(word1, word2);
                    //int percent = (int)Math.Round((dist / word1.Length) * 100.0);
                    if ((dist > 0) && (dist <= mindist))
                    //if ((dist > 0) && (percent <= distpercent))
                    {
                        dtested.Add(word2);
                        if (distgroups.ContainsKey(word1))
                        {
                            distgroups[word1].Add(word2);
                        }
                        else
                        {
                            List<string> node = new List<string>();
                            node.Add(word2);
                            distgroups[word1] = node;
                        }
                    }
                }
            }

            //Create new document
            Word.Document newdoc = Globals.ThisAddIn.Application.Documents.Add();
            Word.View view = Globals.ThisAddIn.Application.ActiveWindow.View;
            view.DisplayPageBoundaries = false;
            Word.Paragraph pgraph;

            //Intro text
            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.set_Style(newdoc.Styles["Heading 1"]);
            pgraph.Range.Text = "Proper Noun Checker\n";
            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.set_Style(newdoc.Styles["Normal"]);
            pgraph.Range.Text = "This tool only looks at words that start with a capital letter. It then uses phonetic comparison and edit distance to find other proper nouns that are similar. Words in all caps (acronyms) are not included.\n";
            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.Range.Text = "The system tries to ignore words at the beginning of sentences and in headers. This means some errors may go unseen, so use multiple tools!\n";
            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.Range.Text = "Most of what you see here are false positives! That's unavoidable. But it still catches certain otherwise-hard-to-find misspellings.\n";

            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.Range.InsertBreak(Word.WdBreakType.wdSectionBreakContinuous);
            Word.Section sec = newdoc.Sections[2];
            sec.PageSetup.TextColumns.SetCount(2);
            sec.PageSetup.TextColumns.LineBetween = -1;

            //Distance
            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.set_Style(newdoc.Styles["Heading 2"]);
            //pgraph.KeepWithNext = 0;
            pgraph.Range.Text = "Edit Distance (" + mindist + ")\n";

            foreach (string key in distgroups.Keys)
            {
                List<string> group = distgroups[key];
                pgraph = newdoc.Content.Paragraphs.Add();
                pgraph.set_Style(newdoc.Styles["Normal"]);
                pgraph.Range.Text = key + ", " + string.Join(", ", group) + "\n";

            }

            pgraph.Range.InsertBreak(Word.WdBreakType.wdPageBreak);
            //pgraph = newdoc.Content.Paragraphs.Add();
            //pgraph.Range.InsertBreak(Word.WdBreakType.wdSectionBreakContinuous);
            //Word.InlineShape line = pgraph.Range.InlineShapes.AddHorizontalLineStandard();
            //line.Height = 2;
            //line.Fill.Solid();
            //line.HorizontalLineFormat.NoShade = true;
            //line.Fill.ForeColor.RGB = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            //line.HorizontalLineFormat.PercentWidth = 90;
            //line.HorizontalLineFormat.Alignment = WdHorizontalLineAlignment.wdHorizontalLineAlignCenter;
            //sec = newdoc.Sections[3];
            //sec.PageSetup.TextColumns.SetCount(2);
            //sec.PageSetup.TextColumns.LineBetween = -1;

            //Metaphone
            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.set_Style(newdoc.Styles["Heading 2"]);
            //pgraph.KeepWithNext = 0;
            pgraph.Range.Text = "Phonetic Comparisons\n";

            foreach (ushort key in mpgroups.Keys)
            {
                if (key == 65535)
                {
                    continue;
                }
                List<string> group = mpgroups[key];
                if (group.Count > 1)
                {
                    pgraph = newdoc.Content.Paragraphs.Add();
                    pgraph.set_Style(newdoc.Styles["Normal"]);
                    pgraph.Range.Text = string.Join(", ", group) + "\n";
                }
            }

            //pgraph = newdoc.Content.Paragraphs.Add();
            //pgraph.Range.InsertBreak(Word.WdBreakType.wdSectionBreakContinuous);
            newdoc.GrammarChecked = true;
        }

        private void btn_ApplyBoilerplate_Click(object sender, RibbonControlEventArgs e)
        {
            //get the text from the settings
            StringCollection bps = Properties.Settings.Default.boilerplate;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < bps.Count - 1; i+=2)
            {
                dict.Add(bps[i], bps[i + 1]);
            }

            //Find out which option is selected
            RibbonDropDownItem item = this.dd_Boilerplate.SelectedItem;
            if (dict.ContainsKey(item.Label))
            {
                Word.Document doc = Globals.ThisAddIn.Application.ActiveDocument;
                Word.Selection selection = Globals.ThisAddIn.Application.Selection;
                if (selection != null && selection.Range != null)
                {
                    selection.Comments.Add(selection.Range, dict[item.Label]);
                }
                else
                {
                    MessageBox.Show("Could not find any selected text or valid insertion point. Please let Aaron know!");
                }
            }
            else
            {
                MessageBox.Show("An error occurred when finding your boilerplate. Please let Aaron know!");
                return;
            }
        }

        private void btn_AcceptFormatting_Click(object sender, RibbonControlEventArgs e)
        {
            Word.Document doc = Globals.ThisAddIn.Application.ActiveDocument;
            Word.Window win = Globals.ThisAddIn.Application.ActiveWindow;
            Word.View view = win.View;

            view.ShowComments = false;
            view.ShowInkAnnotations = false;
            view.ShowInsertionsAndDeletions = false;

            doc.AcceptAllRevisionsShown();

            view.ShowComments = true;
            view.ShowInkAnnotations = true;
            view.ShowInsertionsAndDeletions = true;
            MessageBox.Show("Formatting changes have been accepted.");
        }

        private void btn_WordFreq_Click(object sender, RibbonControlEventArgs e)
        {
            ProgressDialog d = new ProgressDialog();
            d.Show();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            Word.Document doc = Globals.ThisAddIn.Application.ActiveDocument;
            Dictionary<string, uint> wordlist = new Dictionary<string, uint>();
            Regex re_allnums = new Regex(@"^\d+$");

            IEnumerable<Word.Range> textranges = TextHelpers.GetText(doc);
            d.pbMax = textranges.Count();
            d.pbVal = 0;
            foreach (Word.Range rng in textranges)
            {
                d.pbVal++;
                string txt = rng.Text;

                //strip punctuation
                txt = TextHelpers.StripPunctuation(txt);


                string[] substrs = Regex.Split(txt, @"\s+");
                foreach (string word in substrs)
                {
                    Match m = re_allnums.Match(word);
                    if (!m.Success)
                    {
                        if (word.Trim() != "")
                        {
                            if (wordlist.ContainsKey(word))
                            {
                                wordlist[word]++;
                            }
                            else
                            {
                                wordlist.Add(word, 1);
                            }
                        }
                    }

                }
            }
            Debug.WriteLine("Counts tabulated. Time elapsed: " + watch.Elapsed.ToString());
            watch.Restart();

            //Create new document
            Word.Document newdoc = Globals.ThisAddIn.Application.Documents.Add();
            Word.Paragraph pgraph;

            //Intro text
            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.set_Style(newdoc.Styles["Heading 1"]);
            pgraph.Range.Text = "Word Frequency List\n";
            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.set_Style(newdoc.Styles["Normal"]);
            pgraph.Range.Text = "Capitalization is retained as is. That means that words that appear at the beginning of a sentence will appear capitalized. Don't forget that you can sort the table!\n";
            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.set_Style(newdoc.Styles["Normal"]);
            pgraph.Range.Text = "Total words found (case sensitive): "+ wordlist.Count.ToString() +"\n";

            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.Range.InsertBreak(Word.WdBreakType.wdSectionBreakContinuous);
            Word.Section sec = newdoc.Sections[2];
            sec.PageSetup.TextColumns.SetCount(3);

            var words = wordlist.ToList();
            words.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            newdoc.Tables.Add(pgraph.Range, words.Count, 2);
            //newdoc.Tables.Add(pgraph.Range, 1, 2);
            newdoc.Tables[1].AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
            newdoc.Tables[1].AllowAutoFit = true;
            d.pbMax = words.Count;
            d.pbVal = 0;
            int row = 1;
            foreach (var pair in words)
            {
                d.pbVal++;
                //newdoc.Tables[1].Rows.Add();
                Word.Cell cell = newdoc.Tables[1].Cell(row, 1);
                cell.Range.Text = pair.Key;
                cell = newdoc.Tables[1].Cell(row, 2);
                cell.Range.Text = pair.Value.ToString();
                row++;
            }

            pgraph = newdoc.Content.Paragraphs.Add();
            pgraph.Range.InsertBreak(Word.WdBreakType.wdSectionBreakContinuous);
            newdoc.GrammarChecked = true;
            Debug.WriteLine("All done. Time elapsed: " + watch.Elapsed.ToString());
            watch.Stop();
            d.Hide();
        }

        private void btn_Help_Click(object sender, RibbonControlEventArgs e)
        {
            System.Reflection.Assembly assemblyInfo = System.Reflection.Assembly.GetExecutingAssembly();
            string assemblyLocation = assemblyInfo.Location;
            Debug.WriteLine(assemblyLocation);

            //CodeBase is the location of the ClickOnce deployment files
            Uri uriCodeBase = new Uri(assemblyInfo.CodeBase);
            string ClickOnceLocation = Path.GetDirectoryName(uriCodeBase.LocalPath.ToString());
            Debug.WriteLine(ClickOnceLocation);

            string loc = ClickOnceLocation + Path.DirectorySeparatorChar + "help.html";
            System.Diagnostics.Process.Start(@"file:///" + loc);
        }

        private void btn_PhraseFrequency_Click(object sender, RibbonControlEventArgs e)
        {
            Word.Document doc = Globals.ThisAddIn.Application.ActiveDocument;
            uint newminlen;
            uint newmaxlen;
            UInt32.TryParse(edit_MinPhraseLen.Text, out newminlen);
            UInt32.TryParse(edit_MaxPhraseLen.Text, out newmaxlen);
            if ( (newminlen != 0) && (newmaxlen != 0) && (newminlen <= newmaxlen) )
            {
                Properties.Settings.Default.minphraselen = newminlen;
                Properties.Settings.Default.maxphraselen = newmaxlen;
                Properties.Settings.Default.Save();

                Dictionary<string, uint> phrases = new Dictionary<string, uint>();
                //Iterate through all text
                foreach (Word.Range rng in TextHelpers.GetText(doc))
                {
                    //Break into sentences
                    foreach (Word.Range sentence in rng.Sentences)
                    {
                        //Strip punctuation
                        string nopunc = TextHelpers.StripPunctuation(sentence.Text);
                        nopunc = nopunc.Replace("  ", " ");
                        //Break into words
                        string[] words = nopunc.Split(' ');
                        //Extract phrases
                        for (uint i = newminlen; i <= newmaxlen; i++)
                        {
                            for (int start=0; start < words.Length-i; start++)
                            {
                                List<string> phraselst = new List<string>();
                                for (int idx = 0; idx < i; idx++)
                                {
                                    phraselst.Add(words[start + idx]);
                                }
                                string phrase = string.Join(" ", phraselst).ToLower();
                                //Add to data structre
                                if (phrases.ContainsKey(phrase))
                                {
                                    phrases[phrase]++;
                                } else
                                {
                                    phrases[phrase] = 1;
                                }
                            }
                        }
                    }
                }

                //Display results

                //Create new document
                Word.Document newdoc = Globals.ThisAddIn.Application.Documents.Add();
                Word.Paragraph pgraph;

                //Intro text
                pgraph = newdoc.Content.Paragraphs.Add();
                pgraph.set_Style(newdoc.Styles["Heading 1"]);
                pgraph.Range.Text = "Phrase Frequency List\n";
                pgraph = newdoc.Content.Paragraphs.Add();
                pgraph.set_Style(newdoc.Styles["Normal"]);
                pgraph.Range.Text = "Punctuation (other than apostrophes) has been removed. All words have been lowercased for comparison.\n";

                pgraph = newdoc.Content.Paragraphs.Add();
                pgraph.Range.InsertBreak(Word.WdBreakType.wdSectionBreakContinuous);
                Word.Section sec = newdoc.Sections[2];
                sec.PageSetup.TextColumns.SetCount(2);

                var phraselist = phrases.Where(x => x.Value > 1).ToList();
                phraselist.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
                foreach (var pair in phraselist)
                {
                    pgraph = newdoc.Content.Paragraphs.Add();
                    pgraph.set_Style(newdoc.Styles["Normal"]);
                    pgraph.Range.Text = pair.Key + "\t" + pair.Value.ToString() + "\n";
                }

                pgraph = newdoc.Content.Paragraphs.Add();
                pgraph.Range.InsertBreak(Word.WdBreakType.wdSectionBreakContinuous);
                newdoc.GrammarChecked = true;
            }
            else
            {
                MessageBox.Show("The phrase length fields must contain numbers greater than zero, and the minimum length must be less than or equal to the maximum length.");
            }
        }

        private void btn_LinkText_Click(object sender, RibbonControlEventArgs e)
        {
            Word.Hyperlinks myLinks = Globals.ThisAddIn.Application.ActiveDocument.Hyperlinks;
            string anchor = InputDialog.ShowDialog("What text do you want linked (case sensitive)?", "Link Text");
            if (!String.IsNullOrEmpty(anchor))
            {
                string link = InputDialog.ShowDialog("Where do you want the link to point (leave blank to remove link)?", "Link Text");
                object findText = anchor;
                Globals.ThisAddIn.Application.Selection.Find.Execute(findText, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true, Word.WdFindWrap.wdFindAsk, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                if (Globals.ThisAddIn.Application.Selection.Find.Found)
                {
                    while (Globals.ThisAddIn.Application.Selection.Find.Found)
                    {
                        if (String.IsNullOrEmpty(link))
                        {
                            if (Globals.ThisAddIn.Application.Selection.Hyperlinks.Count > 0)
                            {
                                DialogResult result = MessageBox.Show("You entered a blank link. Do you wish to remove existing links? Click Yes to continue. Click No to ignore this one instance. Click Cancel to abort the whole linking process.", "Link Text: Confirm Removal", MessageBoxButtons.YesNoCancel);
                                if (result == DialogResult.Yes)
                                {
                                    Debug.WriteLine("Removing link from the anchor '" + anchor + "'.");
                                    foreach (Word.Hyperlink objLink in Globals.ThisAddIn.Application.Selection.Hyperlinks)
                                    {
                                        objLink.Delete();
                                    }
                                }
                                else if (result == DialogResult.No)
                                {
                                    Debug.WriteLine("Skipping this instance of the anchor.");
                                }
                                else
                                {
                                    Debug.WriteLine("Aborting!");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (Globals.ThisAddIn.Application.Selection.Hyperlinks.Count > 0)
                            {
                                DialogResult result = MessageBox.Show("This instance of the text is already linked. Do you want to update this link to '" + link + "'? Click Yes to proceed. Click No to skip this one instance. Click Cancel to abort the whole linking process.", "Link Text: Confirm Link", MessageBoxButtons.YesNoCancel);
                                if (result == DialogResult.Yes)
                                {
                                    Debug.WriteLine("Updating link to the anchor '" + anchor + "', pointing to '" + link + "'.");
                                    foreach (Word.Hyperlink objLink in Globals.ThisAddIn.Application.Selection.Hyperlinks)
                                    {
                                        objLink.Delete();
                                    }
                                    Word.Hyperlink mylink = myLinks.Add(Globals.ThisAddIn.Application.Selection.Range, link);
                                }
                                else if (result == DialogResult.No)
                                {
                                    Debug.WriteLine("Skipping this instance of the anchor.");
                                }
                                else
                                {
                                    Debug.WriteLine("Aborting!");
                                    break;
                                }
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show("Do you wish to link the selected text? It will point to the following URL: '" + link + "'. Click Yes to proceed. Click No to skip this one instance. Click Cancel to abort the whole linking process.", "Link Text: Confirm Link", MessageBoxButtons.YesNoCancel);
                                if (result == DialogResult.Yes)
                                {
                                    Debug.WriteLine("Adding link to the anchor '" + anchor + "', pointing to '" + link + "'.");
                                    Word.Hyperlink mylink = myLinks.Add(Globals.ThisAddIn.Application.Selection.Range, link);
                                }
                                else if (result == DialogResult.No)
                                {
                                    Debug.WriteLine("Skipping this instance of the anchor.");
                                }
                                else
                                {
                                    Debug.WriteLine("Aborting!");
                                    break;
                                }
                            }
                        }
                        Globals.ThisAddIn.Application.Selection.Find.Execute(findText, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true, Word.WdFindWrap.wdFindAsk, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    }
                    MessageBox.Show("Linking process complete.");
                }
                else
                {
                    MessageBox.Show("No instances of '" + anchor + "' found. Remember that the search is case sensitive and, if text is selected, will only search inside that text.");
                }
            }
        }
    }
}
