using System.Collections.Generic;
using System.Xml;
using AnnotatedSentence;
using ParseTree;

namespace AnnotatedTree
{
    public class ParseNodeSearchable : ParseNode
    {
        private readonly List<SearchType> _searchTypes;
        private readonly List<ViewLayerType> _viewLayerTypes;
        private readonly List<string> _searchValues;
        private readonly bool _isLeaf;

        public ParseNodeSearchable(ParseNodeSearchable parent, XmlNode node)
        {
            children = new List<ParseNode>();
            this.parent = parent;
            _isLeaf = node.Name == "leaf";
            _searchTypes = new List<SearchType>();
            _viewLayerTypes = new List<ViewLayerType>();
            _searchValues = new List<string>();
            if (node.Attributes.Count != 0)
            {
                for (var i = 0; i < node.Attributes.Count; i++)
                {
                    var attribute = node.Attributes[i];
                    var viewLayerType = attribute.Name.Substring(0, 3);
                    var searchType = attribute.Name.Substring(3);
                    _searchValues.Add(attribute.InnerText);
                    if (searchType.Equals("equals"))
                    {
                        _searchTypes.Add(SearchType.EQUALS);
                    }
                    else
                    {
                        if (searchType.Equals("contains"))
                        {
                            _searchTypes.Add(SearchType.CONTAINS);
                        }
                        else
                        {
                            if (searchType.Equals("matches"))
                            {
                                _searchTypes.Add(SearchType.MATCHES);
                            }
                            else
                            {
                                if (searchType.Equals("starts"))
                                {
                                    _searchTypes.Add(SearchType.STARTS);
                                }
                                else
                                {
                                    if (searchType.Equals("ends"))
                                    {
                                        _searchTypes.Add(SearchType.ENDS);
                                    }
                                    else
                                    {
                                        if (searchType.Equals("equalsignorecase"))
                                        {
                                            _searchTypes.Add(SearchType.EQUALS_IGNORE_CASE);
                                        }
                                        else
                                        {
                                            if (searchType.Equals("isnull"))
                                            {
                                                _searchTypes.Add(SearchType.IS_NULL);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (viewLayerType.Equals("mor") || viewLayerType.Equals("inf"))
                    {
                        _viewLayerTypes.Add(ViewLayerType.INFLECTIONAL_GROUP);
                    }
                    else
                    {
                        if (viewLayerType.Equals("tur"))
                        {
                            _viewLayerTypes.Add(ViewLayerType.TURKISH_WORD);
                        }
                        else
                        {
                            if (viewLayerType.Equals("per"))
                            {
                                _viewLayerTypes.Add(ViewLayerType.PERSIAN_WORD);
                            }
                            else
                            {
                                if (viewLayerType.Equals("eng"))
                                {
                                    _viewLayerTypes.Add(ViewLayerType.ENGLISH_WORD);
                                }
                                else
                                {
                                    if (viewLayerType.Equals("ner"))
                                    {
                                        _viewLayerTypes.Add(ViewLayerType.NER);
                                    }
                                    else
                                    {
                                        if (viewLayerType.Equals("sem") ||
                                            viewLayerType.Equals("tse"))
                                        {
                                            _viewLayerTypes.Add(ViewLayerType.SEMANTICS);
                                        }
                                        else
                                        {
                                            if (viewLayerType.Equals("met"))
                                            {
                                                _viewLayerTypes.Add(ViewLayerType.META_MORPHEME);
                                            }
                                            else
                                            {
                                                if (viewLayerType.Equals("pro"))
                                                {
                                                    _viewLayerTypes.Add(ViewLayerType.PROPBANK);
                                                }
                                                else
                                                {
                                                    if (viewLayerType.Equals("dep"))
                                                    {
                                                        _viewLayerTypes.Add(ViewLayerType.DEPENDENCY);
                                                    }
                                                    else
                                                    {
                                                        if (viewLayerType.Equals("sha") ||
                                                            viewLayerType.Equals("chu"))
                                                        {
                                                            _viewLayerTypes.Add(ViewLayerType.SHALLOW_PARSE);
                                                        }
                                                        else
                                                        {
                                                            if (viewLayerType.Equals("ese"))
                                                            {
                                                                _viewLayerTypes.Add(ViewLayerType.ENGLISH_SEMANTICS);
                                                            }
                                                            else
                                                            {
                                                                if (viewLayerType.Equals("epr"))
                                                                {
                                                                    _viewLayerTypes.Add(ViewLayerType.ENGLISH_PROPBANK);
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

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name.Equals("node") || child.Name.Equals("leaf"))
                {
                    children.Add(new ParseNodeSearchable(this, child));
                }
            }
        }

        public SearchType GetType(int index)
        {
            return _searchTypes[index];
        }

        public string GetValue(int index)
        {
            return _searchValues[index];
        }

        public ViewLayerType GetViewLayerType(int index)
        {
            return _viewLayerTypes[index];
        }

        public new bool IsLeaf()
        {
            return _isLeaf;
        }

        public int Size()
        {
            return _searchValues.Count;
        }
    }
}