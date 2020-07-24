using AnnotatedSentence;

namespace AnnotatedTree.Processor.Condition
{
    public class IsTurkishLeafNode : IsLeafNode
    {
        public new bool Satisfies(ParseNodeDrawable parseNode)
        {
            if (base.Satisfies(parseNode))
            {
                var data = parseNode.GetLayerInfo().GetLayerData(ViewLayerType.TURKISH_WORD);
                var parentData = parseNode.GetParent().GetData().GetName();
                return data != null && !data.Contains("*") && !(data.Equals("0") && parentData.Equals("-NONE-"));
            }

            return false;
        }
    }
}