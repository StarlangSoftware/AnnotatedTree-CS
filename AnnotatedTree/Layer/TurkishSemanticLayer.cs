using System.Collections.Generic;
using System.Linq;

namespace AnnotatedTree.Layer
{
    public class TurkishSemanticLayer : MultiWordLayer<string>
    {
        /// <summary>
        /// Constructor for the Turkish semantic layer. Sets semantic information for each word in
        /// the node.
        /// </summary>
        /// <param name="layerValue">Layer value for the Turkish semantic information. Consists of semantic (Turkish synset id)
        ///                   information for every word.</param>
        public TurkishSemanticLayer(string layerValue)
        {
            LayerName = "semantics";
            SetLayerValue(layerValue);
        }

        /// <summary>
        /// Sets the value for the Turkish semantic layer in a node. Value may consist of multiple sense information
        /// separated via '$' character. Each sense value is a string representing the synset id of the sense.
        /// </summary>
        /// <param name="layerValue">New layer info</param>
        public sealed override void SetLayerValue(string layerValue)
        {
            this.items = new List<string>();
            this.LayerValue = layerValue;
            if (layerValue != null)
            {
                var splitMeanings = layerValue.Split("$");
                items = items.Union(splitMeanings).ToList();
            }
        }
    }
}