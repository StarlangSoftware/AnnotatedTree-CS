using AnnotatedSentence;

namespace AnnotatedTree.Processor.LeafConverter
{
    public class LeafToLanguageConverter : LeafToStringConverter
    {
        protected ViewLayerType viewLayerType;

        public string LeafConverter(ParseNodeDrawable leafNode)
        {
            var layerData = leafNode.GetLayerData(viewLayerType);
            var parentLayerData = ((ParseNodeDrawable) leafNode.GetParent()).GetLayerData(viewLayerType);
            if (layerData != null)
            {
                if (layerData.Contains("*") || (layerData.Equals("0") && parentLayerData.Equals("-NONE-")))
                {
                    return "";
                }

                return " " + layerData.Replace("-LRB-", "(").Replace("-RRB-", ")").Replace("-LSB-", "[")
                           .Replace("-RSB-", "]").Replace("-LCB-", "{").Replace("-RCB-", "}")
                           .Replace("-lrb-", "(").Replace("-rrb-", ")").Replace("-lsb-", "[")
                           .Replace("-rsb-", "]").Replace("-lcb-", "{").Replace("-rcb-", "}");
            }

            return "";
        }
    }
}