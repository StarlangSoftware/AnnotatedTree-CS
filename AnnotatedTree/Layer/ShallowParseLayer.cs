using System.Collections.Generic;
using System.Linq;

namespace AnnotatedTree.Layer
{
    public class ShallowParseLayer : MultiWordLayer<string>
    {
        /// <summary>
        /// Constructor for the shallow parse layer. Sets shallow parse information for each word in
        /// the node.
        /// </summary>
        /// <param name="layerValue">Layer value for the shallow parse information. Consists of shallow parse information
        ///                   for every word.</param>
        public ShallowParseLayer(string layerValue)
        {
            LayerName = "shallowParse";
            SetLayerValue(layerValue);
        }

        /// <summary>
        /// Sets the value for the shallow parse layer in a node. Value may consist of multiple shallow parse information
        /// separated via space character. Each shallow parse value is a string.
        /// </summary>
        /// <param name="layerValue">New layer info</param>
        public sealed override void SetLayerValue(string layerValue)
        {
            this.items = new List<string>();
            this.LayerValue = layerValue;
            if (layerValue != null)
            {
                var splitParse = layerValue.Split(" ");
                items = items.Union(splitParse).ToList();
            }
        }
    }
}