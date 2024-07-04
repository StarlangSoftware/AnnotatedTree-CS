using System.Collections.Generic;
using AnnotatedSentence;
using MorphologicalAnalysis;

namespace AnnotatedTree.Layer
{
    public class MorphologicalAnalysisLayer : MultiWordMultiItemLayer<MorphologicalParse>
    {
        /// <summary>
        /// Constructor for the morphological analysis layer. Sets the morphological parse information for multiple words in
        /// the node.
        /// </summary>
        /// <param name="layerValue">Layer value for the morphological parse information. Consists of morphological parse information
        ///                   of multiple words separated via space character.</param>
        public MorphologicalAnalysisLayer(string layerValue)
        {
            LayerName = "morphologicalAnalysis";
            SetLayerValue(layerValue);
        }

        /// <summary>
        /// Sets the layer value to the string form of the given morphological parse.
        /// </summary>
        /// <param name="layerValue">New morphological parse.</param>
        public override void SetLayerValue(string layerValue)
        {
            this.items = new List<MorphologicalParse>();
            this.LayerValue = layerValue;
            if (layerValue != null)
            {
                var splitWords = layerValue.Split(" ");
                foreach (var word in splitWords)
                {
                    items.Add(new MorphologicalParse(word));
                }
            }
        }

        /// <summary>
        /// Sets the layer value to the string form of the given morphological parse.
        /// </summary>
        /// <param name="parse">New morphological parse.</param>
        public void SetLayerValue(MorphologicalParse parse)
        {
            LayerValue = parse.GetTransitionList();
            items = new List<MorphologicalParse>();
            items.Add(parse);
        }

        /// <summary>
        ///  Returns the total number of morphological tags (for PART_OF_SPEECH) or inflectional groups
        /// (for INFLECTIONAL_GROUP) in the words in the node.
        /// </summary>
        /// <param name="viewLayer">Layer type.</param>
        /// <returns>Total number of morphological tags (for PART_OF_SPEECH) or inflectional groups (for INFLECTIONAL_GROUP)
        /// in the words in the node.</returns>
        public override int GetLayerSize(ViewLayerType viewLayer)
        {
            int size;
            switch (viewLayer)
            {
                case ViewLayerType.PART_OF_SPEECH:
                    size = 0;
                    foreach (var parse in items)
                    {
                        size += parse.TagSize();
                    }

                    return size;
                case ViewLayerType.INFLECTIONAL_GROUP:
                    size = 0;
                    foreach (var parse in items)
                    {
                        size += parse.Size();
                    }

                    return size;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Returns the morphological tag (for PART_OF_SPEECH) or inflectional group (for INFLECTIONAL_GROUP) at position
        /// index.
        /// </summary>
        /// <param name="viewLayer">Layer type.</param>
        /// <param name="index">Position of the morphological tag (for PART_OF_SPEECH) or inflectional group (for INFLECTIONAL_GROUP)</param>
        /// <returns>The morphological tag (for PART_OF_SPEECH) or inflectional group (for INFLECTIONAL_GROUP)</returns>
        public override string GetLayerInfoAt(ViewLayerType viewLayer, int index)
        {
            int size;
            switch (viewLayer)
            {
                case ViewLayerType.PART_OF_SPEECH:
                    size = 0;
                    foreach (var parse in items)
                    {
                        if (index < size + parse.TagSize())
                        {
                            return parse.GetTag(index - size);
                        }

                        size += parse.TagSize();
                    }

                    return null;
                case ViewLayerType.INFLECTIONAL_GROUP:
                    size = 0;
                    foreach (var parse in items)
                    {
                        if (index < size + parse.Size())
                        {
                            return parse.GetInflectionalGroupString(index - size);
                        }

                        size += parse.Size();
                    }

                    return null;
            }

            return null;
        }

        /// <summary>
        /// Checks if the last inflectional group contains VERB tag.
        /// </summary>
        /// <returns>True if the last inflectional group contains VERB tag, false otherwise.</returns>
        public bool IsVerbal()
        {
            const string dbLabel = "^DB+";
            const string needle = "VERB+";
            string haystack;
            if (LayerValue.Contains(dbLabel))
                haystack = LayerValue.Substring(LayerValue.LastIndexOf(dbLabel) + 4);
            else
                haystack = LayerValue;
            return haystack.Contains(needle);
        }

        /// <summary>
        /// Checks if the last verbal inflectional group contains ZERO tag.
        /// </summary>
        /// <returns>True if the last verbal inflectional group contains ZERO tag, false otherwise.</returns>
        public bool IsNominal()
        {
            const string dbLabel = "^DB+VERB+";
            const string needle = "ZERO+";
            string haystack;
            if (LayerValue.Contains(dbLabel))
                haystack = LayerValue.Substring(LayerValue.LastIndexOf(dbLabel) + 9);
            else
                haystack = LayerValue;
            return haystack.Contains(needle);
        }
    }
}