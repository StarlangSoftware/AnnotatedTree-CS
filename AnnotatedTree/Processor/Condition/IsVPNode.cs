namespace AnnotatedTree.Processor.Condition
{
    public class IsVPNode : NodeDrawableCondition
    {
        /// <summary>
        /// Checks if the node is not a leaf node and its tag is VP.
        /// </summary>
        /// <param name="parseNode">Parse node to check.</param>
        /// <returns>True if the node is not a leaf node and its tag is VP, false otherwise.</returns>
        public bool Satisfies(ParseNodeDrawable parseNode)
        {
            return parseNode.NumberOfChildren() > 0 && parseNode.GetData().IsVp();
        }
    }
}