namespace AnnotatedTree.Processor.Condition
{
    public class IsEnglishLeafNode : IsLeafNode
    {
        public new bool Satisfies(ParseNodeDrawable parseNode) {
            if (base.Satisfies(parseNode)) {
                return !new IsNullElement().Satisfies(parseNode);
            }
            return false;
        }
        
    }
}