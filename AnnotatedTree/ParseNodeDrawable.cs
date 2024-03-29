using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AnnotatedSentence;
using AnnotatedTree.Processor;
using AnnotatedTree.Processor.Condition;
using Dictionary.Dictionary;
using MorphologicalAnalysis;
using NamedEntityRecognition;
using ParseTree;

namespace AnnotatedTree
{
    public class ParseNodeDrawable : ParseNode, ICloneable
    {
        protected LayerInfo layers;
        protected int depth;
        private const string VerbalLabel = "VG";

        private static readonly List<string> SentenceLabels =
            new List<string>(new[] {"SINV", "SBARQ", "SBAR", "SQ", "S"});

        public ParseNodeDrawable(ParseNodeDrawable parent, string line, bool isLeaf, int depth)
        {
            var parenthesisCount = 0;
            var childLine = "";
            this.depth = depth;
            this.parent = parent;
            children = new List<ParseNode>();
            if (isLeaf)
            {
                if (!line.Contains("{"))
                {
                    data = new Symbol(line);
                }
                else
                {
                    layers = new LayerInfo(line);
                }
            }
            else
            {
                var startPos = line.IndexOf(" ");

                data = new Symbol(line.Substring(1, startPos - 1));
                if (line.IndexOf(")") == line.LastIndexOf(")"))
                {
                    children.Add(new ParseNodeDrawable(this,
                        line.Substring(startPos + 1, line.IndexOf(")") - startPos - 1), true,
                        depth + 1));
                }
                else
                {
                    for (var i = startPos + 1; i < line.Length; i++)
                    {
                        if (line[i] != ' ' || parenthesisCount > 0)
                        {
                            childLine = childLine + line[i];
                        }

                        if (line[i] == '(')
                        {
                            parenthesisCount++;
                        }
                        else
                        {
                            if (line[i] == ')')
                            {
                                parenthesisCount--;
                            }
                        }

                        if (parenthesisCount == 0 && childLine != "")
                        {
                            children.Add(new ParseNodeDrawable(this, childLine.Trim(), false, depth + 1));
                            childLine = "";
                        }
                    }
                }
            }
        }

        public ParseNodeDrawable(Symbol data) : base(data)
        {
        }

        public ParseNodeDrawable(ParseNodeDrawable parent, ParseNodeDrawable child, string symbol)
        {
            this.children = new List<ParseNode>();
            this.depth = child.depth;
            child.UpdateDepths(this.depth + 1);
            this.parent = parent;
            this.parent.SetChild(parent.GetChildIndex(child), this);
            this.children.Add(child);
            child.parent = this;
            this.data = new Symbol(symbol);
        }

        public object Clone()
        {
            var result = new ParseNodeDrawable(data) {children = new List<ParseNode>()};
            if (layers != null)
                result.layers = (LayerInfo) layers.Clone();
            return result;
        }

        public LayerInfo GetLayerInfo()
        {
            return layers;
        }

        public new Symbol GetData()
        {
            if (layers == null)
            {
                return base.GetData();
            }

            return new Symbol(GetLayerData(ViewLayerType.ENGLISH_WORD));
        }

        public void ClearLayers()
        {
            layers = new LayerInfo();
        }

        public void ClearLayer(ViewLayerType layerType)
        {
            if (children.Count == 0 && LayerExists(layerType))
            {
                layers.RemoveLayer(layerType);
            }

            for (var i = 0; i < NumberOfChildren(); i++)
            {
                ((ParseNodeDrawable) children[i]).ClearLayer(layerType);
            }
        }

        public void ClearData()
        {
            data = null;
        }

        public void SetDataAndClearLayers(Symbol data)
        {
            base.SetData(data);
            layers = null;
        }

        public new void SetData(Symbol newData)
        {
            if (layers == null)
            {
                base.SetData(newData);
            }
            else
            {
                layers.SetLayerData(ViewLayerType.ENGLISH_WORD, newData.GetName());
            }
        }

        public string HeadWord(ViewLayerType viewLayerType)
        {
            if (children.Count > 0)
            {
                return ((ParseNodeDrawable) HeadChild()).HeadWord(viewLayerType);
            }

            return GetLayerData(viewLayerType);
        }

        public string GetLayerData()
        {
            if (data != null)
                return data.GetName();
            return layers.GetLayerDescription();
        }

