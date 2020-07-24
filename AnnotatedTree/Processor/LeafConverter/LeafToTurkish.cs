using AnnotatedSentence;

namespace AnnotatedTree.Processor.LeafConverter
{
    public class LeafToTurkish : LeafToLanguageConverter
    {
        public LeafToTurkish()
        {
            viewLayerType = ViewLayerType.TURKISH_WORD;
        }
    }
}