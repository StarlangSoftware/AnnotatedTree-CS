using AnnotatedSentence;

namespace AnnotatedTree.Processor.Condition
{
    public class IsTransferable : IsLeafNode
    {
        private readonly ViewLayerType _secondLanguage;

        public IsTransferable(ViewLayerType secondLanguage)
        {
            this._secondLanguage = secondLanguage;
        }

        /// <summary>
        /// Checks if the node is a leaf node and is not a None or Null node.
        /// </summary>
        /// <param name="parseNode">Parse node to check.</param>
        /// <returns>True if the node is a leaf node and is not a None or Null node, false otherwise.</returns>
        public override bool Satisfies(ParseNodeDrawable parseNode)
        {
            if (base.Satisfies(parseNode))
            {
                if (new IsNoneNode(_secondLanguage).Satisfies(parseNode))
                {
                    return false;
                }

                return !new IsNullElement().Satisfies(parseNode);
            }

            return false;
        }
    }
}