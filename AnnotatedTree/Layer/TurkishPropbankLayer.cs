using PropBank;

namespace AnnotatedTree.Layer
{
    public class TurkishPropbankLayer : SingleWordLayer<Argument>
    {
        private Argument _propbank;

        /// <summary>
        /// Constructor for the Turkish propbank layer. Sets single semantic role information for multiple words in
        /// the node.
        /// </summary>
        /// <param name="layerValue">Layer value for the propbank information. Consists of semantic role information
        ///                   of multiple words.</param>
        public TurkishPropbankLayer(string layerValue)
        {
            LayerName = "propBank";
            SetLayerValue(layerValue);
        }

        /// <summary>
        /// Sets the layer value for Turkish propbank layer. Converts the string form to an Argument.
        /// </summary>
        /// <param name="layerValue">New value for Turkish propbank layer.</param>
        public new void SetLayerValue(string layerValue)
        {
            this.LayerValue = layerValue;
            _propbank = new Argument(layerValue);
        }

        /// <summary>
        /// Accessor for the propbank field.
        /// </summary>
        /// <returns>Propbank field.</returns>
        public Argument GetArgument()
        {
            return _propbank;
        }

        /// <summary>
        /// Another accessor for the propbank field.
        /// </summary>
        /// <returns>String form of the propbank field.</returns>
        public new string GetLayerValue()
        {
            return _propbank.GetArgumentType() + "$" + _propbank.GetId();
        }
    }
}