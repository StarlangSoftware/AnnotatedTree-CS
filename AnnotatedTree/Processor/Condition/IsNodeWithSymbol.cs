namespace AnnotatedTree.Processor.Condition
{
    public class IsNodeWithSymbol : NodeDrawableCondition
    {
        private readonly string _symbol;

        /// <summary>
        /// Stores the symbol to check.
        /// </summary>
        /// <param name="symbol">Symbol to check</param>
        public IsNodeWithSymbol(string symbol){
            this._symbol = symbol;
        }
        
        /// <summary>
        /// Checks if the tag of the parse node is equal to the given symbol.
        /// </summary>
        /// <param name="parseNode">Parse node to check.</param>
        /// <returns>True if the tag of the parse node is equal to the given symbol, false otherwise.</returns>
        public bool Satisfies(ParseNodeDrawable parseNode)
        {
            if (parseNode.NumberOfChildren() > 0){
                return parseNode.GetData().ToString().Equals(_symbol);
            }

            return false;
        }
    }
}