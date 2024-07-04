using NamedEntityRecognition;

namespace AnnotatedTree.Layer
{
    public class NERLayer : SingleWordLayer<NamedEntityType>
    {
        private NamedEntityType _namedEntity;

        /// <summary>
        /// Constructor for the named entity layer. Sets single named entity information for multiple words in
        /// the node.
        /// </summary>
        /// <param name="layerValue">Layer value for the named entity information. Consists of single named entity information
        ///                   of multiple words.</param>
        public NERLayer(string layerValue)
        {
            LayerName = "namedEntity";
            SetLayerValue(layerValue);
        }

        /// <summary>
        /// Sets the layer value for Named Entity layer. Converts the string form to a named entity.
        /// </summary>
        /// <param name="layerValue">New value for Named Entity layer.</param>
        public new void SetLayerValue(string layerValue)
        {
            this.LayerValue = layerValue;
            _namedEntity = NamedEntityTypeStatic.GetNamedEntityType(layerValue);
        }

        /// <summary>
        /// Get the string form of the named entity value. Converts named entity type to string form.
        /// </summary>
        /// <returns>String form of the named entity value.</returns>
        public new string GetLayerValue()
        {
            return NamedEntityTypeStatic.GetNamedEntityType(_namedEntity);
        }
    }
}