        public string GetLayerData(ViewLayerType viewLayer)
        {
            if (viewLayer == ViewLayerType.WORD || layers == null)
                return data.GetName();
            return layers.GetLayerData(viewLayer);
        }

        public int GetDepth()
        {
            return depth;
        }

        public int StructureAgreementCount(ParseNodeDrawable parseNode)
        {
            if (children.Count > 1)
            {
                var sum = 1;
                for (var i = 0; i < children.Count; i++)
                {
                    if (i < parseNode.NumberOfChildren())
                    {
                        if (!GetChild(i).GetData().GetName()
                            .Equals(parseNode.GetChild(i).GetData().GetName()))
                        {
                            sum = 0;
                            break;
                        }
                    }
                    else
                    {
                        sum = 0;
                        break;
                    }
                }

                for (var i = 0; i < children.Count; i++)
                {
                    if (i < parseNode.NumberOfChildren() && GetChild(i).GetData().GetName()
                        .Equals(parseNode.GetChild(i).GetData().GetName()))
                    {
                        sum += ((ParseNodeDrawable) GetChild(i)).StructureAgreementCount(
                            (ParseNodeDrawable) parseNode.GetChild(i));
                    }
                    else
                    {
                        for (var j = 0; j < parseNode.NumberOfChildren(); j++)
                        {
                            if (GetChild(i).GetData().GetName()
                                .Equals(parseNode.GetChild(j).GetData().GetName()))
                            {
                                sum += ((ParseNodeDrawable) GetChild(i)).StructureAgreementCount(
                                    (ParseNodeDrawable) parseNode.GetChild(j));
                                break;
                            }
                        }
                    }
                }

                return sum;
            }

            return 0;
        }

        public int GlossAgreementCount(ParseNodeDrawable parseNode, ViewLayerType viewLayerType)
        {
            if (children.Count == 0)
            {
                if (parseNode.NumberOfChildren() == 0)
                {
                    if (GetLayerData(viewLayerType).Equals(parseNode.GetLayerData(viewLayerType)))
                    {
                        return 1;
                    }

                    return 0;
                }

                return 0;
            }

            var sum = 0;
            for (var i = 0; i < children.Count; i++)
            {
                if (i < parseNode.NumberOfChildren())
                {
                    sum += ((ParseNodeDrawable) GetChild(i)).GlossAgreementCount(
                        (ParseNodeDrawable) parseNode.GetChild(i), viewLayerType);
                }
            }

            return sum;
        }

        public void ReplaceChild(ParseNodeDrawable oldChild, ParseNodeDrawable newChild)
        {
            newChild.UpdateDepths(this.depth + 1);
            newChild.parent = this;
            children[children.IndexOf(oldChild)] = newChild;
        }

        public void UpdateDepths(int newDepth)
        {
            this.depth = newDepth;
            foreach (var aChildren in children)
            {
                var aChild = (ParseNodeDrawable) aChildren;
                aChild.UpdateDepths(newDepth + 1);
            }
        }

        public int MaxDepth()
        {
            int currentDepth = this.depth;
            foreach (var aChildren in children)
            {
                var aChild = (ParseNodeDrawable) aChildren;
                if (aChild.MaxDepth() > currentDepth)
                    currentDepth = aChild.MaxDepth();
            }

            return currentDepth;
        }

