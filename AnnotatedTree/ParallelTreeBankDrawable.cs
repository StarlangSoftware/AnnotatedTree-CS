using AnnotatedSentence;
using ParseTree;

namespace AnnotatedTree
{
    public class ParallelTreeBankDrawable : ParallelTreeBank
    {
        public ParallelTreeBankDrawable(string folder1, string folder2)
        {
            _fromTreeBank = new TreeBankDrawable(folder1);
            _toTreeBank = new TreeBankDrawable(folder2);
            RemoveDifferentTrees();
        }

        public ParallelTreeBankDrawable(string folder1, string folder2, string pattern)
        {
            _fromTreeBank = new TreeBankDrawable(folder1, pattern);
            _toTreeBank = new TreeBankDrawable(folder2, pattern);
            RemoveDifferentTrees();
        }

        public ParseTreeDrawable FromTree(int index)
        {
            return (ParseTreeDrawable) _fromTreeBank.Get(index);
        }

        public ParseTreeDrawable ToTree(int index)
        {
            return (ParseTreeDrawable) _toTreeBank.Get(index);
        }

        public TreeBankDrawable FromTreeBank()
        {
            return (TreeBankDrawable) _fromTreeBank;
        }

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