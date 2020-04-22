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

        public TreeBankDrawable(List<ParseTree.ParseTree> parseTrees)
        {
            this.parseTrees = parseTrees;
        }

        public void ReadTree(string path, string fileName)
        {
            var parseTree = new ParseTreeDrawable(fileName);
            if (parseTree.GetRoot() != null)
            {
                parseTree.SetName(fileName);
                parseTree.SetFileDescription(new FileDescription(path, fileName));
                parseTrees.Add(parseTree);
            }
            else
            {
                Console.WriteLine("Parse Tree " + fileName + " can not be read");
            }
        }

        public TreeBankDrawable(string folder)
        {
            parseTrees = new List<ParseTree.ParseTree>();
            var listOfFiles = Directory.GetFiles(folder);
            Array.Sort(listOfFiles);
            foreach (var file in listOfFiles)
            {
                ReadTree(file, folder);
            }
        }

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
                    parseTree.SetName(file);
                    parseTree.SetFileDescription(new FileDescription(folder, file));
                    parseTrees.Add(parseTree);
                }
                else
                {
                    Console.WriteLine("Parse Tree " + file + " can not be read");
                }
            }
        }

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

        public TreeBankDrawable(string path, string extension, int from, int to)
        {
            parseTrees = new List<ParseTree.ParseTree>();
            for (int i = from; i <= to; i++)
            {
                ParseTreeDrawable parseTree = new ParseTreeDrawable(new FileDescription(path, extension, i));
                if (parseTree.GetRoot() != null)
                {
                    parseTrees.Add(parseTree);
                }
            }
        }

        public List<ParseTree.ParseTree> GetParseTrees()
        {
            return parseTrees;
        }

        public new ParseTreeDrawable Get(int index)
        {
            return (ParseTreeDrawable) parseTrees[index];
        }

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
                TreeToStringConverter treeToStringConverter = new TreeToStringConverter(parseTree, leafToLanguageConverter);
                string sentence = treeToStringConverter.Convert();
                if (sentence != ""){
                    corpus.AddSentence(new Sentence(sentence));
                } else {
                    Console.WriteLine("Parse Tree " + parseTree.GetName() + " is not translated");
                }
            }
            return corpus;
        }

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
                ParseTreeDrawable parseTree = (ParseTreeDrawable) tree;
                nodeList = nodeList.Union(parseTree.ExtractNodesWithVerbs(wordNet)).ToList();
            }
            return nodeList;
        }

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

        public void Sort(){
            parseTrees.Sort(new ParseTreeComparator());
        }

    }
}