﻿using System.Collections.Generic;

namespace AntlrGrammarEditor
{
    public enum CaseInsensitiveType
    {
        None,
        lower,
        UPPER
    }

    public enum GrammarType
    {
        Combined,
        Separated,
        Lexer
    }

    public class Grammar
    {
        public const string AntlrDotExt = ".g4";
        public const string LexerPostfix = "Lexer";
        public const string ParserPostfix = "Parser";

        public string Name { get; set; }

        public string FileExtension { get; set; } = "txt";

        public GrammarType Type { get; set; }

        public CaseInsensitiveType CaseInsensitiveType { get; set; }

        public List<string> Files { get; set; } = new List<string>();

        public List<string> TextFiles { get; set; } = new List<string>();

        public string Directory { get; set; } = "";
    }
}