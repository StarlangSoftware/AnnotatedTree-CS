using System;
using System.Collections.Generic;
using System.IO;
using Corpus;
using ParseTree;

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
    }
}