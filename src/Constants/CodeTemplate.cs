using System;
using System.IO;

namespace JustinWritesCode.CodeGeneration;

public record struct CodeTemplate
{
    public string StringTemplate { get; init; }
    public string ManifestResourceName { get; init; }
    public Scriban.Template Template { get; init; }

    public CodeTemplate(string codeTemplate)
    {
        this.StringTemplate = codeTemplate;
        this.ManifestResourceName = null;
        this.Template = Scriban.Template.Parse(StringTemplate);
    }

    public static CodeTemplate FromResource(string manifestResourceName)
    {
        try
        {
            var codeTemplate = new CodeTemplate(new StreamReader(typeof(CodeTemplate).Assembly
                .GetManifestResourceStream(manifestResourceName))
                .ReadToEnd());
            return codeTemplate;
        }
        catch(Exception e)
        {
            throw new Exception($"Could not load resource {manifestResourceName}", e);
        }
    }
}

public record struct CodeTemplate<T>
{
    public string StringTemplate { get; init; }
    public string ManifestResourceName { get; init; }
    public Scriban.Template Template { get; init; }

    public static CodeTemplate FromResource(string manifestResourceName)
    {
        var codeTemplate = new CodeTemplate(new StreamReader(typeof(T).Assembly
               .GetManifestResourceStream(manifestResourceName))
               .ReadToEnd());
        return codeTemplate;
    }
}
