using System;
using System.Collections.Generic;
using System.IO;
using AnnotatedSentence;
using AnnotatedTree.Processor;
using AnnotatedTree.Processor.Condition;
using Corpus;
using ParseTree;

namespace AnnotatedTree
{
    public class ParseTreeDrawable : ParseTree.ParseTree
    {
        private FileDescription _fileDescription;
        private string _name;

        public ParseTreeDrawable(string path, string rawFileName) : this(new FileDescription(path, rawFileName))
        {
        }

        public ParseTreeDrawable(string path, string extension, int index) : this(
            new FileDescription(path, extension, index))
        {
        }

        public ParseTreeDrawable(string path, FileDescription fileDescription) : this(
            new FileDescription(path, fileDescription.GetExtension(), fileDescription.GetIndex()))
        {
        }

        public ParseTreeDrawable(FileDescription fileDescription)
        {
            this._fileDescription = fileDescription;
            ReadFromFile(fileDescription.GetPath());
        }

        public void SetFileDescription(FileDescription fileDescription)
        {
            this._fileDescription = fileDescription;
        }

        public FileDescription GetFileDescription()
        {
            return _fileDescription;
        }

        public void CopyInfo(ParseTreeDrawable parseTree)
        {
            this._fileDescription = parseTree._fileDescription;
        }

        public void Reload()
        {
            ReadFromFile(_fileDescription.GetPath());
        }

        private void ReadFromFile(string currentPath)
        {
            var streamReader = new StreamReader(_fileDescription.GetFileName(currentPath));
            var line = streamReader.ReadLine();
            if (line.Contains("(") && line.Contains(")"))
            {
                line = line.Substring(line.IndexOf("(") + 1, line.LastIndexOf(")") - line.IndexOf("(") - 1).Trim();
                root = new ParseNodeDrawable(null, line, false, 0);
            }
            else
            {
                Console.WriteLine("File " + _fileDescription.GetFileName(currentPath) +
                                  " is not a valid parse tree file");
                root = null;
            }

            streamReader.Close();
        }

        public ParseTreeDrawable(string fileName)
        {
            _name = fileName;
            var streamReader = new StreamReader(fileName);
            var line = streamReader.ReadLine();
            if (line != null && line.Contains("(") && line.Contains(")"))
            {
                line = line.Substring(line.IndexOf("(") + 1, line.LastIndexOf(")") - line.IndexOf("(") - 1).Trim();
                root = new ParseNodeDrawable(null, line, false, 0);
            }
            else
            {
                root = null;
            }

            streamReader.Close();
        }

        public void SetName(string name)
        {
            this._name = name;
        }

        public string GetName()
        {
            return _name;
        }

        public void NextTree(int count)
        {
            if (_fileDescription.NextFileExists(count))
            {
                _fileDescription.AddToIndex(count);
                Reload();
            }
        }

        public void PreviousTree(int count)
        {
            if (_fileDescription.PreviousFileExists(count))
            {
                _fileDescription.AddToIndex(-count);
                Reload();
            }
        }

        public void Save()
        {
            var streamWriter = new StreamWriter(_fileDescription.GetFileName());
            streamWriter.WriteLine("( " + this + " )");
            streamWriter.Close();
        }

        public void SaveWithPath(string newPath)
        {
            var streamWriter = new StreamWriter(_fileDescription.GetFileName(newPath));
            streamWriter.WriteLine("( " + this + " )");
            streamWriter.Close();
        }

        public int GlossAgreementCount(ParseTree.ParseTree parseTree, ViewLayerType viewLayerType)
        {
            return ((ParseNodeDrawable) root).GlossAgreementCount((ParseNodeDrawable) parseTree.GetRoot(),
                viewLayerType);
        }

        public int StructureAgreementCount(ParseTree.ParseTree parseTree)
        {
            return ((ParseNodeDrawable) root).StructureAgreementCount((ParseNodeDrawable) parseTree.GetRoot());
        }

        public List<ParseNodeDrawable> Satisfy(ParseTreeSearchable tree)
        {
            return ((ParseNodeDrawable) root).Satisfy(tree);
        }

        public void UpdatePosTags()
        {
            ((ParseNodeDrawable) root).UpdatePosTags();
        }

        public int MaxDepth()
        {
            return ((ParseNodeDrawable) root).MaxDepth();
        }

        public void MoveLeft(ParseNode node)
        {
            if (root != node)
            {
                root.MoveLeft(node);
            }
        }

        public void MoveRight(ParseNode node)
        {
            if (root != node)
            {
                root.MoveRight(node);
            }
        }

        public void DivideIntoWords(ParseNodeDrawable parseNode)
        {
            var layers = parseNode.GetLayerInfo().DivideIntoWords();
            parseNode.GetParent().RemoveChild(parseNode);
            foreach (var layerInfo in layers)
            {
                var symbol = new Symbol(layerInfo.GetMorphologicalParseAt(0).GetTreePos());
                var child = new ParseNodeDrawable(symbol);
                parseNode.GetParent().AddChild(child);
                var grandChild = new ParseNodeDrawable(child, layerInfo.GetLayerDescription(), true,
                    parseNode.GetDepth() + 1);
                child.AddChild(grandChild);
                ((ParseNodeDrawable) root).UpdateDepths(0);
            }
        }

        public void MoveNode(ParseNode fromNode, ParseNode toNode)
        {
            if (root != fromNode)
            {
                var parent = fromNode.GetParent();
                parent.RemoveChild(fromNode);
                toNode.AddChild(fromNode);
                ((ParseNodeDrawable) root).UpdateDepths(0);
            }
        }

