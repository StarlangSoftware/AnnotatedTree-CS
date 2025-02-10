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

        /// <summary>
        /// Constructs the layer information from the given string. Layers are represented as
        /// {layername1=layervalue1}{layername2=layervalue2}...{layernamek=layervaluek} where layer name is one of the
        /// following: turkish, persian, english, morphologicalAnalysis, metaMorphemes, metaMorphemesMoved, dependency,
        /// semantics, namedEntity, propBank, englishPropbank, englishSemantics, shallowParse. Splits the string w.r.t.
        /// parentheses and constructs layer objects and put them layers map accordingly.
        /// </summary>
        /// <param name="info">Line consisting of layer info.</param>
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

        /// <summary>
        /// Changes the given layer info with the given string layer value. For all layers new layer object is created and
        /// replaces the original object. For turkish layer, it also destroys inflectional_group, part_of_speech,
        /// meta_morpheme, meta_morpheme_moved and semantics layers. For persian layer, it also destroys the semantics layer.
        /// </summary>
        /// <param name="viewLayer">Layer name.</param>
        /// <param name="layerValue">New layer value.</param>
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

        /// <summary>
        /// Updates the inflectional_group and part_of_speech layers according to the given parse.
        /// </summary>
        /// <param name="parse">New parse to update layers.</param>
        public void SetMorphologicalAnalysis(MorphologicalParse parse)
        {
            _layers[ViewLayerType.INFLECTIONAL_GROUP] = new MorphologicalAnalysisLayer(parse.ToString());
            _layers[ViewLayerType.PART_OF_SPEECH] = new MorphologicalAnalysisLayer(parse.ToString());
        }

        /// <summary>
        /// Updates the metamorpheme layer according to the given parse.
        /// </summary>
        /// <param name="parse">New parse to update layer.</param>
        public void SetMetaMorphemes(MetamorphicParse parse)
        {
            _layers[ViewLayerType.META_MORPHEME] = new MetaMorphemeLayer(parse.ToString());
        }

        /// <summary>
        /// Checks if the given layer exists.
        /// </summary>
        /// <param name="viewLayerType">Layer name</param>
        /// <returns>True if the layer exists, false otherwise.</returns>
        public bool LayerExists(ViewLayerType viewLayerType)
        {
            return _layers.ContainsKey(viewLayerType);
        }

        /// <summary>
        /// Two level layer check method. For turkish, persian and english_semantics layers, if the layer does not exist,
        /// returns english layer. For part_of_speech, inflectional_group, meta_morpheme, semantics, propbank, shallow_parse,
        /// english_propbank layers, if the layer does not exist, it checks turkish layer. For meta_morpheme_moved, if the
        /// layer does not exist, it checks meta_morpheme layer.
        /// </summary>
        /// <param name="viewLayer">Layer to be checked.</param>
        /// <returns>Returns the original layer if the layer exists. For turkish, persian and english_semantics layers, if the
        /// layer  does not exist, returns english layer. For part_of_speech, inflectional_group, meta_morpheme, semantics,
        /// propbank,  shallow_parse, english_propbank layers, if the layer does not exist, it checks turkish layer
        /// recursively. For meta_morpheme_moved, if the layer does not exist, it checks meta_morpheme layer recursively.</returns>
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

        /// <summary>
        /// Returns number of words in the Turkish or Persian layer, whichever exists.
        /// </summary>
        /// <returns>Number of words in the Turkish or Persian layer, whichever exists.</returns>
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

        /// <summary>
        /// Returns the layer value at the given index.
        /// </summary>
        /// <param name="viewLayerType">Layer for which the value at the given word index will be returned.</param>
        /// <param name="index">Word Position of the layer value.</param>
        /// <param name="layerName">Name of the layer.</param>
        /// <returns>Layer info at word position index for a multiword layer.</returns>
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

        /// <summary>
        /// Layers may contain multiple Turkish words. This method returns the Turkish word at position index.
        /// </summary>
        /// <param name="index">Position of the Turkish word.</param>
        /// <returns>The Turkish word at position index.</returns>
        public string GetTurkishWordAt(int index)
        {
            return GetMultiWordAt(ViewLayerType.TURKISH_WORD, index, "turkish");
        }

        /// <summary>
        /// Returns number of meanings in the Turkish layer.
        /// </summary>
        /// <returns>Number of meanings in the Turkish layer.</returns>
        public int GetNumberOfMeanings()
        {
            if (_layers.ContainsKey(ViewLayerType.SEMANTICS))
            {
                return ((TurkishSemanticLayer) _layers[ViewLayerType.SEMANTICS]).Size();
            }

            return 0;
        }

        /// <summary>
        /// Layers may contain multiple semantic information corresponding to multiple Turkish words. This method returns
        /// the sense id at position index.
        /// </summary>
        /// <param name="index">Position of the Turkish word.</param>
        /// <returns>The Turkish sense id at position index.</returns>
        public string GetSemanticAt(int index)
        {
            return GetMultiWordAt(ViewLayerType.SEMANTICS, index, "semantics");
        }

        /// <summary>
        /// Layers may contain multiple shallow parse information corresponding to multiple Turkish words. This method
        /// returns the shallow parse tag at position index.
        /// </summary>
        /// <param name="index">Position of the Turkish word.</param>
        /// <returns>The shallow parse tag at position index.</returns>
        public string GetShallowParseAt(int index)
        {
            return GetMultiWordAt(ViewLayerType.SHALLOW_PARSE, index, "shallowParse");
        }

        /// <summary>
        /// Returns the Turkish PropBank argument info.
        /// </summary>
        /// <returns>Turkish PropBank argument info.</returns>
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

        /// <summary>
        /// A word may have multiple English propbank info. This method returns the English PropBank argument info at
        /// position index.
        /// </summary>
        /// <param name="index">Position of the argument</param>
        /// <returns>English PropBank argument info at position index.</returns>
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

        /// <summary>
        /// Layers may contain multiple morphological parse information corresponding to multiple Turkish words. This method
        /// returns the morphological parse at position index.
        /// </summary>
        /// <param name="index">Position of the Turkish word.</param>
        /// <returns>The morphological parse at position index.</returns>
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

        /// <summary>
        /// Layers may contain multiple metamorphic parse information corresponding to multiple Turkish words. This method
        /// returns the metamorphic parse at position index.
        /// </summary>
        /// <param name="index">Position of the Turkish word.</param>
        /// <returns>The metamorphic parse at position index.</returns>
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

        /// <summary>
        /// Layers may contain multiple metamorphemes corresponding to one or multiple Turkish words. This method
        /// returns the metamorpheme at position index.
        /// </summary>
        /// <param name="index">Position of the metamorpheme.</param>
        /// <returns>The metamorpheme at position index.</returns>
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

        /// <summary>
        /// Layers may contain multiple metamorphemes corresponding to one or multiple Turkish words. This method
        /// returns all metamorphemes from position index.
        /// </summary>
        /// <param name="index">Start position of the metamorpheme.</param>
        /// <returns>All metamorphemes from position index.</returns>
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

        /// <summary>
        /// For layers with multiple item information, this method returns total items in that layer.
        /// </summary>
        /// <param name="viewLayer">Layer name</param>
        /// <returns>Total items in the given layer.</returns>
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

        /// <summary>
        /// For layers with multiple item information, this method returns the item at position index.
        /// </summary>
        /// <param name="viewLayer">Layer name</param>
        /// <param name="index">Position of the item.</param>
        /// <returns>The item at position index.</returns>
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

        /// <summary>
        /// Returns the string form of all layer information except part_of_speech layer.
        /// </summary>
        /// <returns>The string form of all layer information except part_of_speech layer.</returns>
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

        /// <summary>
        /// Returns the layer info for the given layer.
        /// </summary>
        /// <param name="viewLayer">Layer name.</param>
        /// <returns>Layer info for the given layer.</returns>
        public string GetLayerData(ViewLayerType viewLayer)
        {
            if (_layers.ContainsKey(viewLayer))
            {
                return _layers[viewLayer].GetLayerValue();
            }

            return null;
        }

        /// <summary>
        /// Returns the layer info for the given layer, if that layer exists. Otherwise, it returns the fallback layer info
        /// determined by the checkLayer.
        /// </summary>
        /// <param name="viewLayer">Layer name</param>
        /// <returns>Layer info for the given layer if it exists. Otherwise, it returns the fallback layer info determined by
        /// the checkLayer.</returns>
        public string GetRobustLayerData(ViewLayerType viewLayer)
        {
            viewLayer = CheckLayer(viewLayer);
            return GetLayerData(viewLayer);
        }

        /// <summary>
        /// Initializes the metamorphemesmoved layer with metamorpheme layer except the root word.
        /// </summary>
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

        /// <summary>
        /// Removes the given layer from hash map.
        /// </summary>
        /// <param name="layerType">Layer to be removed.</param>
        public void RemoveLayer(ViewLayerType layerType)
        {
            _layers.Remove(layerType);
        }

        /// <summary>
        /// Removes metamorpheme and metamorphemesmoved layers.
        /// </summary>
        public void MetaMorphemeClear()
        {
            _layers.Remove(ViewLayerType.META_MORPHEME);
            _layers.Remove(ViewLayerType.META_MORPHEME_MOVED);
        }

        /// <summary>
        /// Removes English layer.
        /// </summary>
        public void EnglishClear()
        {
            _layers.Remove(ViewLayerType.ENGLISH_WORD);
        }

        /// <summary>
        /// Removes the dependency layer.
        /// </summary>
        public void DependencyClear()
        {
            _layers.Remove(ViewLayerType.DEPENDENCY);
        }

        /// <summary>
        /// Removed metamorphemesmoved layer.
        /// </summary>
        public void MetaMorphemesMovedClear()
        {
            _layers.Remove(ViewLayerType.META_MORPHEME_MOVED);
        }

        /// <summary>
        /// Removes the Turkish semantic layer.
        /// </summary>
        public void SemanticClear()
        {
            _layers.Remove(ViewLayerType.SEMANTICS);
        }

        /// <summary>
        /// Removes the English semantic layer.
        /// </summary>
        public void EnglishSemanticClear()
        {
            _layers.Remove(ViewLayerType.ENGLISH_SEMANTICS);
        }

        /// <summary>
        /// Removes the morphological analysis, part of speech, metamorpheme, and metamorphemesmoved layers.
        /// </summary>
        public void MorphologicalAnalysisClear()
        {
            _layers.Remove(ViewLayerType.INFLECTIONAL_GROUP);
            _layers.Remove(ViewLayerType.PART_OF_SPEECH);
            _layers.Remove(ViewLayerType.META_MORPHEME);
            _layers.Remove(ViewLayerType.META_MORPHEME_MOVED);
        }

        /// <summary>
        /// Removes the metamorpheme at position index.
        /// </summary>
        /// <param name="index">Position of the metamorpheme to be removed.</param>
        /// <returns>Metamorphemes concatenated as a string after the removed metamorpheme.</returns>
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

        /// <summary>
        /// Checks if the last inflectional group contains VERB tag.
        /// </summary>
        /// <returns>True if the last inflectional group contains VERB tag, false otherwise.</returns>
        public bool IsVerbal()
        {
            if (_layers.ContainsKey(ViewLayerType.INFLECTIONAL_GROUP))
            {
                return ((MorphologicalAnalysisLayer) _layers[ViewLayerType.INFLECTIONAL_GROUP]).IsVerbal();
            }

            return false;
        }

        /// <summary>
        /// Checks if the last verbal inflectional group contains ZERO tag.
        /// </summary>
        /// <returns>True if the last verbal inflectional group contains ZERO tag, false otherwise.</returns>
        public bool IsNominal()
        {
            if (_layers.ContainsKey(ViewLayerType.INFLECTIONAL_GROUP))
            {
                return ((MorphologicalAnalysisLayer) _layers[ViewLayerType.INFLECTIONAL_GROUP]).IsNominal();
            }

            return false;
        }

        /// <summary>
        /// Creates an array list of LayerInfo objects, where each object correspond to one word in the tree node. Turkish
        /// words, morphological parses, metamorpheme parses, semantic senses, shallow parses are divided into corresponding
        /// words. Named entity tags and propbank arguments are the same for all words.
        /// </summary>
        /// <returns>An array list of LayerInfo objects created from the layer info of the node.</returns>
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

        /// <summary>
        /// Converts layer info of the word at position wordIndex to an AnnotatedWord. Layers are converted to their
        /// counterparts in the AnnotatedWord.
        /// </summary>
        /// <param name="wordIndex">Index of the word to be converted.</param>
        /// <returns>Converted annotatedWord</returns>
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
                annotatedWord.SetArgumentList(GetArgument().ToString());
            }

            if (LayerExists(ViewLayerType.SHALLOW_PARSE))
            {
                annotatedWord.SetShallowParse(GetShallowParseAt(wordIndex));
            }

            return annotatedWord;
        }
    }
}