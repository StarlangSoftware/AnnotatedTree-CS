using AnnotatedSentence;

namespace AnnotatedTree.Processor.LeafConverter
{
    public class LeafToPersian : LeafToLanguageConverter
    {
        public LeafToPersian()
        {
            viewLayerType = ViewLayerType.PERSIAN_WORD;
        }
    }
}