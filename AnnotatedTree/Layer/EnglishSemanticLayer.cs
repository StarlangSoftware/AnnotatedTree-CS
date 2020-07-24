namespace AnnotatedTree.Layer
{
    public class EnglishSemanticLayer : SingleWordLayer<string>
    {
        public EnglishSemanticLayer(string layerValue)
        {
            layerName = "englishSemantics";
            SetLayerValue(layerValue);
        }
    }
}