namespace AnnotatedTree.Processor.Condition
{
    public class IsNodeWithSymbol : NodeDrawableCondition
    {
        private readonly string _symbol;

        public IsNodeWithSymbol(string symbol){
            this._symbol = symbol;
        }
        public bool Satisfies(ParseNodeDrawable parseNode)
        {
            if (parseNode.NumberOfChildren() > 0){
                return parseNode.GetData().ToString().Equals(_symbol);
            }

            return false;
        }
    }
}