        public bool Satisfy(ParseNodeSearchable node)
        {
            int i;
            if (node.IsLeaf() && children.Count > 0)
                return false;
            for (i = 0; i < node.Size(); i++)
            {
                var viewLayer = node.GetViewLayerType(i);
                var currentData = node.GetValue(i);
                if (GetLayerData(viewLayer) == null && node.GetType(i) != SearchType.EQUALS &&
                    node.GetType(i) != SearchType.IS_NULL)
                {
                    return false;
                }

                switch (node.GetType(i))
                {
                    case SearchType.CONTAINS:
                        if (!GetLayerData(viewLayer).Contains(currentData))
                        {
                            return false;
                        }

                        break;
                    case SearchType.EQUALS:
                        if (GetLayerData(viewLayer) == null)
                        {
                            if (node.GetValue(i) != "")
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (!GetLayerData(viewLayer).Equals(currentData))
                            {
                                return false;
                            }
                        }

                        break;
                    case SearchType.EQUALS_IGNORE_CASE:
                        if (!GetLayerData(viewLayer).Equals(currentData, StringComparison.OrdinalIgnoreCase))
                        {
                            return false;
                        }

                        break;
                    case SearchType.MATCHES:
                        if (!new Regex(currentData).IsMatch(GetLayerData(viewLayer)))
                        {
                            return false;
                        }

                        break;
                    case SearchType.STARTS:
                        if (!GetLayerData(viewLayer).StartsWith(currentData))
                        {
                            return false;
                        }

                        break;
                    case SearchType.ENDS:
                        if (!GetLayerData(viewLayer).EndsWith(currentData))
                        {
                            return false;
                        }

                        break;
                    case SearchType.IS_NULL:
                        if (GetLayerData(viewLayer) != null)
                        {
                            return false;
                        }

                        break;
                }
            }

            if (node.NumberOfChildren() > children.Count)
            {
                return false;
            }

            for (i = 0; i < children.Count; i++)
            {
                if (i < node.NumberOfChildren() &&
                    !((ParseNodeDrawable) GetChild(i)).Satisfy((ParseNodeSearchable) node.GetChild(i)))
                {
                    return false;
                }
            }

            return true;
        }

