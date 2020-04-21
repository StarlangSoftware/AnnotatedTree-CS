namespace AnnotatedTree.Processor.LeafConverter
{
    public interface LeafToStringConverter
    {
        string LeafConverter(ParseNodeDrawable leafNode);
    }
}