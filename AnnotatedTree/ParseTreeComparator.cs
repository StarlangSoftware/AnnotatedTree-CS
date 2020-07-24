using System;
using System.Collections.Generic;

namespace AnnotatedTree
{
    public class ParseTreeComparator : Comparer<ParseTree.ParseTree>
    {
        public override int Compare(ParseTree.ParseTree x, ParseTree.ParseTree y)
        {
            return string.Compare(((ParseTreeDrawable) x).GetFileDescription().GetFileName(), ((ParseTreeDrawable) y).GetFileDescription().GetFileName(), StringComparison.Ordinal);
        }
    }
}