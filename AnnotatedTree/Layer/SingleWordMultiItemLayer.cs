using System.Collections.Generic;
using AnnotatedSentence;

namespace AnnotatedTree.Layer
{
    public abstract class SingleWordMultiItemLayer<T> : SingleWordLayer<T>
    {
        protected List<T> items = new List<T>();

        public T GetItemAt(int index)
        {
            if (index < items.Count)
            {
                return items[index];
            }

            return default(T);
        }

        public int GetLayerSize(ViewLayerType viewLayer)
        {
            return items.Count;
        }
    }
}