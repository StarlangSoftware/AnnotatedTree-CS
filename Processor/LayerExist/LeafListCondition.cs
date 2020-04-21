using System.Collections.Generic;

namespace AnnotatedTree.Processor.LayerExist
{
    public interface LeafListCondition
    {
        bool Satisfies(List<ParseNodeDrawable> leafList);
    }
}