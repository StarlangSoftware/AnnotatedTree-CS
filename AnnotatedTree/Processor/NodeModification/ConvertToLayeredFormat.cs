using AnnotatedSentence;

namespace AnnotatedTree.Processor.NodeModification
{
    public class ConvertToLayeredFormat : NodeModifier
    {
        public void Modifier(ParseNodeDrawable parseNode)
        {
            if (parseNode.IsLeaf()){
                string name = parseNode.GetData().GetName();
                parseNode.ClearLayers();
                parseNode.GetLayerInfo().SetLayerData(ViewLayerType.ENGLISH_WORD, name);
                parseNode.ClearData();
            }
        }
    }
}