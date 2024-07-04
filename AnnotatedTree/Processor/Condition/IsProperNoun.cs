namespace AnnotatedTree.Processor.Condition
{
    public class IsProperNoun : IsLeafNode
    {
        /// <summary>
        /// Checks if the node is a leaf node and its parent has the tag NNP or NNPS.
        /// </summary>
        /// <param name="parseNode">Parse node to check.</param>
        /// <returns>True if the node is a leaf node and its parent has the tag NNP or NNPS, false otherwise.</returns>
        public override bool Satisfies(ParseNodeDrawable parseNode) {
            if (base.Satisfies(parseNode)){
                var parentData = parseNode.GetParent().GetData().GetName();
                return parentData.Equals("NNP") || parentData.Equals("NNPS");
            }
            return false;
        }
    }
}