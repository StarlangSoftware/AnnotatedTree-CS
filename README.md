# AnnotatedTree-CS
A treebank is a corpus where the sentences in each language are syntactically (if necessary morphologically) annotated. In the  treebanks, the syntactic annotation usually follows constituent and/or dependency structure.

Treebanks annotated for the syntactic or semantic structures of the sentences are essential for developing state-of-the-art statistical natural language processing (NLP) systems including part-of-speech-taggers, syntactic parsers, and machine translation systems. There are two main groups of syntactic treebanks, namely treebanks annotated for constituency (phrase structure) and the ones that are annotated for dependency structure.

## Data Format

We extend the original format with the relevant information, given between curly braces. For example, the word 'problem' in a sentence in the standard Penn Treebank notation, may be represented in the data format provided below:

	(NN problem)

After all levels of processing are finished, the data structure stored for the same word has the following form in the system.

	(NN {turkish=sorunu} {english=problem} 
	{morphologicalAnalysis=sorun+NOUN+A3SG+PNON+ACC}
	{metaMorphemes=sorun+yH}
	{semantics=TUR10-0703650})

As is self-explanatory, 'turkish' tag shows the original Turkish word; 'morphologicalanalysis' tag shows the correct morphological parse of that word; 'semantics' tag shows the ID of the correct sense of that word; 'namedEntity' tag shows the named entity tag of that word; 'propbank' tag shows the semantic role of that word for the verb synset id (frame id in the frame file) which is also given in that tag.

## Tree Transfer

### Removal of \*NONE\* Leafs
The translated trees contain \*NONE\* leafs vacated by English constituents embedded in the morphemes of Turkish stems. After morphological annotation, the semantic aspects of those English constituents will be represented in the morpheme leaves. Hence, we remove all \*NONE\* leaves and all their ancestors until we reach an ancestor that has more than one child.

### Branching Multiword Leaves
A single English word may be translated to Turkish as a multiword expression. In such cases, we branch the multiword leaf into multiple leaves. We assign tags to the parents of the new leaf nodes according to their morphological analyses.

### Fixing Plural Nouns
Plural nouns tagged as NNS in an English tree are sometimes translated as singular to Turkish. For example, while English nouns next to a cardinality are attached the plurality suffix, Turkish nouns are not. In such cases, we check the morpholog- ical analyses of the nouns to detect whether they have the plurality suffix “-lAr” which is equivalent to the “-s” plurality suffix in English.

### Removal of Unnecessary Ancestors
After removal of \*NONE\* leafs, we generally end up with trees that have unnecessary ancestors. For each node, we remove all its ancestors until we reach an ancestor that has more than one child. However, if the leaf node does not contain any suffixes, we do not remove the immediate ancestor of that node.

### Branching Morphemes
The final and probably the most important operation is to branch the morphemes. We need to exploit the morphological analysis to compensate the information loss that occurred during the removal operations in the previous stages.

We treat the suffixes attached to nouns and verbs differently. While all suffixes attached to a noun will be siblings of that noun, each suffix attached to a verb will generate a new parent node and the next suffix will be a sibling of that new node.

In Turkish, suffixes may convert nouns to verbs or vice versa. In such cases, the corresponding node will be treated according to its final form and any additional suffixes will be attached following the above rule. In this stage, we introduced new tags for morphological annotation. Verb and noun roots are tagged as VERB and NOUN respectively. Suffixes attached to verbs determine the new tag that will be created. We simply concatenate their morpholog- ical annotation to VB-.

------------------------------------------------

