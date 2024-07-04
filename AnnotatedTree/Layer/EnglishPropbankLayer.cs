using System.Collections.Generic;
using PropBank;

namespace AnnotatedTree.Layer
{
    public class EnglishPropbankLayer : SingleWordMultiItemLayer<Argument>
    {
        /// <summary>
        /// Constructor for the propbank layer for English language.
        /// </summary>
        /// <param name="layerValue">Value for the English propbank layer.</param>
        public EnglishPropbankLayer(string layerValue)
        {
            LayerName = "englishPropbank";
            SetLayerValue(layerValue);
        }

        /// <summary>
        /// Sets the value for the propbank layer in a node. Value may consist of multiple propbank information separated via
        /// '#' character. Each propbank value consists of argumentType and id info separated via '$' character.
        /// </summary>
        /// <param name="layerValue">New layer info</param>
        public new void SetLayerValue(string layerValue)
        {
            items = new List<Argument>();
            this.LayerValue = layerValue;
            if (layerValue != null)
            {
                var splitWords = layerValue.Split("#");
                foreach (var word in splitWords){
                    items.Add(new Argument(word));
                }
            }
        }
    }
}