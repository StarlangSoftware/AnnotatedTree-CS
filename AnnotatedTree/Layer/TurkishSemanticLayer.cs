using System.Collections.Generic;
using System.Linq;

namespace AnnotatedTree.Layer
{
    public class TurkishSemanticLayer : MultiWordLayer<string>
    {
        public TurkishSemanticLayer(string layerValue)
        {
            layerName = "semantics";
            SetLayerValue(layerValue);
        }

        public sealed override void SetLayerValue(string layerValue)
        {
            this.items = new List<string>();
            this.layerValue = layerValue;
            if (layerValue != null)
            {
                var splitMeanings = layerValue.Split("$");
                items = items.Union(splitMeanings).ToList();
            }
        }
    }
}