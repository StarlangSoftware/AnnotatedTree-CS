using AnnotatedTree.Processor.LeafConverter;

namespace AnnotatedTree.Processor
{
    public class TreeToStringConverter
    {
        private readonly LeafToStringConverter _converter;
        private readonly ParseTreeDrawable _parseTree;

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

        public string Convert()
        {
            return ConvertToString((ParseNodeDrawable) _parseTree.GetRoot());
        }

        public TreeToStringConverter(ParseTreeDrawable parseTree, LeafToStringConverter converter)
        {
            this._parseTree = parseTree;
            this._converter = converter;
        }
    }
}