using System;
using AnnotatedSentence;
using Dictionary.Dictionary;

namespace AnnotatedTree.Processor.Condition
{
    public class IsVerbNode : IsLeafNode
    {
        private readonly WordNet.WordNet _wordNet;

        public IsVerbNode(WordNet.WordNet wordNet)
        {
            this._wordNet = wordNet;
        }

        public new bool Satisfies(ParseNodeDrawable parseNode)
        {
            var layerInfo = parseNode.GetLayerInfo();
            if (base.Satisfies(parseNode) && layerInfo != null &&
                layerInfo.GetLayerData(ViewLayerType.SEMANTICS) != null)
            {
                for (int i = 0; i < layerInfo.GetNumberOfMeanings(); i++)
                {
                    String synSetId = layerInfo.GetSemanticAt(i);
                    if (_wordNet.GetSynSetWithId(synSetId) != null &&
                        _wordNet.GetSynSetWithId(synSetId).GetPos() == Pos.VERB)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}