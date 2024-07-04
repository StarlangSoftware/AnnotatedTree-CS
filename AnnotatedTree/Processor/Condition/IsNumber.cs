using System.Text.RegularExpressions;
using AnnotatedSentence;

namespace AnnotatedTree.Processor.Condition
{
    public class IsNumber : IsLeafNode
    {
        /// <summary>
        /// Checks if the node is a leaf node and contains numerals as the data and its parent has the tag CD.
        /// </summary>
        /// <param name="parseNode">Parse node to check.</param>
        /// <returns>True if the node is a leaf node and contains numerals as the data and its parent has the tag CD, false
        /// otherwise.</returns>
        public override bool Satisfies(ParseNodeDrawable parseNode) {
            if (base.Satisfies(parseNode)){
                string data = parseNode.GetLayerData(ViewLayerType.ENGLISH_WORD);
                string parentData = parseNode.GetParent().GetData().GetName();
                return parentData.Equals("CD") && new Regex("[0-9,.]+").IsMatch(data);
            }
            return false;
        }
        
    }
}