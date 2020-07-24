namespace AnnotatedTree.Processor.Condition
{
    public class IsLeafNode : NodeDrawableCondition
    {
        public bool Satisfies(ParseNodeDrawable parseNode)
        {
            return parseNode.NumberOfChildren() == 0;
        }
    }
}