using System.Collections.Generic;
using System.Xml;
using ParseTree;

namespace AnnotatedTree
{
    public class SearchTree
    {
        private readonly List<ParseTreeSearchable> _searchTrees;

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

        public List<ParseNode> Satisfy(ParseTreeDrawable tree)
        {
            foreach (var treeSearchable in _searchTrees)
            {
                List<ParseNodeDrawable> tmpResult = tree.Satisfy(treeSearchable);
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