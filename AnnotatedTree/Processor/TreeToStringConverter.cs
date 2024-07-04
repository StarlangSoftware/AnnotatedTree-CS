using AnnotatedTree.Processor.LeafConverter;

namespace AnnotatedTree.Processor
{
    public class TreeToStringConverter
    {
        private readonly LeafToStringConverter _converter;
        private readonly ParseTreeDrawable _parseTree;

        /// <summary>
        /// Converts recursively a parse node to a string. If it is a leaf node, calls the converter's leafConverter method,
        /// otherwise concatenates the converted strings of its children.
        /// </summary>
        /// <param name="parseNode">Parse node to convert to string.</param>
        /// <returns>String form of the parse node and all of its descendants.</returns>
        private string ConvertToString(ParseNodeDrawable parseNode)
        {
            if (parseNode.IsLeaf())
            {
                return _converter.LeafConverter(parseNode);
            }

            var st = "";
            for (var i = 0; i < parseNode.NumberOfChildren(); i++)
            {
                st += ConvertToString((ParseNodeDrawable) parseNode.GetChild(i));
            }

            return st;
        }

        /// <summary>
        /// Calls the convertToString method with root of the tree to convert the parse tree to string.
        /// </summary>
        /// <returns>String form of the parse tree.</returns>
        public string Convert()
        {
            return ConvertToString((ParseNodeDrawable) _parseTree.GetRoot());
        }

        /// <summary>
        /// Constructor of the TreeToStringConverter class. Sets the attributes.
        /// </summary>
        /// <param name="parseTree">Parse tree to be converted.</param>
        /// <param name="converter">Node to string converter interface.</param>
        public TreeToStringConverter(ParseTreeDrawable parseTree, LeafToStringConverter converter)
        {
            this._parseTree = parseTree;
            this._converter = converter;
        }
    }
}