namespace AnnotatedTree.Layer
{
    public abstract class SingleWordLayer<T> : WordLayer
    {
        public void SetLayerValue(string layerValue){
            this.layerValue = layerValue;
        }
        
    }
}