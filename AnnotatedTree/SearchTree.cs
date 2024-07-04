using System.Collections.Generic;
using System.Xml;
using ParseTree;

namespace AnnotatedTree
{
    public class SearchTree
    {
        private readonly List<ParseTreeSearchable> _searchTrees;

        /// <summary>
        /// Constructs a set of ParseTreeSearchables from the given file name. It reads the xml file and for each xml element
        /// that contains ParseTreeSearchable, it calls its constructor.
        /// </summary>
        /// <param name="fileName">File that contains the search info.</param>
        public SearchTree(string fileName)
        {
            var doc = new XmlDocument();
            doc.Load(fileName);
            _searchTrees = new List<ParseTreeSearchable>();
            foreach (XmlNode parseNode in doc.DocumentElement.ChildNodes)
            {
                if (parseNode.Name == "tree")
                {
                    foreach (XmlNode nextNode in parseNode.ChildNodes)
                    {
                        while (nextNode.Name != "node" && nextNode.Name != "leaf")
                        {
                            _searchTrees.Add(new ParseTreeSearchable(nextNode));
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Returns the ParseNodes in the given tree that satisfy all conditions given in the search trees.
        /// </summary>
        /// <param name="tree">Tree in which search operation will be done</param>
        /// <returns>ParseNodes in the given tree that satisfy all conditions given in the search trees.</returns>
        public List<ParseNode> Satisfy(ParseTreeDrawable tree)
        {
            foreach (var treeSearchable in _searchTrees)
            {
                var tmpResult = tree.Satisfy(treeSearchable);
                if (tmpResult.Count > 0)
                {
                    var result = new List<ParseNode>();
                    foreach (var parseNodeDrawable in tmpResult){
                        result.Add(parseNodeDrawable);
                    }
                    return result;
                }
            }
            return new List<ParseNode>();
        }
    }
}