using System.Collections.Generic;
using Dictionary.Dictionary;
using MorphologicalAnalysis;

namespace AnnotatedTree.AutoProcessor.AutoTranslation.PartOfSpeech
{
    public class TurkishVerbTranslator : TurkishPartOfSpeechTranslator
    {
        public TurkishVerbTranslator(ParseNodeDrawable parseNode, List<string> parentList, List<string> englishWordList,
            string prefix, string lastWordForm, TxtWord lastWord) : base(parseNode, parentList, englishWordList, prefix,
            lastWordForm, lastWord)
        {
        }

        protected string PersonalSuffix1(string personalPronoun)
        {
            switch (personalPronoun)
            {
                case "i":
                    return "Hm";
                case "you":
                    return "SHn";
                case "we":
                    return "Hz";
                case "they":
                    return "lAr";
            }

            return "";
        }

        protected string PersonalSuffix2(string personalPronoun)
        {
            switch (personalPronoun)
            {
                case "i":
                    return "m";
                case "you":
                    return "n";
                case "we":
                    return "k";
                case "they":
                    return "lAr";
            }

            return "";
        }

        protected string PersonalSuffix3(string personalPronoun)
        {
            switch (personalPronoun)
            {
                case "i":
                    return "yHm";
                case "you":
                    return "SHn";
                case "we":
                    return "yHz";
                case "they":
                    return "lAr";
            }

            return "";
        }

        protected string PersonalSuffix4(string personalPronoun)
        {
            switch (personalPronoun)
            {
                case "i":
                    return "m";
                case "you":
                    return "zSHn";
                case "we":
                    return "yHz";
                case "they":
                    return "zlAr";
            }

            return "";
        }

        protected string AddSuffix(string suffix, string prefix, TxtWord root, string stem, string personalPronoun)
        {
            var transition = new Transition(suffix + PersonalSuffix2(personalPronoun));
            return prefix + transition.MakeTransition(root, stem);
        }

        protected string AddPassiveSuffix(string suffix1, string suffix2, string suffix3, string prefix, TxtWord root,
            string stem)
        {
            Transition transition;
            switch (root.VerbType())
            {
                case "F4PW": //"nDH"
                case "F4PW-NO-REF":
                    transition = new Transition(suffix1);
                    break;
                case "F5PR-NO-REF": //"DH"
                case "F5PL-NO-REF":
                    transition = new Transition(suffix2);
                    break;
                default: //"HlDH"
                    transition = new Transition(suffix3);
                    break;
            }

            return prefix + transition.MakeTransition(root, stem);
        }

        protected bool IsLastWordOfVerbAsNoun(ParseNodeDrawable parseNode)
        {
            ParseNodeDrawable parent = (ParseNodeDrawable) parseNode.GetParent();
            ParseNodeDrawable grandParent = (ParseNodeDrawable) parent.GetParent();
            if (parent.IsLastChild(parseNode) && parent.GetData().GetName().Equals("VP"))
            {
                if (grandParent != null && grandParent.GetData().GetName().Equals("NP") &&
                    grandParent.GetChild(0).Equals(parent)
                    && grandParent.NumberOfChildren() == 2 && grandParent.GetChild(1).GetData().GetName().Equals("NP"))
                {
                    return true;
                }
            }

            return false;
        }
    }
}