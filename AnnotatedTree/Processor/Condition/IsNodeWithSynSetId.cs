namespace AnnotatedTree.Processor.Condition
{
    public class IsNodeWithSynSetId : IsLeafNode
    {
        private readonly string _id;

        public IsNodeWithSynSetId(string id)
        {
            this._id = id;
        }

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