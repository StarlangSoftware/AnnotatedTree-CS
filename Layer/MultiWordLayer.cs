using System.Collections.Generic;

namespace AnnotatedTree.Layer
{
    public abstract class MultiWordLayer<T> : WordLayer
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

        public int Size()
        {
            return items.Count;
        }

        public abstract void SetLayerValue(string layerValue);
    }
}