using System.Collections.Generic;
using AnnotatedTree.Processor.Condition;

namespace AnnotatedTree.Processor
{
    public class NodeDrawableCollector
    {
        private readonly NodeDrawableCondition _condition;
        private readonly ParseNodeDrawable _rootNode;

        /// <summary>
        /// Constructor for the NodeDrawableCollector class. NodeDrawableCollector's main aim is to collect a set of
        /// ParseNode's from a subtree rooted at rootNode, where the ParseNode's satisfy a given NodeCondition, which is
        /// implemented by other interface class.
        /// </summary>
        /// <param name="rootNode">Root node of the subtree</param>
        /// <param name="condition">The condition interface for which all nodes in the subtree rooted at rootNode will be checked</param>
        public NodeDrawableCollector(ParseNodeDrawable rootNode, NodeDrawableCondition condition)
        {
            this._rootNode = rootNode;
            this._condition = condition;
        }

        /// <summary>
        /// Private recursive method to check all descendants of the parseNode, if they ever satisfy the given node condition
        /// </summary>
        /// <param name="parseNode">Root node of the subtree</param>
        /// <param name="collected">The {@link ArrayList} where the collected ParseNode's will be stored.</param>
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

        /// <summary>
        /// Collects and returns all ParseNodes satisfying the node condition.
        /// </summary>
        /// <returns>All ParseNodes satisfying the node condition.</returns>
        public List<ParseNodeDrawable> Collect()
        {
            var result = new List<ParseNodeDrawable>();
            CollectNodes(_rootNode, result);
            return result;
        }
    }
}