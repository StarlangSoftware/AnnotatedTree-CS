using System.Collections.Generic;
using System.Linq;
using AnnotatedSentence;

namespace AnnotatedTree.Layer
{
    public abstract class TargetLanguageWordLayer : MultiWordLayer<string>
    {
        public TargetLanguageWordLayer(string layerValue)
        {
            SetLayerValue(layerValue);
        }

        public override void SetLayerValue(string layerValue)
        {
            items = new List<string>();
            this.layerValue = layerValue;
            if (layerValue != null)
            {
                var splitWords = layerValue.Split(" ");
                items = items.Union(splitWords).ToList();
            }
        }

        public int GetLayerSize(ViewLayerType viewLayer)
        {
            return 0;
        }

        public string GetLayerInfoAt(ViewLayerType viewLayer, int index)
        {
            return null;
        }
    }
}