﻿using Antlr4.Runtime;
using System;
using System.Diagnostics;
using System.IO;
/*$PackageName$*/
#if CSharpOptimized
using Antlr4.Runtime.Misc;
#endif

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string fileName = "../../../../Text";
            bool formatOutput = true;
            bool symbolicNames = true;

            if (args.Length > 0)
            {
                fileName = args[0];
            }

            var code = File.ReadAllText(fileName);
            var codeStream = new AntlrInputStream(code);
            var lexer = new __TemplateGrammarName__Lexer(codeStream);
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new LexerErrorListener());

            var stopwatch = Stopwatch.StartNew();
            var tokens = lexer.GetAllTokens();
            stopwatch.Stop();

            var tokensJsonSerializer = new TokensJsonSerializer(lexer)
            {
                Format = formatOutput,
                SymbolicNames = symbolicNames,
                LineColumn = false
            };
            File.WriteAllText("Tokens.json", tokensJsonSerializer.ToJson(tokens));
            Console.WriteLine("Tokens {0}", Path.GetFullPath("Tokens.json"));

/*$ParserPart*/
            string rootRule = null;
            bool notParse = false;

            if (args.Length > 1)
            {
                rootRule = args[1];
                if (args.Length > 2)
                {
                    bool.TryParse(args[2], out notParse);
                }
            }

            if (!notParse)
            {
                var tokensSource = new ListTokenSource(tokens);
                var tokensStream = new CommonTokenStream(tokensSource);
                var parser = new __TemplateGrammarName__Parser(tokensStream);
                parser.RemoveErrorListeners();
                parser.AddErrorListener(new ParserErrorListener());

                stopwatch.Restart();
                string ruleName = rootRule == null ? parser.RuleNames[0] : rootRule;
                var rootMethod = parser.GetType().GetMethod(ruleName);
                var parseTree = (ParserRuleContext)rootMethod.Invoke(parser, new object[0]);
                stopwatch.Stop();

                var parseTreeJsonSerializer = new ParseTreeJsonSerializer(lexer, parser)
                {
                    Format = formatOutput,
                    SymbolicNames = symbolicNames
                };
                File.WriteAllText("ParseTree.json", parseTreeJsonSerializer.ToJson(parseTree));
                Console.WriteLine("ParserTime {0}", stopwatch.Elapsed);
                Console.WriteLine("Tree {0}", Path.GetFullPath("ParseTree.json"));
            }
/*ParserPart$*/
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.ToString().Replace("\r", "").Replace("\n", ""));
        }
    }

    class LexerErrorListener : IAntlrErrorListener<int>
    {
#if CSharpOptimized
        public void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] int offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
#else
        public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
#endif
        {
            Console.Error.WriteLine($"line {line}:{charPositionInLine} {msg}");
        }
    }

    class ParserErrorListener : IAntlrErrorListener<IToken>
    {
#if CSharpOptimized
        public void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] IToken offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
#else
        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
#endif
        {
            Console.Error.WriteLine($"line {line}:{charPositionInLine} {msg}");
        }
    }
}