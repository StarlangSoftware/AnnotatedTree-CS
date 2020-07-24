using System.Collections.Generic;
using AnnotatedSentence;
using MorphologicalAnalysis;

namespace AnnotatedTree.Layer
{
    public class MetaMorphemeLayer : MetaMorphemesMovedLayer
    {
        public MetaMorphemeLayer(string layerValue) : base(layerValue)
        {
            layerName = "metaMorphemes";
        }

        public void SetLayerValue(MetamorphicParse parse)
        {
            layerValue = parse.ToString();
            items = new List<MetamorphicParse>();
            if (layerValue != null)
            {
                var splitWords = layerValue.Split(" ");
                foreach (var word in splitWords)
                {
                    items.Add(new MetamorphicParse(word));
                }
            }
        }

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