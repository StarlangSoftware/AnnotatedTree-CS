using AnnotatedSentence;

namespace AnnotatedTree.Processor.Condition
{
    public class IsNullElement : IsLeafNode
    {
        /// <summary>
        /// Checks if the parse node is a leaf node and its data is '*' and its parent's data is '-NONE-'.
        /// </summary>
        /// <param name="parseNode">Parse node to check.</param>
        /// <returns>True if the parse node is a leaf node and its data is '*' and its parent's data is '-NONE-', false
        /// otherwise.</returns>
        public override bool Satisfies(ParseNodeDrawable parseNode) {
            if (base.Satisfies(parseNode)){
                var data = parseNode.GetLayerData(ViewLayerType.ENGLISH_WORD);
                var parentData = parseNode.GetParent().GetData().GetName();
                return data.Contains("*") || (data.Equals("0") && parentData.Equals("-NONE-"));
            }
            return false;
        }
        
    }
}