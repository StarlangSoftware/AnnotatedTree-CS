using AnnotatedSentence;
using ParseTree;

namespace AnnotatedTree
{
    public class ParallelTreeBankDrawable : ParallelTreeBank
    {
        /// <summary>
        /// Constructor for two parallel treebanks.
        /// </summary>
        /// <param name="folder1">Folder containing the parse tree for the first tree bank.</param>
        /// <param name="folder2">Folder containing the parse tree for the second tree bank.</param>
        public ParallelTreeBankDrawable(string folder1, string folder2)
        {
            _fromTreeBank = new TreeBankDrawable(folder1);
            _toTreeBank = new TreeBankDrawable(folder2);
            RemoveDifferentTrees();
        }

        /// <summary>
        /// Constructor for two parallel treebanks.
        /// </summary>
        /// <param name="folder1">Folder containing the parse tree for the first tree bank.</param>
        /// <param name="folder2">Folder containing the parse tree for the second tree bank.</param>
        /// <param name="pattern">File name pattern for the files.</param>
        public ParallelTreeBankDrawable(string folder1, string folder2, string pattern)
        {
            _fromTreeBank = new TreeBankDrawable(folder1, pattern);
            _toTreeBank = new TreeBankDrawable(folder2, pattern);
            RemoveDifferentTrees();
        }

        /// <summary>
        /// Accessor for the parse tree of the first tree bank.
        /// </summary>
        /// <param name="index">Position of the parse tree for the first tree bank.</param>
        /// <returns>The parse tree of the first tree bank at position index.</returns>
        public ParseTreeDrawable FromTree(int index)
        {
            return (ParseTreeDrawable) _fromTreeBank.Get(index);
        }

        /// <summary>
        /// Accessor for the parse tree of the second tree bank.
        /// </summary>
        /// <param name="index">Position of the parse tree for the second tree bank.</param>
        /// <returns>The parse tree of the second tree bank at position index.</returns>
        public ParseTreeDrawable ToTree(int index)
        {
            return (ParseTreeDrawable) _toTreeBank.Get(index);
        }

        /// <summary>
        /// Accessor for the first tree bank.
        /// </summary>
        /// <returns>First tree bank.</returns>
        public TreeBankDrawable FromTreeBank()
        {
            return (TreeBankDrawable) _fromTreeBank;
        }

        /// <summary>
        /// Accessor for the second tree bank.
        /// </summary>
        /// <returns>Second tree bank.</returns>
        public TreeBankDrawable ToTreeBank()
        {
            return (TreeBankDrawable) _toTreeBank;
        }

        public double InterAnnotatorGlossAgreement(ViewLayerType viewLayerType)
        {
            int agreement = 0, total = 0;
            for (var i = 0; i < _fromTreeBank.Size(); i++)
            {
                var parseTree1 = _fromTreeBank.Get(i);
                var parseTree2 = _toTreeBank.Get(i);
                total += parseTree1.LeafCount();
                agreement += ((ParseTreeDrawable) parseTree1).GlossAgreementCount(parseTree2, viewLayerType);
            }

            return agreement / (total + 0.0);
        }

        public double InterAnnotatorStructureAgreement()
        {
            int agreement = 0, total = 0;
            for (var i = 0; i < _fromTreeBank.Size(); i++)
            {
                var parseTree1 = _fromTreeBank.Get(i);
                var parseTree2 = _toTreeBank.Get(i);
                total += parseTree1.NodeCountWithMultipleChildren();
                agreement += ((ParseTreeDrawable) parseTree1).StructureAgreementCount(parseTree2);
            }

            return agreement / (total + 0.0);
        }

        public void StripPunctuation()
        {
            _fromTreeBank.StripPunctuation();
            _toTreeBank.StripPunctuation();
        }
    }
}