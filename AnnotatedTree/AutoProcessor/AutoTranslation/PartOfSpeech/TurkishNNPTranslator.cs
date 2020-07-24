using System.Collections.Generic;
using Dictionary.Dictionary;

namespace AnnotatedTree.AutoProcessor.AutoTranslation.PartOfSpeech
{
    public class TurkishNNPTranslator : TurkishNounTranslator
    {
        public TurkishNNPTranslator(ParseNodeDrawable parseNode, List<string> parentList, List<string> englishWordList,
            string prefix, string lastWordForm, TxtWord lastWord, TxtDictionary txtDictionary) : base(parseNode,
            parentList, englishWordList, prefix, lastWordForm, lastWord, txtDictionary)

        {
        }

        public new string Translate()
        {
            string[] posArray =  {
                "VBZ", "VBP", "IN", "TO", "POS"
            }
            ;
            string[][] wordArray =  {
                    new [] {
                        "'s", "is"
                    },
                    new [] {
                        "'re", "are"
                    },
                    new [] {
                        "of", "in", "on", "at", "into", "with", "by", "from", "since"
                    },
                    new [] {
                        "to"
                    },
                    new string[] {
                    }
                }
            ;
            string[][] suffixArray =  {
                    new [] {
                        "'DHr", "'DHr"
                    },
                    new [] {
                        "'DHr", "'DHr"
                    },
                    new [] {
                        "'nHn", "'DA", "'DA", "'DA", "'nA", "'ylA", "'ylA", "'DAn", "'DAn"
                    },
                    new [] {
                    
                        "'yA"
                    },
                    new [] {
                        "'nHn"
                    }
                }
            ;
            if (parentList.Count > 1)
            {
                var result = TranslateNouns(posArray, wordArray, suffixArray, parentList, englishWordList, prefix,
                    lastWord, lastWordForm);
                if (result != null)
                {
                    return result;
                }
            }

            return prefix + lastWord.GetName();
        }
    }
}