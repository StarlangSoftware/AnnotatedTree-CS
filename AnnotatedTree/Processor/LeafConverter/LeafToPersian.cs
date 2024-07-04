using AnnotatedSentence;

namespace AnnotatedTree.Processor.LeafConverter
{
    public class LeafToPersian : LeafToLanguageConverter
    {
        /// <summary>
        /// Constructor for LeafToPersian. Sets viewLayerType to PERSIAN.
        /// </summary>
        public LeafToPersian()
        {
            viewLayerType = ViewLayerType.PERSIAN_WORD;
        }
    }
}