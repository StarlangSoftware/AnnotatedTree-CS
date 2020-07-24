namespace AnnotatedTree.Processor.Condition
{
    public class IsPredicateVerbNode : IsVerbNode
    {
        public IsPredicateVerbNode(WordNet.WordNet wordNet) : base(wordNet)
        {
        }

        public new bool Satisfies(ParseNodeDrawable parseNode)
        {
            var layerInfo = parseNode.GetLayerInfo();
            return base.Satisfies(parseNode) && layerInfo != null && layerInfo.GetArgument() != null &&
                   layerInfo.GetArgument().GetArgumentType().Equals("PREDICATE");
        }
    }
}