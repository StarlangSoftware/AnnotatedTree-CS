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

        public new bool Satisfies(ParseNodeDrawable parseNode)
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