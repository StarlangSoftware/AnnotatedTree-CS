using System.Collections.Generic;
using AnnotatedSentence;
using MorphologicalAnalysis;

namespace AnnotatedTree.Layer
{
    public class MetaMorphemesMovedLayer : MultiWordMultiItemLayer<MetamorphicParse>
    {
        /// <summary>
        /// Constructor for the metaMorphemesMoved layer. Sets the metamorpheme information for multiple words in the node.
        /// </summary>
        /// <param name="layerValue">Layer value for the metaMorphemesMoved information. Consists of metamorpheme information of
        ///                   multiple words separated via space character.</param>
        public MetaMorphemesMovedLayer(string layerValue)
        {
            LayerName = "metaMorphemesMoved";
            SetLayerValue(layerValue);
        }

        /// <summary>
        /// Sets the layer value to the string form of the given parse.
        /// </summary>
        /// <param name="layerValue">New metamorphic parse.</param>
        public sealed override void SetLayerValue(string layerValue)
        {
            items = new List<MetamorphicParse>();
            this.LayerValue = layerValue;
            if (layerValue != null)
            {
                string[] splitWords = layerValue.Split(" ");
                foreach (var word in splitWords){
                    items.Add(new MetamorphicParse(word));
                }
            }
        }

        /// <summary>
        /// Returns the total number of metamorphemes in the words in the node.
        /// </summary>
        /// <param name="viewLayer">Not used.</param>
        /// <returns>Total number of metamorphemes in the words in the node.</returns>
        public override int GetLayerSize(ViewLayerType viewLayer)
        {
            var size = 0;
            foreach (var parse in items){
                size += parse.Size();
            }
            return size;
        }

        /// <summary>
        /// Returns the metamorpheme at position index in the metamorpheme list.
        /// </summary>
        /// <param name="viewLayer">Not used.</param>
        /// <param name="index">Position in the metamorpheme list.</param>
        /// <returns>The metamorpheme at position index in the metamorpheme list.</returns>
        public override string GetLayerInfoAt(ViewLayerType viewLayer, int index)
        {
            var size = 0;
            foreach (var parse in items){
                if (index < size + parse.Size())
                {
                    return parse.GetMetaMorpheme(index - size);
                }

                size += parse.Size();
            }
            return null;
        }
    }
}