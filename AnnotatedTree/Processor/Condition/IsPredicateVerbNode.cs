namespace AnnotatedTree.Processor.Condition
{
    public class IsPredicateVerbNode : IsVerbNode
    {
        /// <summary>
        /// Stores the wordnet for checking the pos tag of the synset.
        /// </summary>
        /// <param name="wordNet">Wordnet used for checking the pos tag of the synset.</param>
        public IsPredicateVerbNode(WordNet.WordNet wordNet) : base(wordNet)
        {
        }

        /// <summary>
        /// Checks if the node is a leaf node and at least one of the semantic ids of the parse node belong to a verb synset,
        /// and the semantic role of the node is PREDICATE.
        /// </summary>
        /// <param name="parseNode">Parse node to check.</param>
        /// <returns>True if the node is a leaf node and at least one of the semantic ids of the parse node belong to a verb
        ///          synset and the semantic role of the node is PREDICATE, false otherwise.</returns>
        public override bool Satisfies(ParseNodeDrawable parseNode)
        {
            var layerInfo = parseNode.GetLayerInfo();
            return base.Satisfies(parseNode) && layerInfo != null && layerInfo.GetArgument() != null &&
                   layerInfo.GetArgument().GetArgumentType().Equals("PREDICATE");
        }
    }
}