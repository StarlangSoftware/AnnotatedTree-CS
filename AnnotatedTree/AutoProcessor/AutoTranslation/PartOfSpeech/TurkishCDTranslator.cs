using System.Collections.Generic;
using Dictionary.Dictionary;

namespace AnnotatedTree.AutoProcessor.AutoTranslation.PartOfSpeech
{
    public class TurkishCDTranslator : TurkishNounTranslator
    {
        protected bool withDigits;

        public TurkishCDTranslator(ParseNodeDrawable parseNode, List<string> parentList, List<string> englishWordList,
            string prefix, string lastWordForm, TxtWord lastWord, bool withDigits, TxtDictionary txtDictionary) : base(
            parseNode, parentList, englishWordList, prefix, lastWordForm, lastWord, txtDictionary)
        {
            this.withDigits = withDigits;
        }

        public new string Translate()
        {
            string[] posArray = {"IN", "TO"};
            string[][] wordArray =
            {
                new[]
                {
                    "in", "from", "than"
                },
                new[]
                {
                    "to"
                }
            };
            string[][] suffixArray1 =
            {
                new[]
                {
                    "'DA", "'DAn", "'DAn"
                },
                new[]
                {
                    "'yA"
                }
            };
            string[][] suffixArray2 =
            {
                new[]
                {
                    "DA", "DAn", "DAn"
                },
                new[]
                {
                    "yA"
                }
            };
            if (parentList.Count > 1)
            {
                string result;
                if (withDigits)
                {
                    result = TranslateNouns(posArray, wordArray, suffixArray1, parentList, englishWordList, prefix,
                        lastWord, lastWordForm);
                }
                else
                {
                    result = TranslateNouns(posArray, wordArray, suffixArray2, parentList, englishWordList, prefix,
                        lastWord, lastWordForm);
                }

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}