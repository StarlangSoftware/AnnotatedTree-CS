using System.Collections.Generic;
using AnnotatedSentence;
using MorphologicalAnalysis;

namespace AnnotatedTree.Layer
{
    public class MetaMorphemeLayer : MetaMorphemesMovedLayer
    {
        /// <summary>
        /// Constructor for the metamorpheme layer. Sets the metamorpheme information for multiple words in the node.
        /// </summary>
        /// <param name="layerValue">Layer value for the metamorpheme information. Consists of metamorpheme information of multiple
        ///                   words separated via space character.</param>
        public MetaMorphemeLayer(string layerValue) : base(layerValue)
        {
            LayerName = "metaMorphemes";
        }

        /// <summary>
        /// Sets the layer value to the string form of the given parse.
        /// </summary>
        /// <param name="parse">New metamorphic parse.</param>
        public void SetLayerValue(MetamorphicParse parse)
        {
            LayerValue = parse.ToString();
            items = new List<MetamorphicParse>();
            if (LayerValue != null)
            {
                var splitWords = LayerValue.Split(" ");
                foreach (var word in splitWords)
                {
                    items.Add(new MetamorphicParse(word));
                }
            }
        }

        /// <summary>
        /// Constructs metamorpheme information starting from the position index.
        /// </summary>
        /// <param name="index">Position of the morpheme to start.</param>
        /// <returns>Metamorpheme information starting from the position index.</returns>
        public string GetLayerInfoFrom(int index)
        {
            var size = 0;
            foreach (var parse in items)
            {
                if (index < size + parse.Size())
                {
                    var result = parse.GetMetaMorpheme(index - size);
                    index++;
                    while (index < size + parse.Size())
                    {
                        result = result + "+" + parse.GetMetaMorpheme(index - size);
                        index++;
                    }

                    return result;
                }

                size += parse.Size();
            }

            return null;
        }

        /// <summary>
        /// Removes metamorphemes from the given index. Index shows the position of the metamorpheme in the metamorphemes list.
        /// </summary>
        /// <param name="index">Position of the metamorpheme from which the other metamorphemes will be removed.</param>
        /// <returns>New metamorphic parse not containing the removed parts.</returns>
        public MetamorphicParse MetaMorphemeRemoveFromIndex(int index)
        {
            if (index >= 0 && index < GetLayerSize(ViewLayerType.META_MORPHEME))
            {
                var size = 0;
                foreach (var parse in items)
                {
                    if (index < size + parse.Size())
                    {
                        parse.RemoveMetaMorphemeFromIndex(index - size);
                        return parse;
                    }

                    size += parse.Size();
                }
            }

            return null;
        }
    }
}