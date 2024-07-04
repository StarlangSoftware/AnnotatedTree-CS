namespace AnnotatedTree.Layer
{
    public class PersianWordLayer : TargetLanguageWordLayer
    {
        /// <summary>
        /// Constructor for the word layer for Persian language. Sets the surface form.
        /// </summary>
        /// <param name="layerValue">Value for the word layer.</param>
        public PersianWordLayer(string layerValue) : base(layerValue)
        {
            LayerName = "persian";
        }
    }
}