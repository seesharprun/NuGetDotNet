using Xunit;
using FluentAssertions;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Xml.Linq;

namespace Hello.Tests
{
    public class NuGetTests
    {
        private async Task<XDocument> GetDocumentAsync()
        {
            string markup = await File.ReadAllTextAsync("../../../Hello.csproj");
            return XDocument.Parse(markup);
        }

        [Fact]
        private async void UsingBlockMustUseAlias()
        {
            var doc = await GetDocumentAsync();

            string expected = "Colorful.Console";

            var imports = doc.Descendants("PackageReference")
                .Select(r => r.Attribute("Include"))
                .Where(a => !(a is null))
                .Select(a => a.Value);

            imports.Should()
                .Contain(expected, $"You must import the \"{expected}\" package from NuGet @nuget");
        }

    }
}