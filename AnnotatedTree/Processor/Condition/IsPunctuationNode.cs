using AnnotatedSentence;
using Dictionary.Dictionary;

namespace AnnotatedTree.Processor.Condition
{
    public class IsPunctuationNode : IsLeafNode
    {
        public override bool Satisfies(ParseNodeDrawable parseNode) {
            if (base.Satisfies(parseNode)){
                var data = parseNode.GetLayerData(ViewLayerType.ENGLISH_WORD);
                return Word.IsPunctuation(data) && !data.Equals("$");
            }
            return false;
        }
        
    }
}