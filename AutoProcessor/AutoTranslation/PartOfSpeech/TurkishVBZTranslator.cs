using System.Collections.Generic;
using Dictionary.Dictionary;
using MorphologicalAnalysis;

namespace AnnotatedTree.AutoProcessor.AutoTranslation.PartOfSpeech
{
    public class TurkishVBZTranslator : TurkishVerbTranslator
    {
        public TurkishVBZTranslator(ParseNodeDrawable parseNode, List<string> parentList, List<string> englishWordList,
            string prefix, string lastWordForm, TxtWord lastWord) : base(parseNode, parentList, englishWordList, prefix,
            lastWordForm, lastWord)
        {
        }

        public new string Translate()
        {
            Transition transition;
            if (lastWord.TakesSuffixIRAsAorist())
            {
                transition = new Transition("Hr");
                if (parentList.Count > 1 && parentList[1].Equals("PRP"))
                {
                    transition = new Transition("Hr" + PersonalSuffix1(englishWordList[1].ToLower()));
                }
            }
            else
            {
                transition = new Transition("Ar");
                if (parentList.Count > 1 && parentList[1].Equals("PRP"))
                {
                    transition = new Transition("Ar" + PersonalSuffix1(englishWordList[1].ToLower()));
                }
            }

            return prefix + transition.MakeTransition(lastWord, lastWordForm);
        }
    }
}