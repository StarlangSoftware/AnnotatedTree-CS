using System;
using System.Collections.Generic;

namespace AnnotatedTree
{
    public class ParseTreeComparator : Comparer<ParseTree.ParseTree>
    {
        /// <summary>
        /// Comparator method that compares two parse treess according to their file names.
        /// </summary>
        /// <param name="x">First parse tree to be compared.</param>
        /// <param name="y">Second parse tree to be compared.</param>
        /// <returns>-1 if the first file name comes before the second file name lexicographically, 1 otherwise, and 0 if
        /// both file names are equal.</returns>
        public override int Compare(ParseTree.ParseTree x, ParseTree.ParseTree y)
        {
            return string.Compare(((ParseTreeDrawable) x).GetFileDescription().GetFileName(), ((ParseTreeDrawable) y).GetFileDescription().GetFileName(), StringComparison.Ordinal);
        }
    }
}