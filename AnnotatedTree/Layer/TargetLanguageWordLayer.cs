using System.Collections.Generic;
using System.Linq;
using AnnotatedSentence;

namespace AnnotatedTree.Layer
{
    public abstract class TargetLanguageWordLayer : MultiWordLayer<string>
    {
        /// <summary>
        /// Sets the surface form(s) of the word(s) possibly separated with space.
        /// </summary>
        /// <param name="layerValue">Surface form(s) of the word(s) possibly separated with space.</param>
        public TargetLanguageWordLayer(string layerValue)
        {
            SetLayerValue(layerValue);
        }

        /// <summary>
        /// Sets the surface form(s) of the word(s). Value may consist of multiple surface form(s)
        /// separated via space character.
        /// </summary>
        /// <param name="layerValue">New layer info</param>
        public override void SetLayerValue(string layerValue)
        {
            items = new List<string>();
            this.LayerValue = layerValue;
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