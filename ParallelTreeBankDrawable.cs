using System;
using AnnotatedSentence;

namespace AnnotatedTree
{
    public class ParallelTreeBankDrawable
    {
        private readonly TreeBankDrawable _fromTreeBank;
        private readonly TreeBankDrawable _toTreeBank;

        private void RemoveDifferentTrees()
        {
            int i, j;
            i = 0;
            j = 0;
            while (i < _fromTreeBank.Size() && j < _toTreeBank.Size())
            {
                if (string.Compare(_fromTreeBank.Get(i).GetName(), _toTreeBank.Get(j).GetName(), StringComparison.Ordinal) < 0)
                {
                    _fromTreeBank.RemoveTree(i);
                }
                else
                {
                    if (string.Compare(_toTreeBank.Get(j).GetName(), _fromTreeBank.Get(i).GetName(), StringComparison.Ordinal) < 0)
                    {
                        _toTreeBank.RemoveTree(j);
                    }
                    else
                    {
                        i++;
                        j++;
                    }
                }
            }

            while (i < _fromTreeBank.Size())
            {
                _fromTreeBank.RemoveTree(i);
            }

            while (j < _toTreeBank.Size())
            {
                _toTreeBank.RemoveTree(j);
            }
        }

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

        public int Size()
        {
            return _fromTreeBank.Size();
        }

        public ParseTreeDrawable FromTree(int index)
        {
            return _fromTreeBank.Get(index);
        }

        public ParseTreeDrawable ToTree(int index)
        {
            return _toTreeBank.Get(index);
        }

        public TreeBankDrawable FromTreeBank()
        {
            return _fromTreeBank;
        }

        public TreeBankDrawable ToTreeBank()
        {
            return _toTreeBank;
        }

        public double InterAnnotatorGlossAgreement(ViewLayerType viewLayerType)
        {
            int agreement = 0, total = 0;
            for (var i = 0; i < _fromTreeBank.Size(); i++)
            {
                var parseTree1 = _fromTreeBank.Get(i);
                var parseTree2 = _toTreeBank.Get(i);
                total += parseTree1.LeafCount();
                agreement += parseTree1.GlossAgreementCount(parseTree2, viewLayerType);
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
                agreement += parseTree1.StructureAgreementCount(parseTree2);
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