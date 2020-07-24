using System.Collections.Generic;
using Dictionary.Dictionary;
using MorphologicalAnalysis;

namespace AnnotatedTree.AutoProcessor.AutoTranslation.PartOfSpeech
{
    public class TurkishNNSTranslator : TurkishNounTranslator
    {
        public TurkishNNSTranslator(ParseNodeDrawable parseNode, List<string> parentList, List<string> englishWordList,
            string prefix, string lastWordForm, TxtWord lastWord, TxtDictionary txtDictionary) : base(parseNode,
            parentList, englishWordList, prefix, lastWordForm, lastWord, txtDictionary)

        {
        }

        public new string Translate()
        {
            bool previousWordNoun = false;
            string[] posArray2 =  {
                "IN", "TO"
            }
            ;
            string[] posArray3 =  {
                "POS", "PRP$"
            }
            ;
            string[][] wordArray2 =  {
                    new [] {
                        "of", "in", "on", "at", "into", "with", "from", "since", "by", "until", "than"
                    },
                new [] {
                    "to"
                }
            }
            ;
            string[][] wordArray3 =  {
                    new string[] {
                    },
                new [] {
                    "my", "your", "his", "her", "its", "our", "their"
                }
            }
            ;
            string[][] suffixArray2 =  {
                    new [] {
                        "nHn", "DA", "DA", "DA", "nA", "ylA", "DAn", "DAn", "ylA", "nA", "DAn"
                    },
                new [] {
                    "yA"
                }
            }
            ;
            string[][] suffixArray2Prime =  {
                    new [] {
                        "nHn", "nDA", "nDA", "nDA", "nA", "ylA", "nDAn", "nDAn", "ylA", "nA", "nDAn"
                    },
                new [] {
                    "nA"
                }
            }
            ;
            string[][] suffixArray3 =  {
                    new [] {
                        "nHn"
                    },
                new [] {
                    "Hm", "Hn", "sH", "sH", "sH", "HmHz", "H"
                }
            }
            ;
            if (parentList.Count > 1)
            {
                if (prefix != null)
                {
                    TxtWord prefixWord = (TxtWord) txtDictionary.GetWord(prefix.Trim());
                    if (prefixWord != null && prefixWord.IsNominal())
                    {
                        previousWordNoun = true;
                    }
                }

                Transition transition;
                string result;
                if (IsLastWordOfNounPhrase(parseNode) || previousWordNoun)
                {
                    transition = new Transition("lArH");
                    string newLastWordForm = transition.MakeTransition(lastWord, lastWordForm);
                    result = TranslateNouns(posArray2, wordArray2, suffixArray2Prime, parentList, englishWordList,
                        prefix, lastWord, newLastWordForm);
                    if (result != null)
                    {
                        return result;
                    }
                }
                else
                {
                    transition = new Transition("lAr");
                    string newLastWordForm = transition.MakeTransition(lastWord, lastWordForm);
                    result = TranslateNouns(posArray3, wordArray3, suffixArray3, parentList, englishWordList, prefix,
                        lastWord, newLastWordForm);
                    if (result != null)
                    {
                        var result2 = TranslateNouns(posArray2, wordArray2, suffixArray2, parentList, englishWordList, "",
                            lastWord, result);
                        if (result2 != null)
                        {
                            result = result2;
                        }
                    }
                    else
                    {
                        result = TranslateNouns(posArray2, wordArray2, suffixArray2, parentList, englishWordList,
                            prefix, lastWord, newLastWordForm);
                    }
                }

                if (result != null)
                {
                    return result;
                }

                if (IsLastWordOfNounPhrase(parseNode) || previousWordNoun)
                {
                    return AddSuffix("lArH", prefix, lastWord, lastWordForm);
                }

                return AddSuffix("lAr", prefix, lastWord, lastWordForm);
            }

            return AddSuffix("lAr", prefix, lastWord, lastWordForm);
        }
    }
}