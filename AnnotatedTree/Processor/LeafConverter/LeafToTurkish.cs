using AnnotatedSentence;

namespace AnnotatedTree.Processor.LeafConverter
{
    public class LeafToTurkish : LeafToLanguageConverter
    {
        /// <summary>
        /// Constructor for LeafToTurkish. Sets viewLayerType to TURKISH.
        /// </summary>
        public LeafToTurkish()
        {
            viewLayerType = ViewLayerType.TURKISH_WORD;
        }
    }
}