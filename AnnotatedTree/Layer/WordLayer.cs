namespace AnnotatedTree.Layer
{
    public abstract class WordLayer
    {
        protected string LayerValue;
        protected string LayerName;

        /// <summary>
        /// Accessor for the layerValue attribute.
        /// </summary>
        /// <returns>LayerValue attribute.</returns>
        public string GetLayerValue()
        {
            return LayerValue;
        }

        /// <summary>
        /// Accessor for the layerName attribute.
        /// </summary>
        /// <returns>LayerName attribute.</returns>
        public string GetLayerName()
        {
            return LayerName;
        }

        /// <summary>
        /// Returns string form of the word layer.
        /// </summary>
        /// <returns>String form of the word layer.</returns>
        public string GetLayerDescription()
        {
            return "{" + LayerName + "=" + LayerValue + "}";
        }
    }
}