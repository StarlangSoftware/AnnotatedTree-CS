using System.Collections.Generic;
using PropBank;

namespace AnnotatedTree.Layer
{
    public class EnglishPropbankLayer : SingleWordMultiItemLayer<Argument>
    {
        public EnglishPropbankLayer(string layerValue)
        {
            layerName = "englishPropbank";
            SetLayerValue(layerValue);
        }

        public new void SetLayerValue(string layerValue)
        {
            items = new List<Argument>();
            this.layerValue = layerValue;
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