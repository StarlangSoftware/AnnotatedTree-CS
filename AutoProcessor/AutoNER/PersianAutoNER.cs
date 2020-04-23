using AnnotatedSentence;

namespace AnnotatedTree.AutoProcessor.AutoNER
{
    public class PersianAutoNER : TreeAutoNER
    {
        public PersianAutoNER(ViewLayerType secondLanguage) : base(ViewLayerType.PERSIAN_WORD)
        {
        }

        protected override void AutoDetectPerson(ParseTreeDrawable parseTree)
        {
        }

        protected override void AutoDetectLocation(ParseTreeDrawable parseTree)
        {
        }

        protected override void AutoDetectOrganization(ParseTreeDrawable parseTree)
        {
        }

        protected override void AutoDetectMoney(ParseTreeDrawable parseTree)
        {
        }

        protected override void AutoDetectTime(ParseTreeDrawable parseTree)
        {
        }
    }
}