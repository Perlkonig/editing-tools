using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Word = Microsoft.Office.Interop.Word;

namespace EditTools
{
    public static class TextHelpers
    {
        public struct compword
        {
            public string word;
            public ushort metaphone1;
            public ushort metaphone2;
            public int distance;
        }

        public static IEnumerable<Word.Range> GetText(Word.Document doc)
        {
            foreach (Word.Paragraph para in doc.Paragraphs)
            {
                yield return para.Range;
            }

            foreach (Word.Footnote fn in doc.Footnotes)
            {
                yield return fn.Range;
            }

            foreach (Word.Endnote fn in doc.Endnotes)
            {
                yield return fn.Range;
            }

            yield break;
        }

        public static string StripPunctuation(string txt)
        {
            Regex re_fixquotes = new Regex(@"[\’\‘]");
            string result = re_fixquotes.Replace(txt, "'");

            Regex re_punc = new Regex(@"[^A-Za-z0-9\-\'\ ]");
            result = re_punc.Replace(result, " ");

            Regex re_apos2bar = new Regex(@"(?<=[A-Za-z0-9])\'(?=[A-Za-z0-9])");
            result = re_apos2bar.Replace(result, "|");
            Regex re_stripapos = new Regex(@"\'");
            result = re_stripapos.Replace(result, "");
            Regex re_bar2apos = new Regex(@"\|");
            result = re_bar2apos.Replace(result, "'");

            return result;
        }

        public static int SingularData(string txt)
        {
            Regex re_pre = new Regex(@"(?i)this data");
            Regex re_post = new Regex(@"(?i)data (is|was|has)");
            int count = 0;
            count += re_pre.Matches(txt).Count;
            count += re_post.Matches(txt).Count;
            return count;
        }

        public static HashSet<string> ToWords(string txt)
        {
            HashSet<string> set = new HashSet<string>();
            string[] substrs = Regex.Split(txt, @"\s+");
            foreach (string match in substrs)
            {
                set.Add(match);
            }
            return set;
        }

        public static HashSet<string> RemoveNumbers(HashSet<string> list)
        {
            HashSet<string> result = new HashSet<string>();
            Regex re_allnums = new Regex(@"^\d+$");
            foreach (string word in list)
            {
                Match m = re_allnums.Match(word);
                if (!m.Success)
                {
                    result.Add(word);
                }
            }
            return result;
        }

        public static HashSet<string> ProperNouns(string str)
        {
            Regex re_propers = new Regex(@"(?<![\.\!\?]\s+)[A-Z][A-Za-z0-9]+");
            HashSet<string> result = new HashSet<string>();
            str = ". " + str;

            foreach (Match m in re_propers.Matches(str))
            {
                result.Add(m.ToString());
            }

            return result;
        }

        public static HashSet<string> KeepCaps(HashSet<string> list)
        {
            HashSet<string> capped = new HashSet<string>();
            Regex re_initCap = new Regex(@"^[A-Z]");
            Regex re_allCap = new Regex(@"^[A-Z]+$");

            foreach (string word in list)
            {
                Match match1 = re_initCap.Match(word);
                if (match1.Success)
                {
                    Match match2 = re_allCap.Match(word);
                    if (!match2.Success)
                    {
                        capped.Add(word);
                    }
                }
            }

            return capped;
        }

        public static int EditDistance(string original, string modified)
        {
            if (original == modified)
                return 0;

            int len_orig = original.Length;
            int len_diff = modified.Length;

            if (len_orig == 0)
                return len_diff;

            if (len_diff == 0)
                return len_orig;

            var matrix = new int[len_orig + 1, len_diff + 1];

            for (int i = 1; i <= len_orig; i++)
            {
                matrix[i, 0] = i;
                for (int j = 1; j <= len_diff; j++)
                {
                    int cost = modified[j - 1] == original[i - 1] ? 0 : 1;
                    if (i == 1)
                        matrix[0, j] = j;

                    var vals = new int[] {
                    matrix[i - 1, j] + 1,
                    matrix[i, j - 1] + 1,
                    matrix[i - 1, j - 1] + cost
                };
                    matrix[i, j] = vals.Min();
                    if (i > 1 && j > 1 && original[i - 1] == modified[j - 2] && original[i - 2] == modified[j - 1])
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
                }
            }
            return matrix[len_orig, len_diff];
        }

        public static void highlightText(Word.Range rng, string str, Word.WdColorIndex clr)
        {
            rng.Find.ClearFormatting();
            rng.Find.Forward = true;
            rng.Find.Text = str;
            rng.Find.Execute();
            while (rng.Find.Found)
            {
                rng.HighlightColorIndex = clr;
                rng.Find.Execute();
            }
        }

        public static bool IsBalanced(string txt)
        {
            //BRACKETS FIRST

            Dictionary<char, char> o2c = new Dictionary<char, char>();
            o2c.Add('(', ')');
            o2c.Add('[', ']');
            o2c.Add('{', '}');
            Dictionary<char, char> c2o = new Dictionary<char, char>();
            c2o.Add(')', '(');
            c2o.Add(']', '[');
            c2o.Add('}', '{');

            //strip all non-tokens
            Regex re_nontoken = new Regex(@"[^\(\)\[\]\{\}]");
            string result = re_nontoken.Replace(txt, "");

            //process
            char[] tokens = result.ToCharArray();
            Stack<char> stack = new Stack<char>();
            foreach (char token in tokens)
            {
                //Is this an opener?
                if (o2c.ContainsKey(token))
                {
                    stack.Push(token);
                }
                else
                {
                    //Is the matching token on top?
                    if (stack.Peek() == c2o[token])
                    {
                        stack.Pop();
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (stack.Count > 0)
            {
                return false;
            }

            //SIMPLE QUOTE CHECK

            //singles
            //strip apostrophes
            Regex re_apos = new Regex(@"(?<=[A-Za-z0-9])[\'\‘\’](?=[A-Za-z0-9])");
            result = re_apos.Replace(txt, "");

            //strip nontokens
            Regex re_nonsingle = new Regex(@"[^\'\‘\’]");
            result = re_nonsingle.Replace(result, "");
            if (result.Length % 2 != 0)
            {
                return false;
            }

            //doubles
            //strip nontokens
            Regex re_nondouble = new Regex(@"[^""\“\”]");
            result = re_nondouble.Replace(txt, "");
            if (result.Length % 2 != 0)
            {
                return false;
            }

            return true;
        }
    }
}
