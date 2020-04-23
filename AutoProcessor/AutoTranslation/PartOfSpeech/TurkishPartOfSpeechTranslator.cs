using System.Collections.Generic;
using Dictionary.Dictionary;
using MorphologicalAnalysis;

namespace AnnotatedTree.AutoProcessor.AutoTranslation.PartOfSpeech
{
    public class TurkishPartOfSpeechTranslator : PartOfSpeechTranslator
    {
        protected ParseNodeDrawable parseNode;
        protected List<string> parentList;
        protected List<string> englishWordList;
        protected string prefix;
        protected string lastWordForm;
        protected TxtWord lastWord;

        public TurkishPartOfSpeechTranslator(ParseNodeDrawable parseNode, List<string> parentList,
            List<string> englishWordList, string prefix, string lastWordForm, TxtWord lastWord)
        {
            this.parseNode = parseNode;
            this.parentList = parentList;
            this.englishWordList = englishWordList;
            this.prefix = prefix;
            this.lastWordForm = lastWordForm;
            this.lastWord = lastWord;
        }

        protected string AddSuffix(string suffix, string prefix, TxtWord root, string stem)
        {
            Transition transition;
            transition = new Transition(suffix);
            return prefix + transition.MakeTransition(root, stem);
        }

        public string Translate()
        {
            return "";
        }
    }
}