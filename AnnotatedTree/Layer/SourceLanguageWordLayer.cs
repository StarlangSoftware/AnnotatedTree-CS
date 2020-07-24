namespace AnnotatedTree.Layer
{
    public abstract class SourceLanguageWordLayer : SingleWordLayer<string>
    {
        public SourceLanguageWordLayer(string layerValue)
        {
            SetLayerValue(layerValue);
        }
    }
}