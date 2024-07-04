namespace AnnotatedTree.Processor.Condition
{
    public class IsEnglishLeafNode : IsLeafNode
    {
        /// <summary>
        /// Checks if the parse node is a leaf node and contains a valid English word in its data.
        /// </summary>
        /// <param name="parseNode">Parse node to check.</param>
        /// <returns>True if the parse node is a leaf node and contains a valid English word in its data; false
        /// otherwise.</returns>
        public override bool Satisfies(ParseNodeDrawable parseNode) {
            if (base.Satisfies(parseNode)) {
                return !new IsNullElement().Satisfies(parseNode);
            }
            return false;
        }
        
    }
}