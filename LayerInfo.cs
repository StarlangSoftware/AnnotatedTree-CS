using System;
using System.Collections.Generic;
using AnnotatedSentence;
using AnnotatedTree.Layer;
using MorphologicalAnalysis;
using PropBank;

namespace AnnotatedTree
{
    public class LayerInfo : ICloneable
    {
        private readonly Dictionary<ViewLayerType, WordLayer> _layers;

        public LayerInfo(string info)
        {
            var splitLayers = info.Split('[', ']', '{', '}');
            _layers = new Dictionary<ViewLayerType, WordLayer>();
            foreach (var layer in splitLayers)
            {
                if (layer == "")
                    continue;
                var layerType = layer.Substring(0, layer.IndexOf("="));
                var layerValue = layer.Substring(layer.IndexOf("=") + 1);
                if (layerType.Equals("turkish"))
                {
                    _layers[ViewLayerType.TURKISH_WORD] = new TurkishWordLayer(layerValue);
                }
                else
                {
                    if (layerType.Equals("persian"))
                    {
                        _layers[ViewLayerType.PERSIAN_WORD] = new PersianWordLayer(layerValue);
                    }
                    else
                    {
                        if (layerType.Equals("english"))
                        {
                            _layers[ViewLayerType.ENGLISH_WORD] = new EnglishWordLayer(layerValue);
                        }
                        else
                        {
                            if (layerType.Equals("morphologicalAnalysis"))
                            {
                                _layers[ViewLayerType.INFLECTIONAL_GROUP] =
                                    new MorphologicalAnalysisLayer(layerValue);
                                _layers[ViewLayerType.PART_OF_SPEECH] = new MorphologicalAnalysisLayer(layerValue);
                            }
                            else
                            {
                                if (layerType.Equals("metaMorphemes"))
                                {
                                    _layers[ViewLayerType.META_MORPHEME] = new MetaMorphemeLayer(layerValue);
                                }
                                else
                                {
                                    if (layerType.Equals("metaMorphemesMoved"))
                                    {
                                        _layers[ViewLayerType.META_MORPHEME_MOVED] =
                                            new MetaMorphemesMovedLayer(layerValue);
                                    }
                                    else
                                    {
                                        if (layerType.Equals("dependency"))
                                        {
                                            _layers[ViewLayerType.DEPENDENCY] = new DependencyLayer(layerValue);
                                        }
                                        else
                                        {
                                            if (layerType.Equals("semantics"))
                                            {
                                                _layers[ViewLayerType.SEMANTICS] = new TurkishSemanticLayer(layerValue);
                                            }
                                            else
                                            {
                                                if (layerType.Equals("namedEntity"))
                                                {
                                                    _layers[ViewLayerType.NER] = new NERLayer(layerValue);
                                                }
                                                else
                                                {
                                                    if (layerType.Equals("propBank"))
                                                    {
                                                        _layers[ViewLayerType.PROPBANK] =
                                                            new TurkishPropbankLayer(layerValue);
                                                    }
                                                    else
                                                    {
                                                        if (layerType.Equals("englishPropbank"))
                                                        {
                                                            _layers[ViewLayerType.ENGLISH_PROPBANK] =
                                                                new EnglishPropbankLayer(layerValue);
                                                        }
                                                        else
                                                        {
                                                            if (layerType.Equals("englishSemantics"))
                                                            {
                                                                _layers[ViewLayerType.ENGLISH_SEMANTICS] =
                                                                    new EnglishSemanticLayer(layerValue);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public LayerInfo()
        {
            _layers = new Dictionary<ViewLayerType, WordLayer>();
        }

        public object Clone()
        {
            return new LayerInfo(GetLayerDescription());
        }

        public void SetLayerData(ViewLayerType viewLayer, string layerValue)
        {
            switch (viewLayer)
            {
                case ViewLayerType.PERSIAN_WORD:
                    _layers[ViewLayerType.PERSIAN_WORD] = new PersianWordLayer(layerValue);
                    _layers.Remove(ViewLayerType.SEMANTICS);
                    break;
                case ViewLayerType.TURKISH_WORD:
                    _layers[ViewLayerType.TURKISH_WORD] = new TurkishWordLayer(layerValue);
                    _layers.Remove(ViewLayerType.INFLECTIONAL_GROUP);
                    _layers.Remove(ViewLayerType.PART_OF_SPEECH);
                    _layers.Remove(ViewLayerType.META_MORPHEME);
                    _layers.Remove(ViewLayerType.META_MORPHEME_MOVED);
                    _layers.Remove(ViewLayerType.SEMANTICS);
                    break;
                case ViewLayerType.ENGLISH_WORD:
                    _layers[ViewLayerType.ENGLISH_WORD] = new EnglishWordLayer(layerValue);
                    break;
                case ViewLayerType.PART_OF_SPEECH:
                case ViewLayerType.INFLECTIONAL_GROUP:
                    _layers[ViewLayerType.INFLECTIONAL_GROUP] = new MorphologicalAnalysisLayer(layerValue);
                    _layers[ViewLayerType.PART_OF_SPEECH] = new MorphologicalAnalysisLayer(layerValue);
                    _layers.Remove(ViewLayerType.META_MORPHEME_MOVED);
                    break;
                case ViewLayerType.META_MORPHEME:
                    _layers[ViewLayerType.META_MORPHEME] = new MetaMorphemeLayer(layerValue);
                    break;
                case ViewLayerType.META_MORPHEME_MOVED:
                    _layers[ViewLayerType.META_MORPHEME_MOVED] = new MetaMorphemesMovedLayer(layerValue);
                    break;
                case ViewLayerType.DEPENDENCY:
                    _layers[ViewLayerType.DEPENDENCY] = new DependencyLayer(layerValue);
                    break;
                case ViewLayerType.SEMANTICS:
                    _layers[ViewLayerType.SEMANTICS] = new TurkishSemanticLayer(layerValue);
                    break;
                case ViewLayerType.ENGLISH_SEMANTICS:
                    _layers[ViewLayerType.ENGLISH_SEMANTICS] = new EnglishSemanticLayer(layerValue);
                    break;
                case ViewLayerType.NER:
                    _layers[ViewLayerType.NER] = new NERLayer(layerValue);
                    break;
                case ViewLayerType.PROPBANK:
                    _layers[ViewLayerType.PROPBANK] = new TurkishPropbankLayer(layerValue);
                    break;
                case ViewLayerType.ENGLISH_PROPBANK:
                    _layers[ViewLayerType.ENGLISH_PROPBANK] = new EnglishPropbankLayer(layerValue);
                    break;
                case ViewLayerType.SHALLOW_PARSE:
                    _layers[ViewLayerType.SHALLOW_PARSE] = new ShallowParseLayer(layerValue);
                    break;
            }
        }

        public void SetMorphologicalAnalysis(MorphologicalParse parse)
        {
            _layers[ViewLayerType.INFLECTIONAL_GROUP] = new MorphologicalAnalysisLayer(parse.ToString());
            _layers[ViewLayerType.PART_OF_SPEECH] = new MorphologicalAnalysisLayer(parse.ToString());
        }

        public void SetMetaMorphemes(MetamorphicParse parse)
        {
            _layers[ViewLayerType.META_MORPHEME] = new MetaMorphemeLayer(parse.ToString());
        }

        public bool LayerExists(ViewLayerType viewLayerType)
        {
            return _layers.ContainsKey(viewLayerType);
        }

        public ViewLayerType CheckLayer(ViewLayerType viewLayer)
        {
            switch (viewLayer)
            {
                case ViewLayerType.TURKISH_WORD:
                case ViewLayerType.PERSIAN_WORD:
                case ViewLayerType.ENGLISH_SEMANTICS:
                    if (!_layers.ContainsKey(viewLayer))
                    {
                        return ViewLayerType.ENGLISH_WORD;
                    }

                    break;
                case ViewLayerType.PART_OF_SPEECH:
                case ViewLayerType.INFLECTIONAL_GROUP:
                case ViewLayerType.META_MORPHEME:
                case ViewLayerType.SEMANTICS:
                case ViewLayerType.NER:
                case ViewLayerType.PROPBANK:
                case ViewLayerType.SHALLOW_PARSE:
                case ViewLayerType.ENGLISH_PROPBANK:
                    if (!_layers.ContainsKey(viewLayer))
                        return CheckLayer(ViewLayerType.TURKISH_WORD);
                    break;
                case ViewLayerType.META_MORPHEME_MOVED:
                    if (!_layers.ContainsKey(viewLayer))
                        return CheckLayer(ViewLayerType.META_MORPHEME);
                    break;
            }

            return viewLayer;
        }

        public int GetNumberOfWords()
        {
            if (_layers.ContainsKey(ViewLayerType.TURKISH_WORD))
            {
                return ((TurkishWordLayer) _layers[ViewLayerType.TURKISH_WORD]).Size();
            }

            if (_layers.ContainsKey(ViewLayerType.PERSIAN_WORD))
            {
                return ((PersianWordLayer) _layers[ViewLayerType.PERSIAN_WORD]).Size();
            }

            return 0;
        }

        private string GetMultiWordAt(ViewLayerType viewLayerType, int index, string layerName)
        {
            if (_layers.ContainsKey(viewLayerType))
            {
                if (_layers[viewLayerType] is MultiWordLayer<string>)
                {
                    var multiWordLayer = (MultiWordLayer<string>) _layers[viewLayerType];
                    if (index < multiWordLayer.Size() && index >= 0)
                    {
                        return multiWordLayer.GetItemAt(index);
                    }

                    if (viewLayerType.Equals(ViewLayerType.SEMANTICS))
                    {
                        return multiWordLayer.GetItemAt(multiWordLayer.Size() - 1);
                    }
                }
            }

            return null;
        }

        public string GetTurkishWordAt(int index)
        {
            return GetMultiWordAt(ViewLayerType.TURKISH_WORD, index, "turkish");
        }

        public int GetNumberOfMeanings()
        {
            if (_layers.ContainsKey(ViewLayerType.SEMANTICS))
            {
                return ((TurkishSemanticLayer) _layers[ViewLayerType.SEMANTICS]).Size();
            }

            return 0;
        }

        public string GetSemanticAt(int index)
        {
            return GetMultiWordAt(ViewLayerType.SEMANTICS, index, "semantics");
        }

        public string GetShallowParseAt(int index)
        {
            return GetMultiWordAt(ViewLayerType.SHALLOW_PARSE, index, "shallowParse");
        }

        public Argument GetArgument()
        {
            if (_layers.ContainsKey(ViewLayerType.PROPBANK))
            {
                if (_layers[ViewLayerType.PROPBANK] is TurkishPropbankLayer)
                {
                    var argumentLayer = (TurkishPropbankLayer) _layers[ViewLayerType.PROPBANK];
                    return argumentLayer.GetArgument();
                }

                return null;
            }

            return null;
        }

        public Argument GetArgumentAt(int index)
        {
            if (_layers.ContainsKey(ViewLayerType.ENGLISH_PROPBANK))
            {
                if (_layers[ViewLayerType.ENGLISH_PROPBANK] is SingleWordMultiItemLayer<Argument>)
                {
                    var multiArgumentLayer =
                        (SingleWordMultiItemLayer<Argument>) _layers[ViewLayerType.ENGLISH_PROPBANK];
                    return multiArgumentLayer.GetItemAt(index);
                }
            }

            return null;
        }

        public MorphologicalParse GetMorphologicalParseAt(int index)

        {
            if (_layers.ContainsKey(ViewLayerType.INFLECTIONAL_GROUP))
            {
                if (_layers[ViewLayerType.INFLECTIONAL_GROUP] is MultiWordLayer<MorphologicalParse>)
                {
                    var multiWordLayer = (MultiWordLayer<MorphologicalParse>) _layers[ViewLayerType.INFLECTIONAL_GROUP];
                    if (index < multiWordLayer.Size() && index >= 0)
                    {
                        return multiWordLayer.GetItemAt(index);
                    }
                }
            }

            return null;
        }

        public MetamorphicParse GetMetamorphicParseAt(int index)
        {
            if (_layers.ContainsKey(ViewLayerType.META_MORPHEME))
            {
                if (_layers[ViewLayerType.META_MORPHEME] is MultiWordLayer<MetamorphicParse>)
                {
                    var multiWordLayer = (MultiWordLayer<MetamorphicParse>) _layers[ViewLayerType.META_MORPHEME];
                    if (index < multiWordLayer.Size() && index >= 0)
                    {
                        return multiWordLayer.GetItemAt(index);
                    }
                }
            }

            return null;
        }

        public string GetMetaMorphemeAtIndex(int index)
        {
            if (_layers.ContainsKey(ViewLayerType.META_MORPHEME))
            {
                if (_layers[ViewLayerType.META_MORPHEME] is MetaMorphemeLayer)
                {
                    var metaMorphemeLayer = (MetaMorphemeLayer) _layers[ViewLayerType.META_MORPHEME];
                    if (index < metaMorphemeLayer.GetLayerSize(ViewLayerType.META_MORPHEME) && index >= 0)
                    {
                        return metaMorphemeLayer.GetLayerInfoAt(ViewLayerType.META_MORPHEME, index);
                    }
                }
            }

            return null;
        }

        public string GetMetaMorphemeFromIndex(int index)
        {
            if (_layers.ContainsKey(ViewLayerType.META_MORPHEME))
            {
                if (_layers[ViewLayerType.META_MORPHEME] is MetaMorphemeLayer)
                {
                    var metaMorphemeLayer = (MetaMorphemeLayer) _layers[ViewLayerType.META_MORPHEME];
                    if (index < metaMorphemeLayer.GetLayerSize(ViewLayerType.META_MORPHEME) && index >= 0)
                    {
                        return metaMorphemeLayer.GetLayerInfoFrom(index);
                    }
                }
            }

            return null;
        }

        public int GetLayerSize(ViewLayerType viewLayer)
        {
            if (_layers[viewLayer] is MultiWordMultiItemLayer<object>)
            {
                return ((MultiWordMultiItemLayer<object>) _layers[viewLayer]).GetLayerSize(viewLayer);
            }

            if (_layers[viewLayer] is SingleWordMultiItemLayer<object>)
            {
                return ((SingleWordMultiItemLayer<object>) _layers[viewLayer]).GetLayerSize(viewLayer);
            }

            return 0;
        }

        public string GetLayerInfoAt(ViewLayerType viewLayer, int index)
        {
            switch (viewLayer)
            {
                case ViewLayerType.META_MORPHEME_MOVED:
                    if (_layers[viewLayer] is MultiWordMultiItemLayer<MetamorphicParse>)
                    {
                        return ((MultiWordMultiItemLayer<MetamorphicParse>) _layers[viewLayer]).GetLayerInfoAt(
                            viewLayer,
                            index);
                    }

                    break;
                case ViewLayerType.PART_OF_SPEECH:
                case ViewLayerType.INFLECTIONAL_GROUP:
                    if (_layers[viewLayer] is MultiWordMultiItemLayer<MorphologicalParse>)
                    {
                        return ((MultiWordMultiItemLayer<MorphologicalParse>) _layers[viewLayer]).GetLayerInfoAt(
                            viewLayer, index);
                    }

                    break;
                case ViewLayerType.META_MORPHEME:
                    return GetMetaMorphemeAtIndex(index);
                case ViewLayerType.ENGLISH_PROPBANK:
                    return GetArgumentAt(index).GetArgumentType();
                default:
                    return null;
            }

            return null;
        }

        public string GetLayerDescription()
        {
            var result = "";
            foreach (var viewLayerType in _layers.Keys)
            {
                if (viewLayerType != ViewLayerType.PART_OF_SPEECH)
                {
                    result += _layers[viewLayerType].GetLayerDescription();
                }
            }

            return result;
        }

        public string GetLayerData(ViewLayerType viewLayer)
        {
            if (_layers.ContainsKey(viewLayer))
            {
                return _layers[viewLayer].GetLayerValue();
            }

            return null;
        }

        public string GetRobustLayerData(ViewLayerType viewLayer)
        {
            viewLayer = CheckLayer(viewLayer);
            return GetLayerData(viewLayer);
        }

        private void UpdateMetaMorphemesMoved()
        {
            if (_layers.ContainsKey(ViewLayerType.META_MORPHEME))
            {
                var metaMorphemeLayer = (MetaMorphemeLayer) _layers[ViewLayerType.META_MORPHEME];
                if (metaMorphemeLayer.Size() > 0)
                {
                    var result = metaMorphemeLayer.GetItemAt(0).ToString();
                    for (var i = 1; i < metaMorphemeLayer.Size(); i++)
                    {
                        result = result + " " + metaMorphemeLayer.GetItemAt(i);
                    }

                    _layers[ViewLayerType.META_MORPHEME_MOVED] = new MetaMorphemesMovedLayer(result);
                }
            }
        }

        public void RemoveLayer(ViewLayerType layerType)
        {
            _layers.Remove(layerType);
        }

        public void MetaMorphemeClear()
        {
            _layers.Remove(ViewLayerType.META_MORPHEME);
            _layers.Remove(ViewLayerType.META_MORPHEME_MOVED);
        }

        public void EnglishClear()
        {
            _layers.Remove(ViewLayerType.ENGLISH_WORD);
        }

        public void DependencyClear()
        {
            _layers.Remove(ViewLayerType.DEPENDENCY);
        }

        public void MetaMorphemesMovedClear()
        {
            _layers.Remove(ViewLayerType.META_MORPHEME_MOVED);
        }

        public void SemanticClear()
        {
            _layers.Remove(ViewLayerType.SEMANTICS);
        }

        public void EnglishSemanticClear()
        {
            _layers.Remove(ViewLayerType.ENGLISH_SEMANTICS);
        }

        public void MorphologicalAnalysisClear()
        {
            _layers.Remove(ViewLayerType.INFLECTIONAL_GROUP);
            _layers.Remove(ViewLayerType.PART_OF_SPEECH);
            _layers.Remove(ViewLayerType.META_MORPHEME);
            _layers.Remove(ViewLayerType.META_MORPHEME_MOVED);
        }

        public MetamorphicParse MetaMorphemeRemove(int index)

        {
            MetamorphicParse removedParse = null;
            if (_layers.ContainsKey(ViewLayerType.META_MORPHEME))
            {
                MetaMorphemeLayer metaMorphemeLayer = (MetaMorphemeLayer) _layers[ViewLayerType.META_MORPHEME];
                if (index >= 0 && index < metaMorphemeLayer.GetLayerSize(ViewLayerType.META_MORPHEME))
                {
                    removedParse = metaMorphemeLayer.MetaMorphemeRemoveFromIndex(index);
                    UpdateMetaMorphemesMoved();
                }
            }


            return removedParse;
        }

        public bool IsVerbal()
        {
            if (_layers.ContainsKey(ViewLayerType.INFLECTIONAL_GROUP))
            {
                return ((MorphologicalAnalysisLayer) _layers[ViewLayerType.INFLECTIONAL_GROUP]).IsVerbal();
            }

            return false;
        }

        public bool IsNominal()
        {
            if (_layers.ContainsKey(ViewLayerType.INFLECTIONAL_GROUP))
            {
                return ((MorphologicalAnalysisLayer) _layers[ViewLayerType.INFLECTIONAL_GROUP]).IsNominal();
            }

            return false;
        }

        public List<LayerInfo> DivideIntoWords()
        {
            var result = new List<LayerInfo>();
            for (var i = 0; i < GetNumberOfWords(); i++)
            {
                var layerInfo = new LayerInfo();
                layerInfo.SetLayerData(ViewLayerType.TURKISH_WORD, GetTurkishWordAt(i));
                layerInfo.SetLayerData(ViewLayerType.ENGLISH_WORD, GetLayerData(ViewLayerType.ENGLISH_WORD));
                if (LayerExists(ViewLayerType.INFLECTIONAL_GROUP))
                {
                    layerInfo.SetMorphologicalAnalysis(GetMorphologicalParseAt(i));
                }

                if (LayerExists(ViewLayerType.META_MORPHEME))
                {
                    layerInfo.SetMetaMorphemes(GetMetamorphicParseAt(i));
                }

                if (LayerExists(ViewLayerType.ENGLISH_PROPBANK))
                {
                    layerInfo.SetLayerData(ViewLayerType.ENGLISH_PROPBANK,
                        GetLayerData(ViewLayerType.ENGLISH_PROPBANK));
                }

                if (LayerExists(ViewLayerType.ENGLISH_SEMANTICS))
                {
                    layerInfo.SetLayerData(ViewLayerType.ENGLISH_SEMANTICS,
                        GetLayerData(ViewLayerType.ENGLISH_SEMANTICS));
                }

                if (LayerExists(ViewLayerType.NER))
                {
                    layerInfo.SetLayerData(ViewLayerType.NER, GetLayerData(ViewLayerType.NER));
                }

                if (LayerExists(ViewLayerType.SEMANTICS))
                {
                    layerInfo.SetLayerData(ViewLayerType.SEMANTICS, GetSemanticAt(i));
                }

                if (LayerExists(ViewLayerType.PROPBANK))
                {
                    layerInfo.SetLayerData(ViewLayerType.PROPBANK, GetArgument().ToString());
                }

                if (LayerExists(ViewLayerType.SHALLOW_PARSE))
                {
                    layerInfo.SetLayerData(ViewLayerType.SHALLOW_PARSE, GetShallowParseAt(i));
                }

                result.Add(layerInfo);
            }

            return result;
        }

        public AnnotatedWord ToAnnotatedWord(int wordIndex)
        {
            AnnotatedWord annotatedWord = new AnnotatedWord(GetTurkishWordAt(wordIndex));
            if (LayerExists(ViewLayerType.INFLECTIONAL_GROUP))
            {
                annotatedWord.SetParse(GetMorphologicalParseAt(wordIndex).ToString());
            }

            if (LayerExists(ViewLayerType.META_MORPHEME))
            {
                annotatedWord.SetMetamorphicParse(GetMetamorphicParseAt(wordIndex).ToString());
            }

            if (LayerExists(ViewLayerType.SEMANTICS))
            {
                annotatedWord.SetSemantic(GetSemanticAt(wordIndex));
            }

            if (LayerExists(ViewLayerType.NER))
            {
                annotatedWord.SetNamedEntityType(GetLayerData(ViewLayerType.NER));
            }

            if (LayerExists(ViewLayerType.PROPBANK))
            {
                annotatedWord.SetArgument(GetArgument().ToString());
            }

            if (LayerExists(ViewLayerType.SHALLOW_PARSE))
            {
                annotatedWord.SetShallowParse(GetShallowParseAt(wordIndex));
            }

            return annotatedWord;
        }
    }
}