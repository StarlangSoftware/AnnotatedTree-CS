using AnnotatedTree.Processor.NodeModification;

namespace AnnotatedTree.Processor
{
    public class TreeModifier
    {
        private readonly ParseTreeDrawable _parseTree;
        private readonly NodeModifier _nodeModifier;

        private void NodeModify(ParseNodeDrawable parseNode)
        {
            _nodeModifier.Modifier(parseNode);
            for (var i = 0; i < parseNode.NumberOfChildren(); i++)
            {
                NodeModify((ParseNodeDrawable) parseNode.GetChild(i));
            }
        }

        public void Modify()
        {
            NodeModify((ParseNodeDrawable) _parseTree.GetRoot());
        }

        public TreeModifier(ParseTreeDrawable parseTree, NodeModifier nodeModifier)
        {
            this._parseTree = parseTree;
            this._nodeModifier = nodeModifier;
        }
    }
}