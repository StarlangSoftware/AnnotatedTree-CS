using AnnotatedSentence;

namespace AnnotatedTree.Processor.Condition
{
    public class IsNoneNode : IsLeafNode
    {
        private readonly ViewLayerType _secondLanguage;

        public IsNoneNode(ViewLayerType secondLanguage){
            this._secondLanguage = secondLanguage;
        }

        public override bool Satisfies(ParseNodeDrawable parseNode) {
            if (base.Satisfies(parseNode)){
                var data = parseNode.GetLayerData(_secondLanguage);
                return data != null && data.Equals("*NONE*");
            }
            return false;
        }
        
    }
}