namespace AnnotatedTree.Layer
{
    public class EnglishWordLayer : SourceLanguageWordLayer
    {
        /// <summary>
        /// Constructor for the word layer for English language. Sets the surface form.
        /// </summary>
        /// <param name="layerValue">Value for the word layer.</param>
        public EnglishWordLayer(string layerValue) : base(layerValue)
        {
            LayerName = "english";
        }
    }
}