using NamedEntityRecognition;

namespace AnnotatedTree.Layer
{
    public class NERLayer : SingleWordLayer<NamedEntityType>
    {
        private NamedEntityType _namedEntity;

        public NERLayer(string layerValue)
        {
            layerName = "namedEntity";
            SetLayerValue(layerValue);
        }

        public new void SetLayerValue(string layerValue)
        {
            this.layerValue = layerValue;
            _namedEntity = NamedEntityTypeStatic.GetNamedEntityType(layerValue);
        }

        public new string GetLayerValue()
        {
            return NamedEntityTypeStatic.GetNamedEntityType(_namedEntity);
        }
    }
}