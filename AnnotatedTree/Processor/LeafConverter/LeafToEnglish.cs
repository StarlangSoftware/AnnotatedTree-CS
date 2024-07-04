using AnnotatedSentence;

namespace AnnotatedTree.Processor.LeafConverter
{
    public class LeafToEnglish : LeafToLanguageConverter
    {
        /// <summary>
        /// Constructor for LeafToEnglish. Sets viewLayerType to ENGLISH.
        /// </summary>
        public LeafToEnglish()
        {
            viewLayerType = ViewLayerType.ENGLISH_WORD;
        }
    }
}