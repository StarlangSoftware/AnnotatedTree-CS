using System.Collections.Generic;
using AnnotatedSentence;
using MorphologicalAnalysis;

namespace AnnotatedTree.Layer
{
    public class MorphologicalAnalysisLayer : MultiWordMultiItemLayer<MorphologicalParse>
    {
        public MorphologicalAnalysisLayer(string layerValue)
        {
            layerName = "morphologicalAnalysis";
            SetLayerValue(layerValue);
        }

        public override void SetLayerValue(string layerValue)
        {
            this.items = new List<MorphologicalParse>();
            this.layerValue = layerValue;
            if (layerValue != null)
            {
                var splitWords = layerValue.Split(" ");
                foreach (var word in splitWords)
                {
                    items.Add(new MorphologicalParse(word));
                }
            }
        }

        public void SetLayerValue(MorphologicalParse parse)
        {
            layerValue = parse.GetTransitionList();
            items = new List<MorphologicalParse>();
            items.Add(parse);
        }

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

        public bool IsVerbal()
        {
            const string dbLabel = "^DB+";
            const string needle = "VERB+";
            string haystack;
            if (layerValue.Contains(dbLabel))
                haystack = layerValue.Substring(layerValue.LastIndexOf(dbLabel) + 4);
            else
                haystack = layerValue;
            return haystack.Contains(needle);
        }

        public bool IsNominal()
        {
            const string dbLabel = "^DB+VERB+";
            const string needle = "ZERO+";
            string haystack;
            if (layerValue.Contains(dbLabel))
                haystack = layerValue.Substring(layerValue.LastIndexOf(dbLabel) + 9);
            else
                haystack = layerValue;
            return haystack.Contains(needle);
        }
    }
}