namespace AnnotatedTree.Processor.Condition
{
    public class IsEnglishLeafNode : IsLeafNode
    {
        public override bool Satisfies(ParseNodeDrawable parseNode) {
            if (base.Satisfies(parseNode)) {
                return !new IsNullElement().Satisfies(parseNode);
            }
            return false;
        }
        
    }
}