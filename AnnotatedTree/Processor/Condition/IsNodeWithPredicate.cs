using AnnotatedSentence;

namespace AnnotatedTree.Processor.Condition
{
    public class IsNodeWithPredicate : IsNodeWithSynSetId
    {
        /// <summary>
        /// Stores the synset id to check.
        /// </summary>
        /// <param name="id">Synset id to check</param>
        public IsNodeWithPredicate(string id) : base(id)
        {
        }
        
        /// <summary>
        /// Checks if at least one of the semantic ids of the parse node is equal to the given id and also the node is
        /// annotated as PREDICATE with semantic role.
        /// </summary>
        /// <param name="parseNode">Parse node to check.</param>
        /// <returns>True if at least one of the semantic ids of the parse node is equal to the given id and also the node is
        /// annotated as PREDICATE with semantic role, false otherwise.</returns>
        public override bool Satisfies(ParseNodeDrawable parseNode) {
            var layerInfo = parseNode.GetLayerInfo();
            return base.Satisfies(parseNode) && layerInfo != null && layerInfo.GetLayerData(ViewLayerType.PROPBANK) != null && layerInfo.GetLayerData(ViewLayerType.PROPBANK).Equals("PREDICATE");
        }

    }
}