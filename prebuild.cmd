cd AntlrGrammarEditor
java -jar "Generators/antlr-4.6.4-csharpsharwell.jar" "ANTLRv4Lexer.g4" -o "Generated" -Dlanguage=CSharp_v4_5 -package AntlrGrammarEditor
java -jar "Generators/antlr-4.6.4-csharpsharwell.jar" "ANTLRv4Parser.g4" -o "Generated" -Dlanguage=CSharp_v4_5 -package AntlrGrammarEditor
cd ..