using System;
using System.Collections.Generic;
using Dictionary.Dictionary;
using MorphologicalAnalysis;

namespace AnnotatedTree.AutoProcessor.AutoTranslation.PartOfSpeech
{
    public class TurkishVBTranslator : TurkishVerbTranslator
    {
        public TurkishVBTranslator(ParseNodeDrawable parseNode, List<String> parentList, List<String> englishWordList,
            String prefix, String lastWordForm, TxtWord lastWord) : base(parseNode, parentList, englishWordList, prefix,
            lastWordForm, lastWord)
        {
        }

        public String translate()
        {
            Transition transition;
            if (parentList.Count > 1 && parentList[1].Equals("MD"))
            {
                if (englishWordList[1].Equals("will"))
                {
                    if (parentList.Count > 3 && parentList[2].Equals("RB") && parentList[3].Equals("PRP"))
                    {
                        transition = new Transition("mAyAcAk" + PersonalSuffix1(englishWordList[3].ToLower()));
                    }
                    else
                    {
                        if (parentList.Count > 2 && parentList[2].Equals("PRP"))
                        {
                            transition =
                                new Transition("yAcAk" + PersonalSuffix1(englishWordList[2].ToLower()));
                        }
                        else
                        {
                            if (parentList.Count > 2 && parentList[2].Equals("RB"))
                            {
                                transition = new Transition("mAyAcAk");
                            }
                            else
                            {
                                transition = new Transition("yAcAk");
                            }
                        }
                    }

                    return prefix + transition.MakeTransition(lastWord, lastWordForm);
                }
                else
                {
                    if (englishWordList[1].Equals("can") ||
                        englishWordList[1].Equals("may") ||
                        englishWordList[1].Equals("might") ||
                        englishWordList[1].Equals("could"))
                    {
                        if (parentList.Count > 3 && parentList[2].Equals("RB") && parentList[3].Equals("PRP"))
                        {
                            transition =
                                new Transition("mAyAbilir" + PersonalSuffix1(englishWordList[3].ToLower()));
                        }
                        else
                        {
                            if (parentList.Count > 2 && parentList[2].Equals("PRP"))
                            {
                                transition =
                                    new Transition("yAbilir" + PersonalSuffix1(englishWordList[2].ToLower()));
                            }
                            else
                            {
                                if (parentList.Count > 2 && parentList[2].Equals("RB"))
                                {
                                    transition = new Transition("mAz");
                                }
                                else
                                {
                                    transition = new Transition("yAbilir");
                                }
                            }
                        }

                        return prefix + transition.MakeTransition(lastWord, lastWordForm);
                    }
                    else
                    {
                        if (englishWordList[1].Equals("would") || englishWordList[1].Equals("wo"))
                        {
                            if (parentList.Count > 3 && parentList[2].Equals("RB") &&
                                parentList[3].Equals("PRP"))
                            {
                                transition =
                                    new Transition("mHyor" + PersonalSuffix1(englishWordList[3].ToLower()));
                            }
                            else
                            {
                                if (parentList.Count > 2 && parentList[2].Equals("PRP"))
                                {
                                    transition =
                                        new Transition("Hyor" + PersonalSuffix1(englishWordList[2].ToLower()));
                                }
                                else
                                {
                                    if (parentList.Count > 2 && parentList[2].Equals("RB"))
                                    {
                                        transition = new Transition("mHyor");
                                    }
                                    else
                                    {
                                        transition = new Transition("Hyor");
                                    }
                                }
                            }

                            return prefix + transition.MakeTransition(lastWord, lastWordForm);
                        }
                    }
                }
            }
            else
            {
                if (parentList.Count > 2 && parentList[1].Equals("TO") &&
                    englishWordList[1].Equals("to") && parentList[2].Equals("VBD") &&
                    englishWordList[2].Equals("had"))
                {
                    transition = new Transition("mAlHyDH");
                    if (parentList.Count > 3 && parentList[3].Equals("PRP"))
                    {
                        transition = new Transition("mAlHyDH" + PersonalSuffix2(englishWordList[3].ToLower()));
                    }

                    return prefix + transition.MakeTransition(lastWord, lastWordForm);
                }
                else
                {
                    if (parentList.Count > 2 && parentList[1].Equals("TO") &&
                        englishWordList[1].Equals("to") && parentList[2].Equals("VB") &&
                        englishWordList[2].Equals("have"))
                    {
                        transition = new Transition("mAlH");
                        if (parentList.Count > 3 && parentList[3].Equals("PRP"))
                        {
                            transition = new Transition("mAlH" + PersonalSuffix3(englishWordList[3].ToLower()));
                        }

                        return prefix + transition.MakeTransition(lastWord, lastWordForm);
                    }
                    else
                    {
                        if (parentList.Count > 2 && parentList[1].Equals("RB") &&
                            parentList[2].Equals("VBD") && englishWordList[2].Equals("did"))
                        {
                            transition = new Transition("mADH");
                            if (parentList.Count > 3 && parentList[3].Equals("PRP"))
                            {
                                transition =
                                    new Transition("mADH" + PersonalSuffix2(englishWordList[3].ToLower()));
                            }

                            return prefix + transition.MakeTransition(lastWord, lastWordForm);
                        }
                        else
                        {
                            if (parentList.Count > 2 && parentList[1].Equals("RB") &&
                                parentList[2].Equals("VBP") && englishWordList[2].Equals("do"))
                            {
                                transition = new Transition("mAz");
                                if (parentList.Count > 3 && parentList[3].Equals("PRP"))
                                {
                                    transition =
                                        new Transition("mA" + PersonalSuffix4(englishWordList[3].ToLower()));
                                }

                                return prefix + transition.MakeTransition(lastWord, lastWordForm);
                            }
                            else
                            {
                                if (parentList.Count > 2 && parentList[1].Equals("RB") &&
                                    parentList[2].Equals("VBZ") && englishWordList[2].Equals("does"))
                                {
                                    transition = new Transition("mAz");
                                    if (parentList.Count > 3 && parentList[3].Equals("PRP"))
                                    {
                                        transition =
                                            new Transition(
                                                "mA" + PersonalSuffix4(englishWordList[3].ToLower()));
                                    }

                                    return prefix + transition.MakeTransition(lastWord, lastWordForm);
                                }
                            }
                        }
                    }
                }
            }

            return AddSuffix("mAk", prefix, lastWord, lastWordForm);
        }
    }
}