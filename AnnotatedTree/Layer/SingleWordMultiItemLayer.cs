using System.Collections.Generic;
using AnnotatedSentence;

namespace AnnotatedTree.Layer
{
    public abstract class SingleWordMultiItemLayer<T> : SingleWordLayer<T>
    {
        protected List<T> items = new List<T>();

        /// <summary>
        /// Returns the property at position index for the word.
        /// </summary>
        /// <param name="index">Position of the property</param>
        /// <returns>The property at position index for the word.</returns>
        public T GetItemAt(int index)
        {
            if (index < items.Count)
            {
                return items[index];
            }

            return default(T);
        }

        /// <summary>
        /// Returns the total number of properties for the word in the node.
        /// </summary>
        /// <param name="viewLayer">Not used.</param>
        /// <returns>Total number of properties for the word in the node.</returns>
        public int GetLayerSize(ViewLayerType viewLayer)
        {
            return items.Count;
        }
    }
}