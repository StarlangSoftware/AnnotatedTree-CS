namespace AnnotatedTree.Processor.Condition
{
    public class IsVPNode : NodeDrawableCondition
    {
        public bool Satisfies(ParseNodeDrawable parseNode)
        {
            return parseNode.NumberOfChildren() > 0 && parseNode.GetData().IsVp();
        }
    }
}