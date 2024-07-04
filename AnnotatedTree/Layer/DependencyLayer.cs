namespace AnnotatedTree.Layer
{
    public class DependencyLayer : SingleWordLayer<string>
    {
        /// <summary>
        /// Constructor for the dependency layer. Dependency layer stores the dependency information of a node.
        /// </summary>
        /// <param name="layerValue">Value of the dependency layer.</param>
        public DependencyLayer(string layerValue) {
            LayerName = "dependency";
            SetLayerValue(layerValue);
        }
        
    }
}