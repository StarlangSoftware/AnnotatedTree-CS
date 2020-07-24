namespace AnnotatedTree.Processor.Condition
{
    public interface NodeDrawableCondition
    {
        bool Satisfies(ParseNodeDrawable parseNode);
    }
}