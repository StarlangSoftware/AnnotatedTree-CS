using AnnotatedSentence;

namespace AnnotatedTree.Processor.Condition
{
    public class IsNoneReplaceable : IsLeafNode
    {
        public new bool Satisfies(ParseNodeDrawable parseNode)
        {
            if (base.Satisfies(parseNode))
            {
                var data = parseNode.GetLayerData(ViewLayerType.ENGLISH_WORD);
                var parentData = parseNode.GetParent().GetData().GetName();
                if (parentData.Equals("DT"))
                {
                    return data.Equals("the");
                }

                if (parentData.Equals("IN"))
                {
                    return data.Equals("in") || data.Equals("than") || data.Equals("from") || data.Equals("on") ||
                           data.Equals("with") || data.Equals("of") || data.Equals("at") || data.Equals("if") ||
                           data.Equals("by");
                }

                if (parentData.Equals("TO"))
                {
                    return data.Equals("to");
                }

                if (parentData.Equals("VBZ"))
                {
                    return data.Equals("has") || data.Equals("does") || data.Equals("is") || data.Equals("'s");
                }

                if (parentData.Equals("MD"))
                {
                    return data.Equals("will") || data.Equals("'d") || data.Equals("'ll") || data.Equals("ca") ||
                           data.Equals("can") || data.Equals("could") || data.Equals("would") ||
                           data.Equals("should") || data.Equals("wo") || data.Equals("may") || data.Equals("might");
                }

                if (parentData.Equals("VBP"))
                {
                    return data.Equals("'re") || data.Equals("is") || data.Equals("are") || data.Equals("am") ||
                           data.Equals("'m") || data.Equals("do") || data.Equals("have") || data.Equals("has") ||
                           data.Equals("'ve");
                }

                if (parentData.Equals("VBD"))
                {
                    return data.Equals("had") || data.Equals("did") || data.Equals("were") || data.Equals("was");
                }

                if (parentData.Equals("VBN"))
                {
                    return data.Equals("been");
                }

                if (parentData.Equals("VB"))
                {
                    return data.Equals("have") || data.Equals("be");
                }

                if (parentData.Equals("RB"))
                {
                    return data.Equals("n't") || data.Equals("not");
                }

                if (parentData.Equals("POS"))
                {
                    return data.Equals("'s") || data.Equals("'");
                }

                if (parentData.Equals("WP"))
                {
                    return data.Equals("who") || data.Equals("where") || data.Equals("which") || data.Equals("what") ||
                           data.Equals("why");
                }
            }

            return false;
        }
    }
}