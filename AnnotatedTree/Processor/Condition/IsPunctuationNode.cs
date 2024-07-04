using AnnotatedSentence;
using Dictionary.Dictionary;

namespace AnnotatedTree.Processor.Condition
{
    public class IsPunctuationNode : IsLeafNode
    {
        /// <summary>
        /// Checks if the node is a leaf node and contains punctuation as the data.
        /// </summary>
        /// <param name="parseNode">Parse node to check.</param>
        /// <returns>True if the node is a leaf node and contains punctuation as the data, false otherwise.</returns>
        public override bool Satisfies(ParseNodeDrawable parseNode) {
            if (base.Satisfies(parseNode)){
                var data = parseNode.GetLayerData(ViewLayerType.ENGLISH_WORD);
                return Word.IsPunctuation(data) && !data.Equals("$");
            }
            return false;
        }
        
    }
}