Detailed Description
============
+ [TreeBankDrawable](#treebankdrawable)
+ [ParseTreeDrawable](#parsetreedrawable)
+ [LayerInfo](#layerinfo)
+ [Automatic Annotation](#automatic-annotation)

## TreeBankDrawable

İşaretlenmiş TreeBank'ı yüklemek için

	TreeBankDrawable(string folder, string pattern)
	a = new TreeBankDrawable("/Turkish-Phrase", ".train")

	TreeBankDrawable(string folder)
	a = new TreeBankDrawable("/Turkish-Phrase")

	TreeBankDrawable(string folder, string pattern, int from, int to)
	a = new TreeBankDrawable("/Turkish-Phrase", ".train", 1, 500)

Bir TreeBankDrawable'daki tüm ağaçlara erişmek için

	for (int i = 0; i < a.SentenceCount(); i++){
		ParseTreeDrawable parseTree = (ParseTreeDrawable) a.Get(i);
		....
	}

## ParseTreeDrawable

Kaydedilmiş bir ParseTreeDrawable'yi yüklemek için

	ParseTreeDrawable(FileInputStream file)
	
kullanılır. Genel olarak tek tek ParseTree yüklemek yerine yukarıda anlatıldığı gibi bir TreeBankDrawable yüklemek daha mantıklıdır.

Bir ParseTreeDrawable'nin düğüm sayısını

	int NodeCount()
	
yaprak sayısını 

	int LeafCount()
	
içinde yer alan kelime sayısını da

	int WordCount(boolean excludeStopWords)
	
metodları ile bulabiliriz.

## LayerInfo

İşaretlenmiş bir kelimenin bilgileri LayerInfo sınıfında tutulur. İşaretlenmiş kelimenin morfolojik
analizi

	MorphologicalParse GetMorphologicalParseAt(int index)

İşaretlenmiş kelimenin anlamı

	String GetSemanticAt(int index)

İşaretlenmiş kelimenin özne, dolaylı tümleç, vs. shallow parse tagı

	String GetShallowParseAt(int index)

İşaretlenmiş kelimenin argüman tagı

	Argument GetArgumentAt(int index)
	
Bir düğümdeki kelime sayısı

	int GetNumberOfWords()

## Automatic Annotation

Bir cümlenin argümanlarını otomatik olarak belirlemek için

	TurkishAutoArgument()

sınıfı kullanılır. 

	void AutoArgument(ParseTreeDrawable parseTree, Frameset frameset);

ile ağacın argümanları otomatik olarak işaretlenir.

Bir cümlede otomatik olarak morfolojik belirsizlik gidermek için

	TurkishTreeAutoDisambiguator(RootWordStatistics rootWordStatistics)
								  
sınıfı kullanılır. Örneğin,

	a = TurkishTreeAutoDisambiguator(new RootWordStatistics());
	a.AutoDisambiguate(parseTree);

ile ağacın morfolojik belirsizlik gidermesi otomatik olarak yapılır.

Bir cümlede adlandırılmış varlık tanıma yapmak için

	TurkishSentenceAutoNER()

sınıfı kullanılır. Örneğin,

	a = TurkishTreeAutoNER();
	a.AutoNER(parseTree);

ile ağacın varlık tanıma otomatik olarak yapılır.

Bir cümlede anlamsal işaretleme için

	TurkishTreeAutoSemantic()

sınıfı kullanılır. Örneğin,

	a = TurkishTreeAutoSemantic();
	a.AutoSemantic(parseTree);

ile ağacın anlamsal işaretleme otomatik olarak yapılır.

## Cite
If you use this resource on your research, please cite the following paper: 

```
@article{akcakaya,
  title={An all-words sense annotated {T}urkish corpus},
  author={S. Akçakaya and O. T. Y{\i}ld{\i}z},
  journal={2018 2nd International Conference on Natural Language and Speech Processing (ICNLSP)},
  year={2018},
  pages={1-6}
}

@inproceedings{arican,
  title={{E}nglish-{T}urkish Parallel Semantic Annotation of Penn-Treebank},
  author={ B. N. Ar{\i}can and {\"O}. Bakay and B. Avar and O. T. Y{\i}ld{\i}z and {\"O}. Ergelen},
  booktitle={Wordnet Conference},
  pages={298},
  year={2019}
}