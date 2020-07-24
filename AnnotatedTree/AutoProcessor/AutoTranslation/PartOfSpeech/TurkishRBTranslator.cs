using System.Collections.Generic;
using Dictionary.Dictionary;

namespace AnnotatedTree.AutoProcessor.AutoTranslation.PartOfSpeech
{
    public class TurkishRBTranslator : TurkishPartOfSpeechTranslator
    {
        public TurkishRBTranslator(ParseNodeDrawable parseNode, List<string> parentList, List<string> englishWordList,
            string prefix, string lastWordForm, TxtWord lastWord) : base(parseNode, parentList, englishWordList, prefix,
            lastWordForm, lastWord)
        {
        }

        public new string Translate()
        {
            string[] posArray =
                {
                    "VBZ", "VBP", "VBD", "."
                }
                ;
            string[] suffixArray =
                {
                    "dir", "dir", "di", ""
                }
                ;
            if (parentList.Count > 1)
            {
                for (var i = 0; i < posArray.Length; i++)
                {
                    if (parentList[1].Equals(posArray[i]))
                    {
                        return "değil" + suffixArray[i];
                    }
                }
            }

            return "*NONE*";
        }
    }
}