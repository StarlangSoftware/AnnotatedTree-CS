using AnnotatedSentence;

namespace AnnotatedTree.Processor.Condition
{
    public class IsNullElement : IsLeafNode
    {
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