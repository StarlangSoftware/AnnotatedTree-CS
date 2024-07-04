using System.Collections.Generic;

namespace AnnotatedTree.Layer
{
    public abstract class MultiWordLayer<T> : WordLayer
    {
        protected List<T> items = new List<T>();

        /// <summary>
        /// Returns the item (word or its property) at position index.
        /// </summary>
        /// <param name="index">Position of the item (word or its property).</param>
        /// <returns>The item at position index.</returns>
        public T GetItemAt(int index)
        {
            if (index < items.Count)
            {
                return items[index];
            }

            return default(T);
        }

        /// <summary>
        /// Returns number of items (words) in the items array list.
        /// </summary>
        /// <returns>Number of items (words) in the items array list.</returns>
        public int Size()
        {
            return items.Count;
        }

        public abstract void SetLayerValue(string layerValue);
    }
}