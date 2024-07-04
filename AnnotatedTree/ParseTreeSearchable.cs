using System.Xml;

namespace AnnotatedTree
{
    public class ParseTreeSearchable : ParseTree.ParseTree
    {
        /// <summary>
        /// Construct a ParseTreeSearchable from a xml element.
        /// </summary>
        /// <param name="rootNode">XmlElement that contains the root node information.</param>
        public ParseTreeSearchable(XmlNode rootNode){
            root = new ParseNodeSearchable(null, rootNode);
        }
        
    }
}