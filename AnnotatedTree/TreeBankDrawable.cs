using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnnotatedSentence;
using AnnotatedTree.Processor;
using AnnotatedTree.Processor.LeafConverter;
using Corpus;
using ParseTree;
using Util;

namespace AnnotatedTree
{
    public class TreeBankDrawable : TreeBank
    {
        public static string ENGLISH_PATH = "../English/";
        public static string TURKISH_PATH = "../Turkish/";
        public static string TURKISH_PARSE_PATH = "../Turkish-Parse/";
        public static string TURKISH_PHRASE_PATH = "../Turkish-Phrase/";
        public static string TREE_IMAGES = "../Tree-Images/";

        /// <summary>
        /// A simple constructor for TreeBankDrawable. Sets the parseTrees field with the given parameter.
        /// </summary>
        /// <param name="parseTrees">Parse trees in the treebank.</param>
        public TreeBankDrawable(List<ParseTree.ParseTree> parseTrees)
        {
            this.parseTrees = parseTrees;
        }

        public void ReadTree(string path, string fileName)
        {
            var parseTree = new ParseTreeDrawable(fileName);
            if (parseTree.GetRoot() != null)
            {
                parseTree.SetName(RemovePath(fileName));
                parseTree.SetFileDescription(new FileDescription(path, RemovePath(fileName)));
                parseTrees.Add(parseTree);
            }
            else
            {
                Console.WriteLine("Parse Tree " + fileName + " can not be read");
            }
        }

        /// <summary>
        /// A constructor of {@link TreeBankDrawable} class which reads all {@link ParseTreeDrawable} files inside the given
        /// folder. For each file inside that folder, the constructor creates a ParseTreeDrawable and puts in inside the list
        /// parseTrees.
        /// </summary>
        /// <param name="folder">Folder where all parseTrees reside.</param>
        public TreeBankDrawable(string folder)
        {
            parseTrees = new List<ParseTree.ParseTree>();
            var listOfFiles = Directory.GetFiles(folder);
            Array.Sort(listOfFiles);
            foreach (var file in listOfFiles)
            {
                ReadTree(folder, file);
            }
        }

        /// <summary>
        /// A constructor of {@link TreeBankDrawable} class which reads all {@link ParseTreeDrawable} files with the file
        /// name satisfying the given pattern inside the given folder. For each file inside that folder, the constructor
        /// creates a ParseTreeDrawable and puts in inside the list parseTrees.
        /// </summary>
        /// <param name="folder">Folder where all parseTrees reside.</param>
        /// <param name="pattern">File pattern such as "." ".train" ".test".</param>
        public TreeBankDrawable(string folder, string pattern)
        {
            parseTrees = new List<ParseTree.ParseTree>();
            var listOfFiles = Directory.GetFiles(folder);
            Array.Sort(listOfFiles);
            foreach (var file in listOfFiles)
            {
                if (!file.Contains(pattern))
                    continue;
                var parseTree = new ParseTreeDrawable(file);
                if (parseTree.GetRoot() != null)
                {
                    parseTree.SetName(RemovePath(file));
                    parseTree.SetFileDescription(new FileDescription(folder, RemovePath(file)));
                    parseTrees.Add(parseTree);
                }
                else
                {
                    Console.WriteLine("Parse Tree " + file + " can not be read");
                }
            }
        }

        /// <summary>
        /// A constructor of {@link TreeBankDrawable} class which reads the files numbered in the given interval with the
        /// file name having thr given extension inside the given folder. For each file inside that folder, the constructor
        /// creates a ParseTreeDrawable and puts in inside the list parseTrees.
        /// </summary>
        /// <param name="path">Folder where all parseTrees reside.</param>
        /// <param name="extension">File pattern such as "train" "test".</param>
        /// <param name="interval">Starting  and ending index for the ParseTrees read.</param>
        public TreeBankDrawable(string path, string extension, Interval interval)
        {
            parseTrees = new List<ParseTree.ParseTree>();
            for (var i = 0; i < interval.Size(); i++)
            {
                for (var j = interval.GetFirst(i); j <= interval.GetLast(i); j++)
                {
                    var parseTree = new ParseTreeDrawable(new FileDescription(path, extension, j));
                    if (parseTree.GetRoot() != null)
                    {
                        parseTrees.Add(parseTree);
                    }
                }
            }
        }

