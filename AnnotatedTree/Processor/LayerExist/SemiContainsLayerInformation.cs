using System.Collections.Generic;
using AnnotatedSentence;
using AnnotatedTree.Processor.Condition;

namespace AnnotatedTree.Processor.LayerExist
{
    public class SemiContainsLayerInformation : LeafListCondition
    {
        private readonly ViewLayerType _viewLayerType;

        public SemiContainsLayerInformation(ViewLayerType viewLayerType)
        {
            this._viewLayerType = viewLayerType;
        }

        public bool Satisfies(List<ParseNodeDrawable> leafList)
        {
            int notDone = 0, done = 0;
            foreach (var parseNode in leafList){
                if (!parseNode.GetLayerData(ViewLayerType.ENGLISH_WORD).Contains("*"))
                {
                    switch (_viewLayerType)
                    {
                        case ViewLayerType.TURKISH_WORD:
                            if (parseNode.GetLayerData(_viewLayerType) != null)
                            {
                                done++;
                            }
                            else
                            {
                                notDone++;
                            }

                            break;
                        case ViewLayerType.PART_OF_SPEECH:
                        case ViewLayerType.INFLECTIONAL_GROUP:
                        case ViewLayerType.NER:
                        case ViewLayerType.SEMANTICS:
                            if (new IsTurkishLeafNode().Satisfies(parseNode))
                            {
                                if (parseNode.GetLayerData(_viewLayerType) != null)
                                {
                                    done++;
                                }
                                else
                                {
                                    notDone++;
                                }
                            }

                            break;
                    }
                }
            }
            return done != 0 && notDone != 0;
        }
    }
}