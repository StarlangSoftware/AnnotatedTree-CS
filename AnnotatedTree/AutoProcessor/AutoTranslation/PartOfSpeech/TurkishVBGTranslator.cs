using System.Collections.Generic;
using Dictionary.Dictionary;
using MorphologicalAnalysis;

namespace AnnotatedTree.AutoProcessor.AutoTranslation.PartOfSpeech
{
    public class TurkishVBGTranslator : TurkishVerbTranslator
    {
        public TurkishVBGTranslator(ParseNodeDrawable parseNode, List<string> parentList, List<string> englishWordList,
            string prefix, string lastWordForm, TxtWord lastWord) : base(parseNode, parentList, englishWordList, prefix,
            lastWordForm, lastWord)
        {
        }

        public new string Translate()
        {
            string[] posArray =
                {
                    "VBZ", "VBP", "VBD", "VBN"
                }
                ;
            string[] suffixArray =
                {
                    "yor", "yor", "yordu", "yor"
                }
                ;
            Transition transition;
            int i;
            string negation;
            if (parentList.Count > 4 && parentList[1].Equals("RB") && parentList[2].Equals("VB") &&
                parentList[3].Equals("MD") && parentList[4].Equals("PRP"))
            {
                transition = new Transition("mAyAcAk" + PersonalSuffix1(englishWordList[4].ToLower()));
                return prefix + transition.MakeTransition(lastWord, lastWordForm);
            }

            if (parentList.Count > 3 && parentList[1].Equals("RB") && parentList[2].Equals("VB") &&
                parentList[3].Equals("MD"))
            {
                transition = new Transition("mAyAcAk");
                return prefix + transition.MakeTransition(lastWord, lastWordForm);
            }

            if (parentList.Count > 3 && parentList[1].Equals("VB") && parentList[2].Equals("MD") &&
                parentList[3].Equals("PRP"))
            {
                transition = new Transition("yAcAk" + PersonalSuffix1(englishWordList[3].ToLower()));
                return prefix + transition.MakeTransition(lastWord, lastWordForm);
            }

            if (parentList.Count > 2 && parentList[1].Equals("VB") && parentList[2].Equals("MD"))
            {
                transition = new Transition("yAcAk");
                return prefix + transition.MakeTransition(lastWord, lastWordForm);
            }

            if (parentList.Count > 1 && parentList[1].Equals("RB"))
            {
                i = 2;
                negation = "mH";
            }
            else
            {
                i = 1;
                negation = "H";
            }

            if (i < parentList.Count)
            {
                for (int j = 0; j < posArray.Length; j++)
                {
                    if (parentList[i].Equals(posArray[j]))
                    {
                        if (i + 1 < parentList.Count && parentList[i + 1].Equals("PRP"))
                        {
                            transition = new Transition(negation + suffixArray[j] +
                                                        PersonalSuffix1(englishWordList[i + 1].ToLower()));
                        }
                        else
                        {
                            transition = new Transition(negation + suffixArray[j]);
                        }

                        return prefix + transition.MakeTransition(lastWord, lastWordForm);
                    }
                }
            }

            if (IsLastWordOfVerbAsNoun(parseNode))
            {
                return AddSuffix("yAn", prefix, lastWord, lastWordForm);
            }

            return AddSuffix("mAk", prefix, lastWord, lastWordForm);
        }
    }
}