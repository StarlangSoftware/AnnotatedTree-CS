namespace AnnotatedTree.Layer
{
    public abstract class SourceLanguageWordLayer : SingleWordLayer<string>
    {
        /// <summary>
        /// Sets the name of the word
        /// </summary>
        /// <param name="layerValue">Name of the word</param>
        public SourceLanguageWordLayer(string layerValue)
        {
            SetLayerValue(layerValue);
        }
    }
}