using System.Collections.Generic;
using AnnotatedSentence;
using MorphologicalAnalysis;

namespace AnnotatedTree.Layer
{
    public class MetaMorphemesMovedLayer : MultiWordMultiItemLayer<MetamorphicParse>
    {
        public MetaMorphemesMovedLayer(string layerValue)
        {
            layerName = "metaMorphemesMoved";
            SetLayerValue(layerValue);
        }

        public sealed override void SetLayerValue(string layerValue)
        {
            items = new List<MetamorphicParse>();
            this.layerValue = layerValue;
            if (layerValue != null)
            {
                string[] splitWords = layerValue.Split(" ");
                foreach (var word in splitWords){
                    items.Add(new MetamorphicParse(word));
                }
            }
        }

        public override int GetLayerSize(ViewLayerType viewLayer)
        {
            var size = 0;
            foreach (var parse in items){
                size += parse.Size();
            }
            return size;
        }

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