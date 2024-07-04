namespace AnnotatedTree.Layer
{
    public class TurkishWordLayer : TargetLanguageWordLayer
    {
        /// <summary>
        /// Constructor for the word layer for Turkish language. Sets the surface form.
        /// </summary>
        /// <param name="layerValue">Value for the word layer.</param>
        public TurkishWordLayer(string layerValue) : base(layerValue)
        {
            LayerName = "turkish";
        }
    }
}