using System.IO;

namespace JustinWritesCode.CodeGeneration;

public record struct CodeTemplate
{
    public string StringTemplate { get; init; }
    public string ManifestResourceName { get; init; }
    public Scriban.Template Template { get; init; }

    public CodeTemplate(string manifestResourceName)
    {
        this.ManifestResourceName = manifestResourceName;
        this.StringTemplate = new StreamReader(typeof(Constants).Assembly
               .GetManifestResourceStream(ManifestResourceName))
               .ReadToEnd();
        this.Template = Scriban.Template.Parse(StringTemplate);
    }
}
