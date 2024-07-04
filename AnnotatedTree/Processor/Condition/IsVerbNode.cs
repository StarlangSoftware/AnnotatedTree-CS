using System;
using AnnotatedSentence;
using Dictionary.Dictionary;

namespace AnnotatedTree.Processor.Condition
{
    public class IsVerbNode : IsLeafNode
    {
        private readonly WordNet.WordNet _wordNet;

        /// <summary>
        /// Stores the wordnet for checking the pos tag of the synset.
        /// </summary>
        /// <param name="wordNet">Wordnet used for checking the pos tag of the synset.</param>
        public IsVerbNode(WordNet.WordNet wordNet)
        {
            this._wordNet = wordNet;
        }

        /// <summary>
        /// Checks if the node is a leaf node and at least one of the semantic ids of the parse node belong to a verb
        /// synset.
        /// </summary>
        /// <param name="parseNode">Parse node to check.</param>
        /// <returns>True if the node is a leaf node and at least one of the semantic ids of the parse node belong to a verb
        /// synset, false otherwise.</returns>
        public override bool Satisfies(ParseNodeDrawable parseNode)
        {
            var layerInfo = parseNode.GetLayerInfo();
            if (base.Satisfies(parseNode) && layerInfo != null &&
                layerInfo.GetLayerData(ViewLayerType.SEMANTICS) != null)
            {
                for (int i = 0; i < layerInfo.GetNumberOfMeanings(); i++)
                {
                    var synSetId = layerInfo.GetSemanticAt(i);
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