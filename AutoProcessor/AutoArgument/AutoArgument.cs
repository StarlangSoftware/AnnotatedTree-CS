using System;
using System.Collections.Generic;
using AnnotatedSentence;
using AnnotatedTree.Processor;
using AnnotatedTree.Processor.Condition;
using Dictionary.Dictionary;
using PropBank;

namespace AnnotatedTree.AutoProcessor.AutoArgument
{
    public abstract class AutoArgument
    {
        protected ViewLayerType secondLanguage;
        protected abstract bool AutoDetectArgument(ParseNodeDrawable parseNode, ArgumentType argumentType);

        protected AutoArgument(ViewLayerType secondLanguage)
        {
            this.secondLanguage = secondLanguage;
        }

        public void autoArgument(ParseTreeDrawable parseTree, Frameset frameset)
        {
            NodeDrawableCollector nodeDrawableCollector =
                new NodeDrawableCollector((ParseNodeDrawable) parseTree.GetRoot(), new IsTransferable(secondLanguage));
            List<ParseNodeDrawable> leafList = nodeDrawableCollector.Collect();
            foreach (var parseNode in leafList)
            {
                if (parseNode.GetLayerData(ViewLayerType.PROPBANK) == null)
                {
                    foreach (ArgumentType argumentType in Enum.GetValues(typeof(ArgumentType)))
                    {
                        if (frameset.ContainsArgument(argumentType) && AutoDetectArgument(parseNode, argumentType))
                        {
                            parseNode.GetLayerInfo().SetLayerData(ViewLayerType.PROPBANK,
                                ArgumentTypeStatic.GetPropbankType(argumentType));
                        }
                    }

                    if (Word.IsPunctuation(parseNode.GetLayerData(secondLanguage)))
                    {
                        parseNode.GetLayerInfo().SetLayerData(ViewLayerType.PROPBANK, "NONE");
                    }
                }
            }

            parseTree.Save();
        }
    }
}