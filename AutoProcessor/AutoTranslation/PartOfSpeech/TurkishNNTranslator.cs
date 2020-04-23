using System.Collections.Generic;
using Dictionary.Dictionary;
using MorphologicalAnalysis;

namespace AnnotatedTree.AutoProcessor.AutoTranslation.PartOfSpeech
{
    public class TurkishNNTranslator : TurkishNounTranslator
    {
        public TurkishNNTranslator(ParseNodeDrawable parseNode, List<string> parentList, List<string> englishWordList,
            string prefix, string lastWordForm, TxtWord lastWord, TxtDictionary txtDictionary) : base(parseNode,
            parentList, englishWordList, prefix, lastWordForm, lastWord, txtDictionary)
        {
        }

        public new string Translate()
        {
            bool previousWordNoun = false;
            string[] posArray1 =
                {
                    "VBZ", "VBP"
                }
                ;
            string[] posArray2 =
                {
                    "IN", "TO"
                }
                ;
            string[] posArray3 =
                {
                    "POS", "PRP$"
                }
                ;
            string[] posArray3Prime =
                {
                    "POS"
                }
                ;
            string[][] wordArray1 =
                {
                    new []
                    {
                        "'s", "is"
                    },
                    new []
                    {
                        "'re", "are"
                    }
                }
                ;
            string[][] wordArray2 =
                {
                    new []
                    {
                        "of", "in", "on", "at", "into", "with", "from", "since", "by", "until", "than"
                    },
                    new []
                    {
                        "to"
                    }
                }
                ;
            string[][] wordArray3 =
                {
                    new string[]
                    {
                    },
                    new []
                    {
                        "my", "your", "his", "her", "its", "our", "their"
                    }
                }
                ;
            string[][] wordArray3Prime =
                {
                    new string[]
                    {
                    }
                }
                ;
            string[][] suffixArray1 =
                {
                    new []
                    {
                        "DHr", "DHr"
                    },
                    new []
                    {
                        "DHr", "DHr"
                    }
                }
                ;
            string[][] suffixArray2 =
                {
                    new []
                    {
                        "nHn", "DA", "DA", "DA", "nA", "ylA", "DAn", "DAn", "ylA", "nA", "DAn"
                    },
                    new []
                    {
                        "yA"
                    }
                }
                ;
            string[][] suffixArray2Prime =
                {
                    new []
                    {
                        "nHn", "nDA", "nDA", "nDA", "nA", "ylA", "nDAn", "nDAn", "ylA", "nA", "nDAn"
                    },
                    new []
                    {
                        "nA"
                    }
                }
                ;
            string[][] suffixArray3 =
                {
                    new []
                    {
                        "nHn"
                    },
                    new []
                    {
                        "Hm", "Hn", "sH", "sH", "sH", "HmHz", "lArH"
                    }
                }
                ;
            string[][] suffixArray3Prime =
                {
                    new []
                    {
                        "nHn"
                    }
                }
                ;
            if (parentList.Count > 1)
            {
                if (prefix != null)
                {
                    var prefixWord = (TxtWord) txtDictionary.GetWord(prefix.Trim());
                    if (prefixWord != null && prefixWord.IsNominal())
                    {
                        previousWordNoun = true;
                    }
                }

                string newLastWordForm;
                string result;
                string result2;
                if (IsLastWordOfNounPhrase(parseNode) || previousWordNoun)
                {
                    var transition = new Transition("sH");
                    newLastWordForm = transition.MakeTransition(lastWord, lastWordForm);
                    result = TranslateNouns(posArray3Prime, wordArray3Prime, suffixArray3Prime, parentList,
                        englishWordList, prefix, lastWord, newLastWordForm);
                    if (result != null)
                    {
                        result2 = TranslateNouns(posArray2, wordArray2, suffixArray2Prime, parentList, englishWordList,
                            "", lastWord, result);
                        if (result2 != null)
                        {
                            result = result2;
                        }
                    }
                    else
                    {
                        result = TranslateNouns(posArray2, wordArray2, suffixArray2Prime, parentList, englishWordList,
                            prefix, lastWord, newLastWordForm);
                    }

                    if (result != null)
                    {
                        return result;
                    }

                    result = TranslateNouns(posArray1, wordArray1, suffixArray1, parentList, englishWordList, prefix,
                        lastWord, newLastWordForm);
                }
                else
                {
                    newLastWordForm = lastWordForm;
                    result = TranslateNouns(posArray3, wordArray3, suffixArray3, parentList, englishWordList, prefix,
                        lastWord, newLastWordForm);
                    if (result != null)
                    {
                        result2 = TranslateNouns(posArray2, wordArray2, suffixArray2, parentList, englishWordList, "",
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

                    if (result == null)
                    {
                        result = TranslateNouns(posArray1, wordArray1, suffixArray1, parentList, englishWordList,
                            prefix, lastWord, newLastWordForm);
                    }
                }

                if (result != null)
                {
                    return result;
                }

                if (IsLastWordOfNounPhrase(parseNode) || previousWordNoun)
                {
                    return AddSuffix("sH", prefix, lastWord, lastWordForm);
                }
            }

            return prefix + lastWord.GetName();
        }
    }
}