        public void UpdatePosTags()
        {
            if (children.Count == 1 && children[0].IsLeaf() && !children[0].IsDummyNode())
            {
                var layerInfo = ((ParseNodeDrawable) children[0]).GetLayerInfo();
                var morphologicalParse =
                    layerInfo.GetMorphologicalParseAt(layerInfo.GetNumberOfWords() - 1);
                if (morphologicalParse.IsProperNoun())
                {
                    SetData(new Symbol("NNP"));
                }
                else
                {
                    if (morphologicalParse.IsNoun())
                    {
                        SetData(new Symbol("NN"));
                    }
                    else
                    {
                        if (morphologicalParse.IsAdjective())
                        {
                            SetData(new Symbol("JJ"));
                        }
                        else
                        {
                            if (morphologicalParse.GetPos().Equals("ADV"))
                            {
                                SetData(new Symbol("RB"));
                            }
                            else
                            {
                                if (morphologicalParse.GetPos().Equals("CONJ"))
                                {
                                    SetData(new Symbol("CC"));
                                }
                                else
                                {
                                    if (morphologicalParse.GetPos().Equals("DET"))
                                    {
                                        SetData(new Symbol("DT"));
                                    }
                                    else
                                    {
                                        if (morphologicalParse.GetPos().Equals("POSTP"))
                                        {
                                            SetData(new Symbol("IN"));
                                        }
                                        else
                                        {
                                            if (morphologicalParse.IsCardinal())
                                            {
                                                SetData(new Symbol("CD"));
                                            }
                                            else
                                            {
                                                if (morphologicalParse.IsVerb())
                                                {
                                                    SetData(new Symbol("V"));
                                                }
                                                else
                                                {
                                                    if (morphologicalParse.GetPos().Equals("INTERJ"))
                                                    {
                                                        SetData(new Symbol("UH"));
                                                    }
                                                    else
                                                    {
                                                        if (morphologicalParse.GetPos().Equals("PRON"))
                                                        {
                                                            if (morphologicalParse.ContainsTag(MorphologicalTag
                                                                .QUESTIONPRONOUN)
                                                            )
                                                            {
                                                                SetData(new Symbol("WP"));
                                                            }
                                                            else
                                                            {
                                                                SetData(new Symbol("PRP"));
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
            else
            {
                foreach (var aChildren in children)
                {
                    var aChild = (ParseNodeDrawable) aChildren;
                    aChild.UpdatePosTags();
                }
            }
        }

        public List<Symbol> GetChildrenSymbols()
        {
            var childrenSymbols = new List<Symbol>();
            foreach (var node in children)
            {
                var treeNode = (ParseNodeDrawable) node;
                childrenSymbols.Add(treeNode.GetData());
            }

            return childrenSymbols;
        }

        public void Augment()
        {
            foreach (var child in children)
            {
                ((ParseNodeDrawable) child).Augment();
            }

            var childrenSymbols = GetChildrenSymbols();
            childrenSymbols.Sort(new EnglishWordComparator());
            if (childrenSymbols.Count > 0)
            {
                if (layers != null)
                {
                    layers.SetLayerData(ViewLayerType.ENGLISH_WORD,
                        layers.GetLayerData(ViewLayerType.ENGLISH_WORD) + childrenSymbols);
                }
                else
                {
                    data = new Symbol(data.GetName() + childrenSymbols);
                }
            }
        }

        public new string AncestorString()
        {
            if (parent == null)
            {
                return data.GetName();
            }

            if (layers == null)
            {
                return parent.AncestorString() + data.GetName();
            }

            return parent.AncestorString() + layers.GetLayerData(ViewLayerType.ENGLISH_WORD);
        }

        public void DeAugment()
        {
            var old = GetData().GetName();

            int index = old.IndexOf("[");
            if (index >= 0)
            {
                SetData(new Symbol(old.Substring(0, index)));
            }

            foreach (var child in children)
            {
                ((ParseNodeDrawable) child).DeAugment();
            }
        }

        public bool LayerExists(ViewLayerType viewLayerType)
        {
            if (children.Count == 0)
            {
                if (GetLayerData(viewLayerType) != null)
                {
                    return true;
                }
            }
            else
            {
                foreach (var aChild in children)
                {
                    if (((ParseNodeDrawable) aChild).LayerExists(viewLayerType))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public new bool IsDummyNode()
        {
            var currentData = GetLayerData(ViewLayerType.ENGLISH_WORD);
            var parentData = ((ParseNodeDrawable) parent).GetLayerData(ViewLayerType.ENGLISH_WORD);

            var targetData = GetLayerData(ViewLayerType.TURKISH_WORD);
            if (currentData != null && parentData != null)
            {
                if (targetData != null && targetData.Contains("*"))
                {
                    return true;
                }

                return currentData.Contains("*") || (currentData.Equals("0") && parentData.Equals("-NONE-"));
            }

            return false;
        }

        public List<ParseNodeDrawable> Satisfy(ParseTreeSearchable tree)
        {
            List<ParseNodeDrawable> result = new List<ParseNodeDrawable>();
            if (Satisfy((ParseNodeSearchable) tree.GetRoot()))
            {
                result.Add(this);
            }

            foreach (var child in children)
            {
                result = result.Union(((ParseNodeDrawable) child).Satisfy(tree)).ToList();
            }

            return result;
        }

        public bool LayerAll(ViewLayerType viewLayerType)
        {
            if (children.Count == 0)
            {
                if (GetLayerData(viewLayerType) == null && !IsDummyNode())
                {
                    return false;
                }
            }
            else
            {
                foreach (ParseNode aChild in children)
                {
                    if (!((ParseNodeDrawable) aChild).LayerAll(viewLayerType))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void AttachVgToVp(ParseNodeDrawable node)
        {
            var pn = new ParseNodeDrawable(new Symbol(VerbalLabel));
            pn.AddChild((ParseNodeDrawable) node.Clone());
            pn.parent = this;

            var current = node;
            while (current.parent != this)
                current = (ParseNodeDrawable) current.parent;

            var index = this.children.IndexOf(current);
            this.children.Insert(index + 1, pn);
            node.layers.SetLayerData(ViewLayerType.TURKISH_WORD, "*NONE*");
            node.layers.SetLayerData(ViewLayerType.ENGLISH_WORD, "*NONE*");
        }

        public bool ExtractVerbal()
        {
            var queue = new List<ParseNodeDrawable> {this};
            while (queue.Count > 0)
            {
                var nextItem = queue[0];
                queue.RemoveAt(0);
                if (nextItem.layers != null && nextItem.layers.IsVerbal())
                {
                    if (nextItem.layers.IsNominal())
                    {
                        nextItem.parent.GetParent().SetData(new Symbol(VerbalLabel));
                    }
                    else
                    {
                        AttachVgToVp(nextItem);
                    }

                    return true;
                }

                foreach (var child in nextItem.children)
                {
                    queue.Add((ParseNodeDrawable) child);
                }
            }

            return false;
        }

        private void SetShallowParseLayer(ChunkType chunkType, string label)
        {
            var startWord = true;
            var nodeLabel = "";
            var nodeDrawableCollector = new NodeDrawableCollector(this, new IsTurkishLeafNode());

            var leafList = nodeDrawableCollector.Collect();
            if (SentenceLabels.Contains(label))
                label = label.Replace(label, "S");
            switch (chunkType)
            {
                case ChunkType.EXISTS:
                    label = "";
                    break;
                case ChunkType.NORMAL:
                    label = label.Replace("-.*", "");
                    label = "-" + label;
                    break;
                case ChunkType.DETAILED:
                    label = label.Replace("[-=](\\d)+$", "");
                    if (label.Contains("-"))
                    {
                        label = label.Substring(0, label.IndexOf('-') + 4);
                    }

                    label = "-" + label;
                    break;
            }

            foreach (var node in leafList)
            {
                var layersInNode = node.GetLayerInfo();
                for (var i = 0; i < layersInNode.GetNumberOfWords(); i++)
                {
                    string wordLabel;
                    if (startWord)
                    {
                        wordLabel = "B" + label;
                        startWord = false;
                    }
                    else
                    {
                        wordLabel = "I" + label;
                    }

                    if (i == 0)
                    {
                        nodeLabel = wordLabel;
                    }
                    else
                    {
                        nodeLabel = nodeLabel + " " + wordLabel;
                    }
                }

                node.GetLayerInfo().SetLayerData(ViewLayerType.SHALLOW_PARSE, nodeLabel);
            }
        }

        public void SetShallowParseLayer(ChunkType chunkType)
        {
            if (GetData() != null && GetData().IsChunkLabel() && parent != null)
            {
                string label;
                if (Word.IsPunctuation(GetData().GetName()))
                    label = "PUP";
                else
                    label = data.GetName();
                SetShallowParseLayer(chunkType, label);
            }
            else
            {
                for (int i = 0; i < NumberOfChildren(); i++)
                    ((ParseNodeDrawable) GetChild(i)).SetShallowParseLayer(chunkType);
            }
        }

        public string ToTurkishSentence()
        {
            if (children.Count == 0)
            {
                if (GetLayerData(ViewLayerType.TURKISH_WORD) != null && !IsDummyNode())
                {
                    return " " + GetLayerData(ViewLayerType.TURKISH_WORD).Replace("-LRB-", "(")
                        .Replace("-RRB-", ")")
                        .Replace("-LSB-", "[").Replace("-RSB-", "]").Replace("-LCB-", "{")
                        .Replace("-RCB-", "}")
                        .Replace("-lrb-", "(").Replace("-rrb-", ")").Replace("-lsb-", "[")
                        .Replace("-rsb-", "]")
                        .Replace("-lcb-", "{").Replace("-rcb-", "}");
                }

                return " ";
            }

            var st = "";
            foreach (var aChild in children)
            {
                st += aChild.ToSentence();
            }

            return st;
        }

        public void CheckGazetteer(Gazetteer gazetteer, string word)
        {
            if (gazetteer.Contains(word) && GetParent().GetData().GetName().Equals("NNP"))
            {
                GetLayerInfo().SetLayerData(ViewLayerType.NER, gazetteer.GetName());
            }

            if (word.Contains("'") && gazetteer.Contains(word.Substring(0, word.IndexOf("'")))
                                   && GetParent().GetData().GetName().Equals("NNP"))
            {
                GetLayerInfo().SetLayerData(ViewLayerType.NER, gazetteer.GetName());
            }
        }

        public void GenerateParseNode(ParseNode parseNode, bool surfaceForm)
        {
            if (NumberOfChildren() == 0)
            {
                if (surfaceForm)
                {
                    parseNode.SetData(new Symbol(GetLayerData(ViewLayerType.TURKISH_WORD)));
                }
                else
                {
                    parseNode.SetData(new Symbol(GetLayerInfo().GetMorphologicalParseAt(0).GetWord().GetName()));
                }
            }
            else
            {
                parseNode.SetData(data);
                for (var i = 0; i < NumberOfChildren(); i++)
                {
                    var newChild = new ParseNode();
                    parseNode.AddChild(newChild);
                    ((ParseNodeDrawable) children[i]).GenerateParseNode(newChild, surfaceForm);
                }
            }
        }
        public override string ToString()
        {
            if (children.Count < 2)
            {
                if (children.Count < 1)
                {
                    return GetLayerData();
                }

                return "(" + data.GetName() + " " + children[0] + ")";
            }

            var st = "(" + data.GetName();
            foreach (var aChild in children)
            {
                st = st + " " + aChild;
            }

            return st + ") ";
        }
    }
}