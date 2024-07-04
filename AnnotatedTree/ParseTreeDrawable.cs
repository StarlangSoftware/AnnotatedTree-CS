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

        /// <summary>
        /// Constructor for the ParseTreeDrawable. Sets the file description and reads the tree from the file description.
        /// </summary>
        /// <param name="path">Path of the tree</param>
        /// <param name="rawFileName">File name of the tree such as 0123.train.</param>
        public ParseTreeDrawable(string path, string rawFileName) : this(new FileDescription(path, rawFileName))
        {
        }

        /// <summary>
        /// Another constructor for the ParseTreeDrawable. Sets the file description and reads the tree from the file
        /// description.
        /// </summary>
        /// <param name="path">Path of the tree</param>
        /// <param name="extension">Extension of the file such as train, test or dev.</param>
        /// <param name="index">Index of the file such as 1235.</param>
        public ParseTreeDrawable(string path, string extension, int index) : this(
            new FileDescription(path, extension, index))
        {
        }

        /// <summary>
        /// Another constructor for the ParseTreeDrawable. Sets the file description and reads the tree from the file
        /// description.
        /// </summary>
        /// <param name="path">Path of the tree</param>
        /// <param name="fileDescription">File description that contains the path, index and extension information.</param>
        public ParseTreeDrawable(string path, FileDescription fileDescription) : this(
            new FileDescription(path, fileDescription.GetExtension(), fileDescription.GetIndex()))
        {
        }

        /// <summary>
        /// Another constructor for the ParseTreeDrawable. Sets the file description and reads the tree from the file
        /// description.
        /// </summary>
        /// <param name="fileDescription">File description that contains the path, index and extension information.</param>
        public ParseTreeDrawable(FileDescription fileDescription)
        {
            this._fileDescription = fileDescription;
            ReadFromFile(fileDescription.GetPath());
        }

        /// <summary>
        /// Mutator method for the fileDescription attribute.
        /// </summary>
        /// <param name="fileDescription">New fileDescription value.</param>
        public void SetFileDescription(FileDescription fileDescription)
        {
            this._fileDescription = fileDescription;
        }

        /// <summary>
        /// Accessor method for the fileDescription attribute.
        /// </summary>
        /// <returns>FileDescription attribute.</returns>
        public FileDescription GetFileDescription()
        {
            return _fileDescription;
        }

        /// <summary>
        /// Copies the file description information from the given parse tree.
        /// </summary>
        /// <param name="parseTree">Parse tree whose file description information will be copied.</param>
        public void CopyInfo(ParseTreeDrawable parseTree)
        {
            this._fileDescription = parseTree._fileDescription;
        }

        /// <summary>
        /// Reloads the tree from the input file.
        /// </summary>
        public void Reload()
        {
            ReadFromFile(_fileDescription.GetPath());
        }

        /// <summary>
        ///  Reads the parse tree from the given file description with path replaced with the currentPath. It sets the root
        /// node which calls ParseNodeDrawable constructor recursively.
        /// </summary>
        /// <param name="currentPath">Path of the tree</param>
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

        /// <summary>
        /// Another constructor of the ParseTree. The method takes the file containing a single line as input and constructs
        /// the whole tree by calling the ParseNode constructor recursively.
        /// </summary>
        /// <param name="fileName">File containing a single line for a ParseTree</param>
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

        /// <summary>
        /// Loads the next tree according to the index of the parse tree. For example, if the current
        /// tree fileName is 0123.train, after the call of nextTree(3), the method will load 0126.train. If the next tree
        /// does not exist, nothing will happen.
        /// </summary>
        /// <param name="count">Number of trees to go forward</param>
        public void NextTree(int count)
        {
            if (_fileDescription.NextFileExists(count))
            {
                _fileDescription.AddToIndex(count);
                Reload();
            }
        }

        /// <summary>
        /// Loads the previous tree according to the index of the parse tree. For example, if the current
        /// tree fileName is 0123.train, after the call of previousTree(4), the method will load 0119.train. If the
        /// previous tree does not exist, nothing will happen.
        /// </summary>
        /// <param name="count">Number of trees to go backward</param>
        public void PreviousTree(int count)
        {
            if (_fileDescription.PreviousFileExists(count))
            {
                _fileDescription.AddToIndex(-count);
                Reload();
            }
        }

        /// <summary>
        /// Saves current tree.
        /// </summary>
        public void Save()
        {
            var streamWriter = new StreamWriter(_fileDescription.GetFileName());
            streamWriter.WriteLine("( " + this + " )");
            streamWriter.Close();
        }

        /// <summary>
        /// Saves current tree to the newPath with other file properties staying the same.
        /// </summary>
        /// <param name="newPath">Path to which tree will be saved</param>
        public void SaveWithPath(string newPath)
        {
            var streamWriter = new StreamWriter(_fileDescription.GetFileName(newPath));
            streamWriter.WriteLine("( " + this + " )");
            streamWriter.Close();
        }

        /// <summary>
        /// Returns the number of gloss agreements between this tree and the given tree. Two nodes agree in
        /// glosses if they are both leaf nodes and their layer info are the same.
        /// </summary>
        /// <param name="parseTree">Parse tree to compare in gloss manner</param>
        /// <param name="viewLayerType">Layer name to compare</param>
        /// <returns></returns>
        public int GlossAgreementCount(ParseTree.ParseTree parseTree, ViewLayerType viewLayerType)
        {
            return ((ParseNodeDrawable) root).GlossAgreementCount((ParseNodeDrawable) parseTree.GetRoot(),
                viewLayerType);
        }

        /// <summary>
        /// Returns the number of structural agreement between this tree and the given tree. Two nodes agree in
        /// structural manner if they have the same number of children and all of their children have the same tags in the
        /// same order.
        /// </summary>
        /// <param name="parseTree">Parse tree to compare in structural manner</param>
        /// <returns>The number of structural agreement between this tree and the given tree.</returns>
        public int StructureAgreementCount(ParseTree.ParseTree parseTree)
        {
            return ((ParseNodeDrawable) root).StructureAgreementCount((ParseNodeDrawable) parseTree.GetRoot());
        }

        /// <summary>
        /// Returns all nodes in the current tree those satisfy the conditions in the given second tree.
        /// </summary>
        /// <param name="tree">Tree containing the search condition</param>
        /// <returns>All nodes in the current tree those satisfy the conditions in the given second tree.</returns>
        public List<ParseNodeDrawable> Satisfy(ParseTreeSearchable tree)
        {
            return ((ParseNodeDrawable) root).Satisfy(tree);
        }

        /// <summary>
        /// Updates all pos tags in the leaf nodes according to the morphological tag in those leaves.
        /// nodes.
        /// </summary>
        public void UpdatePosTags()
        {
            ((ParseNodeDrawable) root).UpdatePosTags();
        }

        /// <summary>
        /// Calculates the maximum depth of the tree.
        /// </summary>
        /// <returns>The maximum depth of the tree.</returns>
        public int MaxDepth()
        {
            return ((ParseNodeDrawable) root).MaxDepth();
        }

        /// <summary>
        /// Swaps the given child node of this node with the previous sibling of that given node. If the given node is the
        /// leftmost child, it swaps with the last node.
        /// </summary>
        /// <param name="node">Node to be swapped.</param>
        public void MoveLeft(ParseNode node)
        {
            if (root != node)
            {
                root.MoveLeft(node);
            }
        }

        /// <summary>
        /// Swaps the given child node of this node with the next sibling of that given node. If the given node is the
        /// rightmost child, it swaps with the first node.
        /// </summary>
        /// <param name="node">Node to be swapped.</param>
        public void MoveRight(ParseNode node)
        {
            if (root != node)
            {
                root.MoveRight(node);
            }
        }

        /// <summary>
        /// Divides the given node into multiple parse nodes if it contains more than one word. The parent node will be
        /// the same for the new nodes, original node is deleted from the children, the pos tags of the new parse nodes will
        /// be determined according to their morphological parses.
        /// </summary>
        /// <param name="parseNode">Parse node to be divided</param>
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

        /// <summary>
        /// Moves the subtree rooted at fromNode as a child to the node toNode.
        /// </summary>
        /// <param name="fromNode">Subtree root node to be moved.</param>
        /// <param name="toNode">Node to which a new subtree will be added.</param>
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

        /// <summary>
        /// Moves the subtree rooted at fromNode as a child to the node toNode at position childIndex.
        /// </summary>
        /// <param name="fromNode">Subtree root node to be moved.</param>
        /// <param name="toNode">Node to which a new subtree will be added.</param>
        /// <param name="childIndex">New child index of the toNode.</param>
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

        /// <summary>
        /// Removed the first child of the parent node and adds the given child node as a child to that node.
        /// </summary>
        /// <param name="parent">Parent node.</param>
        /// <param name="child">New child node to be added.</param>
        public void CombineWords(ParseNodeDrawable parent, ParseNodeDrawable child)
        {
            while (parent.NumberOfChildren() > 0)
            {
                parent.RemoveChild(parent.FirstChild());
            }

            parent.AddChild(child);
            ((ParseNodeDrawable) root).UpdateDepths(0);
        }

        /// <summary>
        /// The method checks if all nodes in the tree has the annotation in the given layer.
        /// </summary>
        /// <param name="viewLayerType"></param>
        /// <returns></returns>
        public bool LayerExists(ViewLayerType viewLayerType)
        {
            return ((ParseNodeDrawable) root).LayerExists(viewLayerType);
        }

        /// <summary>
        /// Checks if all nodes in the tree has annotation with the given layer.
        /// </summary>
        /// <param name="viewLayerType">Layer name</param>
        /// <returns>True if all nodes in the tree has annotation with the given layer, false otherwise.</returns>
        public bool LayerAll(ViewLayerType viewLayerType)
        {
            return ((ParseNodeDrawable) root).LayerAll(viewLayerType);
        }

        /// <summary>
        /// Replaces id's of predicates, which have previousId as synset id, with currentId. Replaces also predicate id's of
        /// frame elements, which have predicate id's previousId, with currentId.
        /// </summary>
        /// <param name="previousId">Previous id of the synset.</param>
        /// <param name="currentId">Replacement id.</param>
        /// <returns>Returns true, if any replacement has been done; false otherwise.</returns>
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

        /// <summary>
        /// Returns the leaf node that comes one after the given parse node according to the inorder traversal.
        /// </summary>
        /// <param name="parseNode">Input parse node.</param>
        /// <returns>The leaf node that comes one after the given parse node according to the inorder traversal.</returns>
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

        /// <summary>
        /// Returns the leaf node that comes one before the given parse node according to the inorder traversal.
        /// </summary>
        /// <param name="parseNode">Input parse node.</param>
        /// <returns>The leaf node that comes one before the given parse node according to the inorder traversal.</returns>
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

        /// <summary>
        /// Sets the shallow parse layer of all nodes in the tree according to the given chunking type.
        /// </summary>
        /// <param name="chunkType">Type of the chunking used to annotate.</param>
        public void SetShallowParseLayer(ChunkType chunkType)
        {
            if (root != null)
            {
                ((ParseNodeDrawable) root).SetShallowParseLayer(chunkType);
            }
        }

        /// <summary>
        /// Clears the given layer for all nodes in the tree
        /// </summary>
        /// <param name="layerType">Layer name</param>
        public void ClearLayer(ViewLayerType layerType)
        {
            if (root != null)
            {
                ((ParseNodeDrawable) root).ClearLayer(layerType);
            }
        }

        /// <summary>
        /// Constructs an AnnotatedSentence object from the Turkish tree. Collects all leaf nodes, then for each leaf node
        /// converts layer info of all words at that node to AnnotatedWords. Layers are converted to the counterparts in the
        /// AnnotatedWord.
        /// </summary>
        /// <returns>AnnotatedSentence counterpart of the Turkish tree</returns>
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

        /// <summary>
        /// Constructs an AnnotatedSentence object from the English tree. Collects all leaf nodes, then for each leaf node
        /// converts the word with its parents pos tag to AnnotatedWord.
        /// </summary>
        /// <param name="language">English or Persian.</param>
        /// <returns>AnnotatedSentence counterpart of the English tree</returns>
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

        /// <summary>
        /// Recursive method that generates a new parse tree by replacing the tag information of the all parse nodes (with all
        /// its descendants) with respect to the morphological annotation of all parse nodes (with all its descendants)
        /// of the current parse tree.
        /// </summary>
        /// <param name="surfaceForm">If true, tag will be replaced with the surface form annotation.</param>
        /// <returns>A new parse tree by replacing the tag information of the all parse nodes with respect to the
        /// morphological annotation of all parse nodes of the current parse tree.</returns>
        public ParseTree.ParseTree GenerateParseTree(bool surfaceForm)
        {
            var result = new ParseTree.ParseTree(new ParseNode(root.GetData()));
            ((ParseNodeDrawable)root).GenerateParseNode(result.GetRoot(), surfaceForm);
            return result;
        }
        
        /// <summary>
        /// Returns list of nodes that contain verbs.
        /// </summary>
        /// <param name="wordNet">Wordnet used for checking the pos tag of the synset.</param>
        /// <returns>List of nodes that contain verbs.</returns>
        public List<ParseNodeDrawable> ExtractNodesWithVerbs(WordNet.WordNet wordNet)
        {
            var nodeDrawableCollector =
                new NodeDrawableCollector((ParseNodeDrawable) root, new IsVerbNode(wordNet));
            return nodeDrawableCollector.Collect();
        }

        /// <summary>
        /// Returns list of nodes that contain verbs which are annotated as 'PREDICATE'.
        /// </summary>
        /// <param name="wordNet">Wordnet used for checking the pos tag of the synset.</param>
        /// <returns>List of nodes that contain verbs which are annotated as 'PREDICATE'.</returns>
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