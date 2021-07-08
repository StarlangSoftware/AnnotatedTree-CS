using System.Text.RegularExpressions;
using AnnotatedSentence;

namespace AnnotatedTree.Processor.Condition
{
    public class IsNumber : IsLeafNode
    {
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