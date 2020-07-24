namespace AnnotatedTree.Layer
{
    public abstract class WordLayer
    {
        protected string layerValue;
        protected string layerName;

        public string GetLayerValue()
        {
            return layerValue;
        }

        public string GetLayerName()
        {
            return layerName;
        }

        public string GetLayerDescription()
        {
            return "{" + layerName + "=" + layerValue + "}";
        }
    }
}