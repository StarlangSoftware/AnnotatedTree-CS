using AnnotatedSentence;

namespace AnnotatedTree.Processor.Condition
{
    public class IsNodeWithPredicate : IsNodeWithSynSetId
    {
        public IsNodeWithPredicate(string id) : base(id)
        {
        }
        
        public override bool Satisfies(ParseNodeDrawable parseNode) {
            var layerInfo = parseNode.GetLayerInfo();
            return base.Satisfies(parseNode) && layerInfo != null && layerInfo.GetLayerData(ViewLayerType.PROPBANK) != null && layerInfo.GetLayerData(ViewLayerType.PROPBANK).Equals("PREDICATE");
        }

    }
}