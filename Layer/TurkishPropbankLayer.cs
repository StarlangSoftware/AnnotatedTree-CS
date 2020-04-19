using PropBank;

namespace AnnotatedTree.Layer
{
    public class TurkishPropbankLayer : SingleWordLayer<Argument>
    {
        private Argument _propbank;

        public TurkishPropbankLayer(string layerValue)
        {
            layerName = "propBank";
            SetLayerValue(layerValue);
        }

        public new void SetLayerValue(string layerValue)
        {
            this.layerValue = layerValue;
            _propbank = new Argument(layerValue);
        }

        public Argument GetArgument()
        {
            return _propbank;
        }

        public new string GetLayerValue()
        {
            return _propbank.GetArgumentType() + "$" + _propbank.GetId();
        }
    }
}