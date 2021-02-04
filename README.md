For Developers
============
You can also see [Java](https://github.com/starlangsoftware/AnnotatedTree), [Python](https://github.com/starlangsoftware/AnnotatedTree-Py), [Cython](https://github.com/starlangsoftware/AnnotatedTree-Cy), or [C++](https://github.com/starlangsoftware/AnnotatedTree-CPP) repository.

Detailed Description
============

+ [TreeBankDrawable](#treebankdrawable)
+ [ParseTreeDrawable](#parsetreedrawable)
+ [LayerInfo](#layerinfo)
+ [Automatic Annotation](#automatic-annotation)

## TreeBankDrawable

To load an annotated TreeBank:

	TreeBankDrawable(string folder, string pattern)
	a = new TreeBankDrawable("/Turkish-Phrase", ".train")

	TreeBankDrawable(string folder)
	a = new TreeBankDrawable("/Turkish-Phrase")

	TreeBankDrawable(string folder, string pattern, int from, int to)
	a = new TreeBankDrawable("/Turkish-Phrase", ".train", 1, 500)

To access all the trees in a TreeBankDrawable:

	for (int i = 0; i < a.SentenceCount(); i++){
		ParseTreeDrawable parseTree = (ParseTreeDrawable) a.Get(i);
		....
	}

## ParseTreeDrawable

To load a saved ParseTreeDrawable:

	ParseTreeDrawable(FileInputStream file)
	
is used. Usually it is more useful to load TreeBankDrawable as explained above than to load ParseTree one by one.

To find the node number of a ParseTreeDrawable:

	int NodeCount()
	
the leaf number of a ParseTreeDrawable:

	int LeafCount()
	
the word count in a ParseTreeDrawable:

	int WordCount(boolean excludeStopWords)
	
above methods can be used.

## LayerInfo

Information of an annotated word is kept in LayerInfo class. To access the morphological analysis
of the annotated word:

	MorphologicalParse GetMorphologicalParseAt(int index)

meaning of an annotated word:

	String GetSemanticAt(int index)

the shallow parse tag (e.g., subject, indirect object etc.) of annotated word: 

	String GetShallowParseAt(int index)

the argument tag of the annotated word:

	Argument GetArgumentAt(int index)
	
the word count in a node:

	int GetNumberOfWords()

## Automatic Annotation

To assign the arguments of a sentence automatically:

	TurkishAutoArgument()

above class is used.

	void AutoArgument(ParseTreeDrawable parseTree, Frameset frameset);

With above line, the arguments of the tree are annotated automatically.

To automatically disambiguate a sentence's morphology:

	TurkishTreeAutoDisambiguator(RootWordStatistics rootWordStatistics)
								  
above class is used. For example,

	a = TurkishTreeAutoDisambiguator(new RootWordStatistics());
	a.AutoDisambiguate(parseTree);

with the above code, automatic morphological disambiguation of the tree can be made.

To apply named entity recognition to a sentence:

	TurkishSentenceAutoNER()

above class is used. For example,

	a = TurkishTreeAutoNER();
	a.AutoNER(parseTree);

with the above code, automatic named entity recognition of a tree can be made.

To make semantic annotation in a sentence:

	TurkishTreeAutoSemantic()

above class can be used. For example,

	a = TurkishTreeAutoSemantic();
	a.AutoSemantic(parseTree);

with above code, automatic semantic annotation of the tree can be made.
