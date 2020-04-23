using System.Collections.Generic;
using Dictionary.Dictionary;
using MorphologicalAnalysis;

namespace AnnotatedTree.AutoProcessor.AutoTranslation.PartOfSpeech
{
    public class TurkishVBDTranslator : TurkishVerbTranslator
    {
        public TurkishVBDTranslator(ParseNodeDrawable parseNode, List<string> parentList, List<string> englishWordList,
            string prefix, string lastWordForm, TxtWord lastWord) : base(parseNode, parentList, englishWordList, prefix,
            lastWordForm, lastWord)
        {
        }

        public new string Translate()
        {
            var transition = new Transition("DH");
            if (parentList.Count > 1 && parentList[1].Equals("PRP"))
            {
                transition = new Transition("DH" + PersonalSuffix2(englishWordList[1].ToLower()));
            }

            return prefix + transition.MakeTransition(lastWord, lastWordForm);
        }
    }
}