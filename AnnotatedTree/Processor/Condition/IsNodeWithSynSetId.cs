namespace AnnotatedTree.Processor.Condition
{
    public class IsNodeWithSynSetId : IsLeafNode
    {
        private readonly string _id;

        /// <summary>
        /// Stores the synset id to check.
        /// </summary>
        /// <param name="id">Synset id to check</param>
        public IsNodeWithSynSetId(string id)
        {
            this._id = id;
        }

        /// <summary>
        /// Checks if at least one of the semantic ids of the parse node is equal to the given id.
        /// </summary>
        /// <param name="parseNode">Parse node to check.</param>
        /// <returns>True if at least one of the semantic ids of the parse node is equal to the given id, false
        /// otherwise.</returns>
        public override bool Satisfies(ParseNodeDrawable parseNode)
        {
            if (base.Satisfies(parseNode))
            {
                var layerInfo = parseNode.GetLayerInfo();
                for (var i = 0; i < layerInfo.GetNumberOfMeanings(); i++)
                {
                    var synSetId = layerInfo.GetSemanticAt(i);
                    if (synSetId.Equals(_id))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}