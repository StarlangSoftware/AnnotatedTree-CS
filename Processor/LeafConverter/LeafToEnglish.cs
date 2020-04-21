using AnnotatedSentence;

namespace AnnotatedTree.Processor.LeafConverter
{
    public class LeafToEnglish : LeafToLanguageConverter
    {
        public LeafToEnglish()
        {
            viewLayerType = ViewLayerType.ENGLISH_WORD;
        }
    }
}