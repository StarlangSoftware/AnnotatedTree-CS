using System.Collections.Generic;
using AnnotatedSentence;
using AnnotatedTree.Processor.Condition;

namespace AnnotatedTree.Processor.LayerExist
{
    public class NotContainsLayerInformation : LeafListCondition
    {
        private readonly ViewLayerType _viewLayerType;

        /// <summary>
        /// Constructor for NotContainsLayerInformation class. Sets the viewLayerType attribute.
        /// </summary>
        /// <param name="viewLayerType">Layer for which check is done.</param>
        public NotContainsLayerInformation(ViewLayerType viewLayerType)
        {
            this._viewLayerType = viewLayerType;
        }

        /// <summary>
        /// Checks if none of the leaf nodes in the leafList contains the given layer information.
        /// </summary>
        /// <param name="leafList">Array list storing the leaf nodes.</param>
        /// <returns>True if none of the leaf nodes in the leafList contains the given layer information, false
        /// otherwise.</returns>
        public bool Satisfies(List<ParseNodeDrawable> leafList)
        {
            foreach (var parseNode in leafList)
            {
                if (!parseNode.GetLayerData(ViewLayerType.ENGLISH_WORD).Contains("*"))
                {
                    switch (_viewLayerType)
                    {
                        case ViewLayerType.TURKISH_WORD:
                            if (parseNode.GetLayerData(_viewLayerType) != null)
                            {
                                return false;
                            }

                            break;
                        case ViewLayerType.PART_OF_SPEECH:
                        case ViewLayerType.INFLECTIONAL_GROUP:
                        case ViewLayerType.NER:
                        case ViewLayerType.SEMANTICS:
                            if (parseNode.GetLayerData(_viewLayerType) != null &&
                                new IsTurkishLeafNode().Satisfies(parseNode))
                            {
                                return false;
                            }

                            break;
                    }
                }
            }

            return true;
        }
    }
}