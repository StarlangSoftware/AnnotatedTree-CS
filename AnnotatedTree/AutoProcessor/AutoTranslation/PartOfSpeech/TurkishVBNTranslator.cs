using System.Collections.Generic;
using Dictionary.Dictionary;

namespace AnnotatedTree.AutoProcessor.AutoTranslation.PartOfSpeech
{
    public class TurkishVBNTranslator : TurkishVerbTranslator
    {
        public TurkishVBNTranslator(ParseNodeDrawable parseNode, List<string> parentList, List<string> englishWordList,
            string prefix, string lastWordForm, TxtWord lastWord) : base(parseNode, parentList, englishWordList, prefix,
            lastWordForm, lastWord)
        {
        }

        public new string Translate()
        {
            if (parentList.Count > 1 && (parentList[1].Equals("VBZ") || parentList[1].Equals("VBP")) &&
                (englishWordList[1].Equals("is") || englishWordList[1].Equals("are") ||
                 englishWordList[1].Equals("'s") || englishWordList[1].Equals("'re")))
            {
                return AddSuffix("Hr", prefix, lastWord, lastWordForm);
                /*is/are done*/
            }

            if (parentList.Count > 2 && parentList[1].Equals("RB") &&
                (parentList[2].Equals("VBZ") || parentList[2].Equals("VBP")) &&
                (englishWordList[2].Equals("is") || englishWordList[2].Equals("are") ||
                 englishWordList[2].Equals("'s") || englishWordList[2].Equals("'re")))
            {
                return AddSuffix("mAz", prefix, lastWord, lastWordForm);
                /*is/are not done*/
            }

            if (parentList.Count > 2 && parentList[1].Equals("VB") && parentList[2].Equals("MD") &&
                (englishWordList[2].Equals("can") ||
                 englishWordList[2].Equals("will")))
            {
                return AddSuffix("Abilir", prefix, lastWord, lastWordForm);
                /*can/will be done*/
            }
            else
            {
                if (parentList.Count > 3 && parentList[1].Equals("VB") && parentList[2].Equals("MD") &&
                    (englishWordList[2].Equals("can") ||
                     englishWordList[2].Equals("will")) && parentList[3].Equals("RB"))
                {
                    return AddSuffix("AmAz", prefix, lastWord, lastWordForm);
                    /*can/will not be done*/
                }
                else
                {
                    if (parentList.Count > 3 && parentList[1].Equals("VB") &&
                        parentList[2].Equals("RB") && parentList[3].Equals("MD") &&
                        (englishWordList[3].Equals("can") ||
                         englishWordList[3].Equals("will")))
                    {
                        return AddSuffix("AmAz", prefix, lastWord, lastWordForm);
                        /*can/will not be done*/
                    }
                    else
                    {
                        if (parentList.Count > 1 && parentList[1].Equals("VBD") &&
                            (englishWordList[1].Equals("was") ||
                             englishWordList[1].Equals("were")))
                        {
                            return AddSuffix("DH", prefix, lastWord, lastWordForm);
                            /*was/were done*/
                        }
                        else
                        {
                            if (parentList.Count > 2 && parentList[1].Equals("RB") &&
                                parentList[2].Equals("VBD") &&
                                (englishWordList[2].Equals("was") ||
                                 englishWordList[2].Equals("were")))
                            {
                                return AddSuffix("mADH", prefix, lastWord, lastWordForm);
                                /*was/were not done*/
                            }
                            else
                            {
                                if (parentList.Count > 2 && parentList[1].Equals("VBN") &&
                                    parentList[2].Equals("VBD"))
                                {
                                    return AddSuffix("Hyordu", prefix, lastWord, lastWordForm);
                                    /*had been done*/
                                }
                                else
                                {
                                    if (parentList.Count > 3 && parentList[1].Equals("VBN") &&
                                        parentList[2].Equals("RB") && parentList[3].Equals("VBD"))
                                    {
                                        return AddSuffix("mHyordu", prefix, lastWord, lastWordForm);
                                        /*had not been done*/
                                    }
                                    else
                                    {
                                        if (parentList.Count > 2 && parentList[1].Equals("VBN") &&
                                            parentList[2].Equals("VBZ"))
                                        {
                                            /*has been done*/
                                            if (parentList[3].Equals("PRP"))
                                            {
                                                return AddSuffix("DH", prefix, lastWord, lastWordForm,
                                                    englishWordList[3].ToLower());
                                            }
                                            else
                                            {
                                                return AddSuffix("DH", prefix, lastWord, lastWordForm);
                                            }
                                        }
                                        else
                                        {
                                            if (parentList.Count > 3 && parentList[1].Equals("VBN") &&
                                                parentList[2].Equals("RB") &&
                                                parentList[3].Equals("VBZ"))
                                            {
                                                /*has not been done*/
                                                if (parentList[4].Equals("PRP"))
                                                {
                                                    return AddSuffix("mADH", prefix, lastWord, lastWordForm,
                                                        englishWordList[4].ToLower());
                                                }
                                                else
                                                {
                                                    return AddSuffix("mADH", prefix, lastWord, lastWordForm);
                                                }
                                            }
                                            else
                                            {
                                                if (parentList.Count > 3 && parentList[1].Equals("VB") &&
                                                    parentList[2].Equals("MD") &&
                                                    (englishWordList[2].Equals("could") ||
                                                     englishWordList[2].Equals("would")) &&
                                                    parentList[3].Equals("RB"))
                                                {
                                                    return AddSuffix("mADH", prefix, lastWord, lastWordForm);
                                                    /*could/would not be done*/
                                                }
                                                else
                                                {
                                                    if (parentList.Count > 2 &&
                                                        parentList[1].Equals("VB") &&
                                                        parentList[2].Equals("MD") &&
                                                        (englishWordList[2].Equals("could") ||
                                                         englishWordList[2].Equals("would")))
                                                    {
                                                        return AddSuffix("AbilirDH", prefix, lastWord,
                                                            lastWordForm);
                                                        /*could/would be done*/
                                                    }
                                                    else
                                                    {
                                                        if (parentList.Count > 3 &&
                                                            parentList[1].Equals("VB") &&
                                                            parentList[2].Equals("RB") &&
                                                            parentList[3].Equals("MD") &&
                                                            (englishWordList[3].Equals("could") ||
                                                             englishWordList[3].Equals("would")))
                                                        {
                                                            return AddSuffix("AmAzDH", prefix, lastWord,
                                                                lastWordForm);
                                                            /*could/would not be done*/
                                                        }
                                                        else
                                                        {
                                                            if (parentList.Count > 1 &&
                                                                (parentList[1].Equals("VBZ") ||
                                                                 parentList[1].Equals("VBP")) &&
                                                                (englishWordList[1]
                                                                     .Equals("has") ||
                                                                 englishWordList[1]
                                                                     .Equals("have") ||
                                                                 englishWordList[1]
                                                                     .Equals("'s") ||
                                                                 englishWordList[1]
                                                                     .Equals("'ve")))
                                                            {
                                                                /*has/have done*/
                                                                if (parentList[2].Equals("PRP"))
                                                                {
                                                                    return AddSuffix("mHştH", prefix, lastWord,
                                                                        lastWordForm,
                                                                        englishWordList[2].ToLower());
                                                                }
                                                                else
                                                                {
                                                                    return AddSuffix("mHştH", prefix, lastWord,
                                                                        lastWordForm);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (parentList.Count > 2 &&
                                                                    parentList[1].Equals("RB") &&
                                                                    (parentList[2].Equals("VBZ") ||
                                                                     parentList[2].Equals("VBP")) &&
                                                                    (englishWordList[2]
                                                                         .Equals("has") ||
                                                                     englishWordList[2]
                                                                         .Equals("have")))
                                                                {
                                                                    /*has/have not done*/
                                                                    if (parentList[3].Equals("PRP"))
                                                                    {
                                                                        return AddSuffix("mADH", prefix,
                                                                            lastWord, lastWordForm,
                                                                            englishWordList[3]
                                                                                .ToLower());
                                                                    }
                                                                    else
                                                                    {
                                                                        return AddSuffix("mADH", prefix,
                                                                            lastWord, lastWordForm);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (parentList.Count > 1 &&
                                                                        parentList[1].Equals("VBD") &&
                                                                        englishWordList[1].Equals("had"))
                                                                    {
                                                                        /*had done*/
                                                                        if (parentList[2].Equals("PRP"))
                                                                        {
                                                                            return AddSuffix("mHştH", prefix,
                                                                                lastWord, lastWordForm,
                                                                                englishWordList[2]
                                                                                    .ToLower());
                                                                        }
                                                                        else
                                                                        {
                                                                            return AddSuffix("mHştH", prefix,
                                                                                lastWord, lastWordForm);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (parentList.Count > 1 &&
                                                                            parentList[1].Equals("RB") &&
                                                                            parentList[2].Equals("VBD") &&
                                                                            englishWordList[2]
                                                                                .Equals("had"))
                                                                        {
                                                                            /*had not done*/
                                                                            if (parentList[3].Equals("PRP"))
                                                                            {
                                                                                return AddSuffix("mAmHştH",
                                                                                    prefix, lastWord,
                                                                                    lastWordForm,
                                                                                    englishWordList[3]
                                                                                        .ToLower());
                                                                            }
                                                                            else
                                                                            {
                                                                                return AddSuffix("mAmHştH",
                                                                                    prefix, lastWord,
                                                                                    lastWordForm);
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}