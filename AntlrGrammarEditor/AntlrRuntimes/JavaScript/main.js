var fs = require("fs");
var antlr4 = require('antlr4/index');
var __TemplateGrammarName__Lexer = require('./__TemplateGrammarName__Lexer').__TemplateGrammarName__Lexer;
/*$ParserInclude*/var __TemplateGrammarName__Parser = require('./__TemplateGrammarName__Parser').__TemplateGrammarName__Parser;/*ParserInclude$*/
/*AntlrCaseInsensitive*/

var fileName = "../../Text"

if (process.argv.length >= 2) {
    fileName = process.argv[2];
}

var input = fs.readFileSync(fileName).toString();
var chars = new antlr4.InputStream(input);
var lexer = new __TemplateGrammarName__Lexer(chars);
var tokensStream = new antlr4.CommonTokenStream(lexer);

/*$ParserPart*/
var rootRule;
if (process.argv.length >= 3) {
    rootRule = process.argv[3];
}
var parser = new __TemplateGrammarName__Parser(tokensStream);
var ruleName = rootRule === undefined ? parser.ruleNames[0] : rootRule;
var ast = parser[ruleName]();
console.log("Tree " + ast.toStringTree(null, parser));
/*ParserPart$*/