        /// <summary>
        /// A constructor of {@link TreeBankDrawable} class which reads the files numbered from from to to with the file name
        /// having thr given extension inside the given folder. For each file inside that folder, the constructor
        /// creates a ParseTreeDrawable and puts in inside the list parseTrees.
        /// </summary>
        /// <param name="path">Folder where all parseTrees reside.</param>
        /// <param name="extension">File pattern such as "train" "test".</param>
        /// <param name="from">Starting index for the ParseTrees read.</param>
        /// <param name="to">Ending index for the ParseTrees read.</param>
        public TreeBankDrawable(string path, string extension, int from, int to)
        {
            parseTrees = new List<ParseTree.ParseTree>();
            for (var i = from; i <= to; i++)
            {
                var parseTree = new ParseTreeDrawable(new FileDescription(path, extension, i));
                if (parseTree.GetRoot() != null)
                {
                    parseTrees.Add(parseTree);
                }
            }
        }

        /// <summary>
        /// Accessor for the parseTrees attribute
        /// </summary>
        /// <returns>ParseTrees attribute</returns>
        public List<ParseTree.ParseTree> GetParseTrees()
        {
            return parseTrees;
        }

        /// <summary>
        /// Accessor for a specific tree with the given position in the array.
        /// </summary>
        /// <param name="index">Index of the parseTree.</param>
        /// <returns>Tree that is in the position index</returns>
        public new ParseTreeDrawable Get(int index)
        {
            return (ParseTreeDrawable) parseTrees[index];
        }

        /// <summary>
        /// Accessor for a specific tree with the given file name.
        /// </summary>
        /// <param name="fileName">File name of the tree</param>
        /// <returns>Tree that has the given file name</returns>
        public ParseTreeDrawable Get(string fileName)
        {
            foreach (var tree in parseTrees){
                if (((ParseTreeDrawable) tree).GetFileDescription().GetRawFileName().Equals(fileName))
                {
                    return (ParseTreeDrawable) tree;
                }
            }
            return null;
        }
        
        public Corpus.Corpus CreateCorpus(LeafToLanguageConverter leafToLanguageConverter){
            var corpus = new Corpus.Corpus();
            foreach (var tree in parseTrees){
                var parseTree = (ParseTreeDrawable) tree;
                var treeToStringConverter = new TreeToStringConverter(parseTree, leafToLanguageConverter);
                var sentence = treeToStringConverter.Convert();
                if (sentence != ""){
                    corpus.AddSentence(new Sentence(sentence));
                } else {
                    Console.WriteLine("Parse Tree " + parseTree.GetName() + " is not translated");
                }
            }
            return corpus;
        }

        /// <summary>
        /// Clears the given layer for all nodes in all trees
        /// </summary>
        /// <param name="layerType">Layer name</param>
        public void ClearLayer(ViewLayerType layerType){
            foreach (var tree in parseTrees){
                ParseTreeDrawable parseTree = (ParseTreeDrawable) tree;
                parseTree.ClearLayer(layerType);
                parseTree.Save();
            }
        }

        public List<ParseNodeDrawable> ExtractVerbs(WordNet.WordNet wordNet){
            var nodeList = new List<ParseNodeDrawable>();
            foreach (var tree in parseTrees){
                var parseTree = (ParseTreeDrawable) tree;
                nodeList = nodeList.Union(parseTree.ExtractNodesWithVerbs(wordNet)).ToList();
            }
            return nodeList;
        }

        /// <summary>
        /// Returns list of trees that contain at least one verb which is annotated as 'PREDICATE'.
        /// </summary>
        /// <param name="wordNet">Wordnet used for checking the pos tag of the synset.</param>
        /// <returns>List of trees that contain at least one verb which is annotated as 'PREDICATE'.</returns>
        public List<ParseTreeDrawable> ExtractTreesWithPredicates(WordNet.WordNet wordNet){
            var treeList = new List<ParseTreeDrawable>();
            foreach (var tree in parseTrees){
                var parseTree = (ParseTreeDrawable) tree;
                if (parseTree.ExtractNodesWithPredicateVerbs(wordNet).Count > 0){
                    treeList.Add(parseTree);
                }
            }
            return treeList;
        }

        public void RemoveTree(int index){
            parseTrees.RemoveAt(index);
        }

        /// <summary>
        /// Sorts the tres in the treebanks according to their filenames.
        /// </summary>
        public void Sort(){
            parseTrees.Sort(new ParseTreeComparator());
        }

    }
}