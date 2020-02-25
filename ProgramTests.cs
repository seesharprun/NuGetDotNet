using Xunit;
using FluentAssertions;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using Microsoft.CodeAnalysis;
using System;

namespace Hello.Tests
{
    public class ProgramTests
    {
        private async Task<CompilationUnitSyntax> GetRootAsync()
        {
            string code = await File.ReadAllTextAsync("../../../Program.cs");
            var tree = CSharpSyntaxTree.ParseText(code);
            return await tree.GetRootAsync() as CompilationUnitSyntax;
        }

        [Fact]
        private async void UsingBlockMustUseAlias()
        {
            var root = await GetRootAsync();

            var usingBlocks = root.DescendantNodes()
                .OfType<UsingDirectiveSyntax>()
                .Select(u => u.Alias)
                .Where(a => !(a is null));

            usingBlocks.Should()
                .NotBeEmpty("you must add a using block that includes an alias @usingAlias");
        }

        [Fact]
        private async void UsingBlockMustUseAliasNamedConsole()
        {
            var root = await GetRootAsync();

            string expected = "Console";

            var usingBlocks = root.DescendantNodes()
                .OfType<UsingDirectiveSyntax>()
                .Select(u => u.Alias)
                .Where(a => !(a is null))
                .Select(a => a.Name)
                .OfType<IdentifierNameSyntax>()
                .Select(i => i.Identifier.ValueText);

            usingBlocks.Should()
                .Contain(expected, $"your using block's alias must be named \"{expected}\" @usingConsole");
        }

        [Fact]
        private async void UsingBlockMustReferenceColorfulConsole()
        {
            var root = await GetRootAsync();

            string expected = "Colorful.Console";

	        var usingBlocks = root.DescendantNodes()
				.OfType<UsingDirectiveSyntax>()
				.Where(u => !(u.Alias is null))
				.Select(u => u.Name)
				.OfType<QualifiedNameSyntax>()
				.Select(q => (left: q.Left as IdentifierNameSyntax, middle: (SyntaxToken)q.DotToken, right: q.Right as IdentifierNameSyntax))
				.Select(b => $"{b.left.Identifier.ValueText}{b.middle.ValueText}{b.right.Identifier.ValueText}");

            usingBlocks.Should()
                .Contain(expected, $"your using block should reference the \"{expected}\" library @usingColorful");
        }

        [Fact]
        private async void CodeMustUseConsoleWriteAscii()
        {            
            var root = await GetRootAsync();

            string identifier = "Main";
            string expected = "Console.WriteAscii";

            var lines = root.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Where(m => m.Identifier.ValueText == identifier)
                .Select(m => m.Body)
                .OfType<BlockSyntax>()
                .SelectMany(b => b.Statements)
                .SelectMany(s => s.DescendantNodes().OfType<InvocationExpressionSyntax>())
                .Select(i => i.Expression)
                .OfType<ExpressionSyntax>()
                .Select(e => String.Join('.', e.DescendantNodes().OfType<IdentifierNameSyntax>().Select(i => i.Identifier.ValueText)));

            lines.Should()
                .Contain(expected, $"your code should call the \"{expected}\" method @codeAscii");
        }

        [Fact]
        private async void CodeMustGreetPluralsight()
        {            
            var root = await GetRootAsync();

            string identifier = "Main";
            string expected = "Hello, Pluralsight!";

            var args = root.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Where(m => m.Identifier.ValueText == identifier)
                .Select(m => m.Body)
                .OfType<BlockSyntax>()
                .SelectMany(b => b.Statements)
                .SelectMany(s => s.DescendantNodes().OfType<InvocationExpressionSyntax>())
                .Select(i => i.ArgumentList.Arguments.OfType<ArgumentSyntax>())
                .Select(l => l.Select(i => i.Expression).OfType<LiteralExpressionSyntax>().Select(e => e.Token.ValueText))
                .Where(e => e.Count() == 1)
                .SelectMany(e => e);

            args.Should()
                .Contain(expected, $"your code should greet Pluralsight using the string \"{expected}\" @codePluralsight");
        }
    }
}
