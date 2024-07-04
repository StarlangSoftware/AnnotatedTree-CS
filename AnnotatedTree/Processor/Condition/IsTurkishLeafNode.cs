using AnnotatedSentence;

namespace AnnotatedTree.Processor.Condition
{
    public class IsTurkishLeafNode : IsLeafNode
    {
        /// <summary>
        /// Checks if the parse node is a leaf node and contains a valid Turkish word in its data.
        /// </summary>
        /// <param name="parseNode">Parse node to check.</param>
        /// <returns>True if the parse node is a leaf node and contains a valid Turkish word in its data; false otherwise.</returns>
        public override bool Satisfies(ParseNodeDrawable parseNode)
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