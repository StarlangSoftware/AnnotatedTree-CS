namespace AnnotatedTree.Processor.Condition
{
    public class IsLeafNode : NodeDrawableCondition
    {
        /// <summary>
        /// Checks if the parse node is a leaf node, i.e., it has no child.
        /// </summary>
        /// <param name="parseNode">Parse node to check.</param>
        /// <returns>True if the parse node is a leaf node, false otherwise.</returns>
        public virtual bool Satisfies(ParseNodeDrawable parseNode)
        {
            return parseNode.NumberOfChildren() == 0;
        }
    }
}