using System.Collections.Generic;
using System.Linq;

namespace AnnotatedTree.Layer
{
    public class ShallowParseLayer : MultiWordLayer<string>
    {
        public ShallowParseLayer(string layerValue)
        {
            layerName = "shallowParse";
            SetLayerValue(layerValue);
        }

        public sealed override void SetLayerValue(string layerValue)
        {
            this.items = new List<string>();
            this.layerValue = layerValue;
            if (layerValue != null)
            {
                var splitParse = layerValue.Split(" ");
                items = items.Union(splitParse).ToList();
            }
        }
    }
}