        public void MoveNode(ParseNode fromNode, ParseNode toNode, int childIndex)
        {
            if (root != fromNode)
            {
                var parent = fromNode.GetParent();
                parent.RemoveChild(fromNode);
                toNode.AddChild(childIndex, fromNode);
                ((ParseNodeDrawable) root).UpdateDepths(0);
            }
        }

        public void CombineWords(ParseNodeDrawable parent, ParseNodeDrawable child)
        {
            while (parent.NumberOfChildren() > 0)
            {
                parent.RemoveChild(parent.FirstChild());
            }

            parent.AddChild(child);
            ((ParseNodeDrawable) root).UpdateDepths(0);
        }

        public bool LayerExists(ViewLayerType viewLayerType)
        {
            return ((ParseNodeDrawable) root).LayerExists(viewLayerType);
        }

        public bool LayerAll(ViewLayerType viewLayerType)
        {
            return ((ParseNodeDrawable) root).LayerAll(viewLayerType);
        }

        public bool UpdateConnectedPredicate(string previousId, string currentId)
        {
            var modified = false;
            var nodeDrawableCollector =
                new NodeDrawableCollector((ParseNodeDrawable) root, new IsTurkishLeafNode());
            var leafList = nodeDrawableCollector.Collect();
            foreach (var parseNode in leafList)
            {
                if (parseNode.GetLayerInfo().GetArgument() != null &&
                    parseNode.GetLayerInfo().GetArgument().GetId() != null &&
                    parseNode.GetLayerInfo().GetArgument().GetId().Equals(previousId))
                {
                    parseNode.GetLayerInfo().SetLayerData(ViewLayerType.PROPBANK,
                        parseNode.GetLayerInfo().GetArgument().GetArgumentType() + "$" + currentId);
                    modified = true;
                }
            }

            return modified;
        }

        public ParseNodeDrawable NextLeafNode(ParseNodeDrawable parseNode)
        {
            var nodeDrawableCollector =
                new NodeDrawableCollector((ParseNodeDrawable) root, new IsTurkishLeafNode());
            var leafList = nodeDrawableCollector.Collect();
            for (var i = 0; i < leafList.Count - 1; i++)
            {
                if (leafList[i].Equals(parseNode))
                {
                    return leafList[i + 1];
                }
            }

            return null;
        }

        public ParseNodeDrawable PreviousLeafNode(ParseNodeDrawable parseNode)
        {
            var nodeDrawableCollector =
                new NodeDrawableCollector((ParseNodeDrawable) root, new IsTurkishLeafNode());
            var leafList = nodeDrawableCollector.Collect();
            for (var i = 1; i < leafList.Count; i++)
            {
                if (leafList[i].Equals(parseNode))
                {
                    return leafList[i - 1];
                }
            }

            return null;
        }

        public void SetShallowParseLayer(ChunkType chunkType)
        {
            if (root != null)
            {
                ((ParseNodeDrawable) root).SetShallowParseLayer(chunkType);
            }
        }

        public void ClearLayer(ViewLayerType layerType)
        {
            if (root != null)
            {
                ((ParseNodeDrawable) root).ClearLayer(layerType);
            }
        }

        public AnnotatedSentence.AnnotatedSentence GenerateAnnotatedSentence()
        {
            var sentence = new AnnotatedSentence.AnnotatedSentence("");
            var nodeDrawableCollector =
                new NodeDrawableCollector((ParseNodeDrawable) root, new IsTurkishLeafNode());
            var leafList = nodeDrawableCollector.Collect();
            foreach (var parseNode in leafList)
            {
                var layers = parseNode.GetLayerInfo();
                for (var i = 0; i < layers.GetNumberOfWords(); i++)
                {
                    sentence.AddWord(layers.ToAnnotatedWord(i));
                }
            }

            return sentence;
        }

        public AnnotatedSentence.AnnotatedSentence GenerateAnnotatedSentence(string language)
        {
            var sentence = new AnnotatedSentence.AnnotatedSentence("");
            var nodeDrawableCollector =
                new NodeDrawableCollector((ParseNodeDrawable) root, new IsEnglishLeafNode());
            var leafList = nodeDrawableCollector.Collect();
            foreach (var parseNode in leafList)
            {
                var newWord = new AnnotatedWord("{" + language + "=" + parseNode.GetData().GetName() + "}{posTag="
                                                + parseNode.GetParent().GetData().GetName() + "}");
                sentence.AddWord(newWord);
            }

            return sentence;
        }

        public ParseTree.ParseTree GenerateParseTree(bool surfaceForm)
        {
            var result = new ParseTree.ParseTree(new ParseNode(root.GetData()));
            ((ParseNodeDrawable)root).GenerateParseNode(result.GetRoot(), surfaceForm);
            return result;
        }
        public List<ParseNodeDrawable> ExtractNodesWithVerbs(WordNet.WordNet wordNet)
        {
            var nodeDrawableCollector =
                new NodeDrawableCollector((ParseNodeDrawable) root, new IsVerbNode(wordNet));
            return nodeDrawableCollector.Collect();
        }

        public List<ParseNodeDrawable> ExtractNodesWithPredicateVerbs(WordNet.WordNet wordNet)
        {
            var nodeDrawableCollector =
                new NodeDrawableCollector((ParseNodeDrawable) root, new IsPredicateVerbNode(wordNet));
            return nodeDrawableCollector.Collect();
        }

        public void ExtractVerbal()
        {
            var nodeDrawableCollector =
                new NodeDrawableCollector((ParseNodeDrawable) root, new IsVPNode());
            var nodeList = nodeDrawableCollector.Collect();
            foreach (var node in nodeList)
            {
                if (node.ExtractVerbal())
                {
                    return;
                }
            }
        }
    }
}