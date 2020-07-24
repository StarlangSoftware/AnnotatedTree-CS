using AnnotatedSentence;
using AnnotatedTree.Processor;
using AnnotatedTree.Processor.Condition;

namespace AnnotatedTree.AutoProcessor.AutoNER
{
    public abstract class TreeAutoNER : NamedEntityRecognition.AutoNER
    {
        protected ViewLayerType secondLanguage;

        protected abstract void AutoDetectPerson(ParseTreeDrawable parseTree);
        protected abstract void AutoDetectLocation(ParseTreeDrawable parseTree);
        protected abstract void AutoDetectOrganization(ParseTreeDrawable parseTree);
        protected abstract void AutoDetectMoney(ParseTreeDrawable parseTree);
        protected abstract void AutoDetectTime(ParseTreeDrawable parseTree);

        protected TreeAutoNER(ViewLayerType secondLanguage)
        {
            this.secondLanguage = secondLanguage;
        }

        public void AutoNer(ParseTreeDrawable parseTree)
        {
            AutoDetectPerson(parseTree);
            AutoDetectLocation(parseTree);
            AutoDetectOrganization(parseTree);
            AutoDetectMoney(parseTree);
            AutoDetectTime(parseTree);
            var nodeDrawableCollector =
                new NodeDrawableCollector((ParseNodeDrawable) parseTree.GetRoot(), new IsTransferable(secondLanguage));
            var leafList = nodeDrawableCollector.Collect();
            foreach (var parseNode in leafList){
                if (!parseNode.LayerExists(ViewLayerType.NER))
                {
                    parseNode.GetLayerInfo().SetLayerData(ViewLayerType.NER, "NONE");
                }
            }
            parseTree.Save();
        }
    }
}