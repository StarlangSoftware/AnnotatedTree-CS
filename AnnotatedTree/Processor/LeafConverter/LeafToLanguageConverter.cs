using AnnotatedSentence;

namespace AnnotatedTree.Processor.LeafConverter
{
    public class LeafToLanguageConverter : LeafToStringConverter
    {
        protected ViewLayerType viewLayerType;

        /// <summary>
        /// Converts the data in the leaf node to string, except shortcuts to parentheses are converted to its normal forms,
        /// '*', '0', '-NONE-' are converted to empty string.
        /// </summary>
        /// <param name="leafNode">Node to be converted to string.</param>
        /// <returns>String form of the data, except shortcuts to parentheses are converted to its normal forms,
        /// '*', '0', '-NONE-' are converted to empty string.</returns>
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