using System.Xml;

namespace AnnotatedTree
{
    public class ParseTreeSearchable : ParseTree.ParseTree
    {
        public ParseTreeSearchable(XmlNode rootNode){
            root = new ParseNodeSearchable(null, rootNode);
        }
        
    }
}