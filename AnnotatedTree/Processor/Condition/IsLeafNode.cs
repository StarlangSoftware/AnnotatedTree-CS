namespace AnnotatedTree.Processor.Condition
{
    public class IsLeafNode : NodeDrawableCondition
    {
        public virtual bool Satisfies(ParseNodeDrawable parseNode)
        {
            return parseNode.NumberOfChildren() == 0;
        }
    }
}