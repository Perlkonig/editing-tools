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
using edu.stanford.nlp.tagger.maxent;
using edu.stanford.nlp.ling;
using java.util;
using edu.stanford.nlp.parser.lexparser;
using edu.stanford.nlp.process;
using edu.stanford.nlp.trees;

namespace EditTools
{
    public partial class Ribbon1
    {
        public edu.stanford.nlp.tagger.maxent.MaxentTagger tagger;
        public edu.stanford.nlp.parser.lexparser.LexicalizedParser lp;

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            //Boilerplate
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

            //POSTagger
            Debug.WriteLine("Loading tagger model...");
            MemoryStream _stream = new MemoryStream(Properties.Resources.posmodel);
            java.io.InputStream model = new ikvm.io.InputStreamWrapper(_stream);
            tagger = new MaxentTagger(model);
            Debug.WriteLine("Model loaded.");

            //Typed Dependencies
            //Debug.WriteLine("Loading lexical parser model...");
            //_stream = new MemoryStream(Properties.Resources.englishPCFG_ser);
            //var isw = new ikvm.io.InputStreamWrapper(_stream);
            //var gzs = new java.util.zip.GZIPInputStream(isw);
            //var ois = new java.io.ObjectInputStream(gzs);
            //lp = LexicalizedParser.loadModel(ois);
            //Debug.WriteLine("Model loaded.");
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            ImportDialog id = new ImportDialog();
            id.ShowDialog();
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
            for (int i = 0; i < bps.Count - 1; i++)
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

        private void btn_SingData_Click(object sender, RibbonControlEventArgs e)
        {
            Word.Document doc = Globals.ThisAddIn.Application.ActiveDocument;
            Word.Paragraphs pgraphs;
            Word.Selection sel = Globals.ThisAddIn.Application.Selection;
            bool fromSelection = false;
            if (sel != null && sel.Range != null && sel.Characters.Count > 1)
            {
                pgraphs = sel.Paragraphs;
                fromSelection = true;
            }
            else
            {
                pgraphs = doc.Paragraphs;
            }
            Debug.WriteLine("From selection: " + fromSelection.ToString());

            foreach (Word.Paragraph pgraph in pgraphs)
            {
                Word.Range rng = pgraph.Range;
                foreach (Word.Range sentence in rng.Sentences)
                {
                    // POS
                    var tsentence = MaxentTagger.tokenizeText(new java.io.StringReader(sentence.Text)).toArray();
                    var taggedSentence = tagger.tagSentence((ArrayList) tsentence[0]);
                    var taglist = taggedSentence.toArray();
                    Boolean singular = false;
                    Boolean hasdata = false;

                    //First find obviously singular "data"
                    foreach (TaggedWord entry in taglist)
                    {
                        if (entry.word().ToLower() == "data")
                        {
                            hasdata = true;
                            if ((entry.tag() == "NN") || (entry.tag() == "NNP"))
                            {
                                singular = true;
                                break;
                            }
                        }
                    }

                    //Now look for plural tags with singular verbs
                    if ( (hasdata) && (! singular) )
                    {
                        foreach (TaggedWord entry in taglist)
                        {
                            if (entry.tag() == "VBZ")
                            {
                                singular = true;
                                break;
                            }
                        }
                    }
                    
                    //Highlight problematic sentences
                    if ( (hasdata) && (singular) )
                    {
                        sentence.HighlightColorIndex = Word.WdColorIndex.wdGray50;
                    }
                }

                //var sentences = MaxentTagger.tokenizeText(new java.io.StringReader(rng.Text)).toArray();
                //foreach (ArrayList sentence in sentences)
                //{
                //    String origsent = String.Join(" ", sentence.toArray());
                //    Debug.WriteLine(origsent);
                //    var taggedSentence = tagger.tagSentence(sentence);
                //    var taglist = taggedSentence.toArray();
                //    foreach (TaggedWord entry in taglist)
                //    {
                //        if (entry.word().ToLower() == "data")
                //        {
                //            if ( (entry.tag() == "NN") || (entry.tag() == "NNP") )
                //            {
                //                Debug.WriteLine("Found singular 'data' in the following sentence: " + origsent);
                //                TextHelpers.highlightText(pgraph.Range, "", Word.WdColorIndex.wdGray50);
                //            }
                //        }
                //    }
                //}

                //// Typed Dependencies
                //foreach (Word.Range sentence in rng.Sentences)
                //{
                //    var tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");
                //    var sent2Reader = new java.io.StringReader(sentence.Text);
                //    var rawWords = tokenizerFactory.getTokenizer(sent2Reader).tokenize();
                //    sent2Reader.close();
                //    var tree = lp.apply(rawWords);

                //    var tlp = new PennTreebankLanguagePack();
                //    var gsf = tlp.grammaticalStructureFactory();
                //    var gs = gsf.newGrammaticalStructure(tree);
                //    var tdl = gs.typedDependenciesCCprocessed();
                //    foreach (var dep in tdl.toArray())
                //    {
                //        Debug.WriteLine(dep);
                //    }
                //    Debug.WriteLine("=-=-=-=-=-");
                //}
            }
        }
    }
}
