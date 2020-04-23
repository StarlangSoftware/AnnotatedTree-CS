using System.Collections.Generic;
using AnnotatedSentence;
using Dictionary.Dictionary;

namespace AnnotatedTree.AutoProcessor.AutoTranslation.PartOfSpeech
{
    public class TurkishNounTranslator : TurkishPartOfSpeechTranslator
    {
        protected TxtDictionary txtDictionary;

        public TurkishNounTranslator(ParseNodeDrawable parseNode, List<string> parentList, List<string> englishWordList,
            string prefix, string lastWordForm, TxtWord lastWord, TxtDictionary txtDictionary) : base(parseNode,
            parentList, englishWordList, prefix, lastWordForm, lastWord)
        {
            this.txtDictionary = txtDictionary;
        }

        protected string TranslateNouns(string[] posArray, string[][] wordArray, string[][] suffixArray,
            List<string> parentList, List<string> englishWordList, string prefix, TxtWord currentRoot, string nounRoot)
        {
            for (int i = 0; i < posArray.Length; i++)
            {
                if (parentList[1].Equals(posArray[i]))
                {
                    if (wordArray[i].Length == 0)
                    {
                        return AddSuffix(suffixArray[i][0], prefix, currentRoot, nounRoot);
                    }
                    else
                    {
                        for (int j = 0; j < wordArray[i].Length; j++)
                        {
                            if (englishWordList[1].Equals(wordArray[i][j]))
                            {
                                return AddSuffix(suffixArray[i][j], prefix, currentRoot, nounRoot);
                            }
                        }
                    }
                }
            }

            return null;
        }

        protected bool IsLastWordOfNounPhrase(ParseNodeDrawable parseNode)
        {
            var parent = (ParseNodeDrawable) parseNode.GetParent();
            var grandParent = (ParseNodeDrawable) parent.GetParent();
            var next = (ParseNodeDrawable) parseNode.NextSibling();
            var previous = (ParseNodeDrawable) parseNode.PreviousSibling();
            if (parent.IsLastChild(parseNode))
            {
                if (previous != null && previous.GetData().GetName().StartsWith("J") && previous.LastChild().IsLeaf())
                {
                    string word = ((ParseNodeDrawable) previous.LastChild()).GetLayerData(ViewLayerType.TURKISH_WORD);
                    if (word != null && txtDictionary.GetWord(word) != null &&
                        ((TxtWord) txtDictionary.GetWord(word)).IsNominal())
                    {
                        return true;
                    }
                }

                if (previous != null && previous.GetData().GetName().StartsWith("N"))
                {
                    return true;
                }

                if (grandParent != null && grandParent.IsLastChild(parent) && grandParent.NumberOfChildren() == 2)
                {
                    ParseNodeDrawable parentPrevious = (ParseNodeDrawable) parent.PreviousSibling();
                    if (parentPrevious.GetData().GetName().Equals("PP") &&
                        parentPrevious.LastChild().GetData().GetName().Equals("IN"))
                    {
                        ParseNodeDrawable inNode = (ParseNodeDrawable) parentPrevious.LastChild().LastChild();
                        if (inNode != null && inNode.GetLayerData(ViewLayerType.ENGLISH_WORD) != null &&
                            inNode.GetLayerData(ViewLayerType.ENGLISH_WORD).Equals("of"))
                        {
                            return true;
                        }

                        return false;
                    }

                    return false;
                }

                return false;
            }

            if (next != null && previous != null)
            {
                return !(next.GetData().GetName().StartsWith("N")) && previous.GetData().GetName().StartsWith("N");
            }

            return false;
        }
    }
}