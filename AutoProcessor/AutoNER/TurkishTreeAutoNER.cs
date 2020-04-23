using System;
using System.Collections.Generic;
using System.Globalization;
using AnnotatedSentence;
using AnnotatedTree.Processor;
using AnnotatedTree.Processor.Condition;
using Dictionary.Dictionary;

namespace AnnotatedTree.AutoProcessor.AutoNER
{
    public class TurkishTreeAutoNER : TreeAutoNER
    {
        public TurkishTreeAutoNER(ViewLayerType secondLanguage) : base(ViewLayerType.TURKISH_WORD)
        {
        }

        protected override void AutoDetectPerson(ParseTreeDrawable parseTree)
        {
            NodeDrawableCollector nodeDrawableCollector = new NodeDrawableCollector((ParseNodeDrawable) parseTree.GetRoot(), new IsTurkishLeafNode());
            List<ParseNodeDrawable> leafList = nodeDrawableCollector.Collect();
            foreach (var parseNode in leafList){
                if (!parseNode.LayerExists(ViewLayerType.NER)){
                    String word = parseNode.GetLayerData(ViewLayerType.TURKISH_WORD).ToLower(new CultureInfo("tr"));
                    if (Word.IsHonorific(word) && parseNode.GetParent().GetData().GetName().Equals("NNP")){
                        parseNode.GetLayerInfo().SetLayerData(ViewLayerType.NER, "PERSON");
                    }
                    parseNode.CheckGazetteer(personGazetteer, word);
                }
            }
        }

        protected override void AutoDetectLocation(ParseTreeDrawable parseTree)
        {
            NodeDrawableCollector nodeDrawableCollector = new NodeDrawableCollector((ParseNodeDrawable) parseTree.GetRoot(), new IsTurkishLeafNode());
            List<ParseNodeDrawable> leafList = nodeDrawableCollector.Collect();
            foreach (var parseNode in leafList){
                if (!parseNode.LayerExists(ViewLayerType.NER)){
                    String word = parseNode.GetLayerData(ViewLayerType.TURKISH_WORD).ToLower(new CultureInfo("tr"));
                    parseNode.CheckGazetteer(locationGazetteer, word);
                }
            }
        }

        protected override void AutoDetectOrganization(ParseTreeDrawable parseTree)
        {
            NodeDrawableCollector nodeDrawableCollector = new NodeDrawableCollector((ParseNodeDrawable) parseTree.GetRoot(), new IsTurkishLeafNode());
            List<ParseNodeDrawable> leafList = nodeDrawableCollector.Collect();
            foreach (var parseNode in leafList){
                if (!parseNode.LayerExists(ViewLayerType.NER)){
                    String word = parseNode.GetLayerData(ViewLayerType.TURKISH_WORD).ToLower(new CultureInfo("tr"));
                    if (Word.IsOrganization(word)){
                        parseNode.GetLayerInfo().SetLayerData(ViewLayerType.NER, "ORGANIZATION");
                    }
                    parseNode.CheckGazetteer(organizationGazetteer, word);
                }
            }
        }

        protected override void AutoDetectMoney(ParseTreeDrawable parseTree)
        {
            NodeDrawableCollector nodeDrawableCollector = new NodeDrawableCollector((ParseNodeDrawable) parseTree.GetRoot(), new IsTurkishLeafNode());
            List<ParseNodeDrawable> leafList = nodeDrawableCollector.Collect();
            for (int i = 0; i < leafList.Count; i++) {
                ParseNodeDrawable parseNode = leafList[i];
                if (!parseNode.LayerExists(ViewLayerType.NER)){
                    String word = parseNode.GetLayerData(ViewLayerType.TURKISH_WORD).ToLower(new CultureInfo("tr"));
                    if (Word.IsMoney(word)) {
                        parseNode.GetLayerInfo().SetLayerData(ViewLayerType.NER, "MONEY");
                        int j = i - 1;
                        while (j >= 0){
                            ParseNodeDrawable previous = leafList[j];
                            if (previous.GetParent().GetData().GetName().Equals("CD")){
                                previous.GetLayerInfo().SetLayerData(ViewLayerType.NER, "MONEY");
                            } else {
                                break;
                            }
                            j--;
                        }
                    }
                }
            }
        }

        protected override void AutoDetectTime(ParseTreeDrawable parseTree)
        {
            NodeDrawableCollector nodeDrawableCollector = new NodeDrawableCollector((ParseNodeDrawable) parseTree.GetRoot(), new IsTurkishLeafNode());
            List<ParseNodeDrawable> leafList = nodeDrawableCollector.Collect();
            for (int i = 0; i < leafList.Count; i++){
                ParseNodeDrawable parseNode = leafList[i];
                if (!parseNode.LayerExists(ViewLayerType.NER)){
                    String word = parseNode.GetLayerData(ViewLayerType.TURKISH_WORD).ToLower(new CultureInfo("tr"));
                    if (Word.IsTime(word)){
                        parseNode.GetLayerInfo().SetLayerData(ViewLayerType.NER, "TIME");
                        if (i > 0){
                            ParseNodeDrawable previous = leafList[i - 1];
                            if (previous.GetParent().GetData().GetName().Equals("CD")){
                                previous.GetLayerInfo().SetLayerData(ViewLayerType.NER, "TIME");
                            }
                        }
                    }
                }
            }
        }
    }
}