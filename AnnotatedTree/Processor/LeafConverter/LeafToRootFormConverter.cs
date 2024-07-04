namespace AnnotatedTree.Processor.LeafConverter
{
    public class LeafToRootFormConverter : LeafToStringConverter
    {
        /// <summary>
        /// Converts the data in the leaf node to string. If there are multiple words in the leaf node, they are concatenated
        /// with space.
        /// </summary>
        /// <param name="leafNode">Node to be converted to string.</param>
        /// <returns>String form of the data. If there are multiple words in the leaf node, they are concatenated
        /// with space.</returns>
        public string LeafConverter(ParseNodeDrawable leafNode)
        {
            var layerInfo = leafNode.GetLayerInfo();
            var rootWords = " ";
            for (var i = 0; i < layerInfo.GetNumberOfWords(); i++)
            {
                var root = layerInfo.GetMorphologicalParseAt(i).GetWord().GetName();
                if (!string.IsNullOrEmpty(root))
                {
                    rootWords += " " + root;
                }
            }

            return rootWords;
        }
    }
}