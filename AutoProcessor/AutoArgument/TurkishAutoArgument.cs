using System;
using AnnotatedSentence;
using ParseTree;
using PropBank;

namespace AnnotatedTree.AutoProcessor.AutoArgument
{
    public class TurkishAutoArgument : AutoArgument
    {
        public TurkishAutoArgument() : base(ViewLayerType.TURKISH_WORD)
        {
        }

        private bool CheckAncestors(ParseNode parseNode, String name)
        {
            while (parseNode != null)
            {
                if (parseNode.GetData().GetName().Equals(name))
                {
                    return true;
                }

                parseNode = parseNode.GetParent();
            }

            return false;
        }

        private bool CheckAncestorsUntil(ParseNode parseNode, String suffix)
        {
            while (parseNode != null)
            {
                if (parseNode.GetData().GetName().Contains("-" + suffix))
                {
                    return true;
                }

                parseNode = parseNode.GetParent();
            }

            return false;
        }

        protected override bool AutoDetectArgument(ParseNodeDrawable parseNode, ArgumentType argumentType)
        {
            ParseNode parent = parseNode.GetParent();
            switch (argumentType)
            {
                case ArgumentType.ARG0:
                    if (CheckAncestorsUntil(parent, "SBJ"))
                    {
                        return true;
                    }

                    break;
                case ArgumentType.ARG1:
                    if (CheckAncestorsUntil(parent, "OBJ"))
                    {
                        return true;
                    }

                    break;
                case ArgumentType.ARGMADV:
                    if (CheckAncestorsUntil(parent, "ADV"))
                    {
                        return true;
                    }

                    break;
                case ArgumentType.ARGMTMP:
                    if (CheckAncestorsUntil(parent, "TMP"))
                    {
                        return true;
                    }

                    break;
                case ArgumentType.ARGMMNR:
                    if (CheckAncestorsUntil(parent, "MNR"))
                    {
                        return true;
                    }

                    break;
                case ArgumentType.ARGMLOC:
                    if (CheckAncestorsUntil(parent, "LOC"))
                    {
                        return true;
                    }

                    break;
                case ArgumentType.ARGMDIR:
                    if (CheckAncestorsUntil(parent, "DIR"))
                    {
                        return true;
                    }

                    break;
                case ArgumentType.ARGMDIS:
                    if (CheckAncestors(parent, "CC"))
                    {
                        return true;
                    }

                    break;
                case ArgumentType.ARGMEXT:
                    if (CheckAncestorsUntil(parent, "EXT"))
                    {
                        return true;
                    }

                    break;
            }

            return false;
        }
    }
}