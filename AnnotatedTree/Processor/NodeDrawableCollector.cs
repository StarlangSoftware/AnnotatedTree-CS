using System.Collections.Generic;
using AnnotatedTree.Processor.Condition;

namespace AnnotatedTree.Processor
{
    public class NodeDrawableCollector
    {
        private readonly NodeDrawableCondition _condition;
        private readonly ParseNodeDrawable _rootNode;

        public NodeDrawableCollector(ParseNodeDrawable rootNode, NodeDrawableCondition condition)
        {
            this._rootNode = rootNode;
            this._condition = condition;
        }

        private void CollectNodes(ParseNodeDrawable parseNode, List<ParseNodeDrawable> collected)
        {
            if (_condition == null || _condition.Satisfies(parseNode))
            {
                collected.Add(parseNode);
            }
            for (var i = 0; i < parseNode.NumberOfChildren(); i++)
            {
                CollectNodes((ParseNodeDrawable) parseNode.GetChild(i), collected);
            }
        }

        public List<ParseNodeDrawable> Collect()
        {
            var result = new List<ParseNodeDrawable>();
            CollectNodes(_rootNode, result);
            return result;
        }
    }
}