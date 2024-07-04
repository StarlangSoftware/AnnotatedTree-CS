namespace AnnotatedTree.Layer
{
    public class EnglishSemanticLayer : SingleWordLayer<string>
    {
        /// <summary>
        /// Constructor for the semantic layer for English language. Sets the layer value to the synset id defined in English
        /// WordNet.
        /// </summary>
        /// <param name="layerValue">Value for the English semantic layer.</param>
        public EnglishSemanticLayer(string layerValue)
        {
            LayerName = "englishSemantics";
            SetLayerValue(layerValue);
        }
    }
}