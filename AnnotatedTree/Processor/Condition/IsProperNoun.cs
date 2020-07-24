namespace AnnotatedTree.Processor.Condition
{
    public class IsProperNoun : IsLeafNode
    {
        public new bool Satisfies(ParseNodeDrawable parseNode) {
            if (base.Satisfies(parseNode)){
                var parentData = parseNode.GetParent().GetData().GetName();
                return parentData.Equals("NNP") || parentData.Equals("NNPS");
            }
            return false;
        }
    }
}