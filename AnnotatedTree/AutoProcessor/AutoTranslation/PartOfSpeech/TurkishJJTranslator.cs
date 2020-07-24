using System.Collections.Generic;
using Dictionary.Dictionary;

namespace AnnotatedTree.AutoProcessor.AutoTranslation.PartOfSpeech
{
    public class TurkishJJTranslator : TurkishPartOfSpeechTranslator
    {
        public TurkishJJTranslator(ParseNodeDrawable parseNode, List<string> parentList, List<string> englishWordList,
            string prefix, string lastWordForm, TxtWord lastWord) : base(parseNode, parentList, englishWordList, prefix,
            lastWordForm, lastWord)
        {
        }

        public new string Translate()
        {
            string[] posArray =
                {
                    "VBZ", "VBZ", "VBP", "VBP", "VBD", "VBD"
                }
                ;
            string[] suffixArray =
                {
                    "DHr", "DHr", "DHr", "DHr", "yDH", "yDH"
                }
                ;
            string[] wordArray =
                {
                    "is", "'s", "are", "'re", "was", "were"
                }
                ;
            if (parentList.Count > 1)
            {
                for (var i = 0; i < posArray.Length; i++)
                {
                    if (parentList[1].Equals(posArray[i]) && englishWordList[1].Equals(wordArray[i]))
                    {
                        return AddSuffix(suffixArray[i], prefix, lastWord, lastWordForm);
                    }
                }
            }

            return prefix + lastWord.GetName();
        }
    }
}