namespace AnnotatedTree.Layer
{
    public abstract class SingleWordLayer<T> : WordLayer
    {
        /// <summary>
        /// Sets the property of the word
        /// </summary>
        /// <param name="layerValue">Layer info</param>
        public void SetLayerValue(string layerValue){
            this.LayerValue = layerValue;
        }
        
    }
}