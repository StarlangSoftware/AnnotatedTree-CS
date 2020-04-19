namespace AnnotatedTree.Layer
{
    public class DependencyLayer : SingleWordLayer<string>
    {
        public DependencyLayer(string layerValue) {
            layerName = "dependency";
            SetLayerValue(layerValue);
        }
        
    }
}