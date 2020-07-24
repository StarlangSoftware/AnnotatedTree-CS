using AnnotatedSentence;

namespace AnnotatedTree.Layer
{
    public abstract class MultiWordMultiItemLayer<T> : MultiWordLayer<T>
    {
        public abstract int GetLayerSize(ViewLayerType viewLayer);
        public abstract string GetLayerInfoAt(ViewLayerType viewLayer, int index);
        
    }
}