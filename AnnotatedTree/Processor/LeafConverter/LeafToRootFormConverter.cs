namespace AnnotatedTree.Processor.LeafConverter
{
    public class LeafToRootFormConverter : LeafToStringConverter
    {
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