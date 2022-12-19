/*
 * Constants.cstemplate
 *
 *   Created: 2022-10-31-07:30:55
 *   Modified: 2022-12-08-01:01:51
 *
 *   Author: Justin Chase <justin@justinwritescode.com>
 *
 *   Copyright © 2022 Justin Chase, All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

#pragma warning disable

namespace JustinWritesCode.CodeGeneration   ;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static partial class Constants
{
    public const string BeginCodeHeaderSentinel = "// BEGIN CODE HEADER";
    public const string AuthorsPlaceholder = "$AUTHORS";
    public const string AuthorNamePlaceholder = "$AUTHOR";
    public const string AuthorEmailPlaceholder = "$AUTHOR_EMAIL";
    public const string AuthorUrlPlaceholder = "$AUTHOR_URL";
    public const string LicensePlaceholder = "$LICENSE";
    public const string YearPlaceholder = "$YEAR";
    public const string TimestampPlaceholder = "$TIMESTAMP";
    public const string FilenamePlaceholder = "$FILENAME";
    public const string AttributeClassNamePlaceholder = "$ATTRIBUTE_CLASS_NAME";
    public const string AttributeTargetsPlaceholder = "$ATTRIBUTE_TARGETS";
    public const string AttributeParamsPlaceholder = "$PARAMS";
    public const string AttributePropertiesDeclarationsPlaceholder = "$PROPS";
    public const string AttributePropertyAssignmentsPlaceholder = "$ASSIGNMENTS";
    public const string PackageProjectUrl = nameof(PackageProjectUrl);
    public const string AuthorsPropertyName = "Authors";
    public const string PackageLicenseExpression = nameof(PackageLicenseExpression);
    public const string AuthorsBuildProperty = $"build_property.{AuthorsPropertyName}";
    public const string LicenseBuildProperty = $"build_property.{PackageLicenseExpression}";
    public const string ProjectUrlBuildProperty = $"build_property.{PackageProjectUrl}";

    public static readonly string CodeHeaderTemplate =
        new StreamReader(typeof(Constants).Assembly.GetManifestResourceStream("CodeHeader.cstemplate")!)
        .ReadToEnd()
        .TrimToSentinel();

    public static readonly string MultipleAuthorsCodeHeaderCommentTemplate =
        new StreamReader(typeof(Constants).Assembly.GetManifestResourceStream("MultipleAuthorsCodeHeaderComment.cstemplate")!)
        .ReadToEnd();

    public static readonly string MultipleAuthorsCodeHeaderCommentTemplate_PerAuthor =
        new StreamReader(typeof(Constants).Assembly.GetManifestResourceStream("MultipleAuthorsCodeHeaderCommentTemplate_PerAuthor.cstemplate")!)
        .ReadToEnd();

    public static readonly string SingleAuthorCodeHeaderTemplate =
        new StreamReader(typeof(Constants).Assembly.GetManifestResourceStream("SingleAuthorCodeHeaderTemplate.cstemplate")!)
        .ReadToEnd();

    public static readonly CodeTemplate AttributeDeclarationTemplate = new CodeTemplate("AttributeDeclaration.cstemplate");
    public static readonly CodeTemplate MinimalCodeHeaderTemplate = new CodeTemplate("MinimalCodeHeader.cstemplate");
        // new StreamReader(typeof(Constants).Assembly.GetManifestResourceStream("MinimalCodeHeader.cstemplate")!)
        // .ReadToEnd();

    public static string GenerateCodeHeader(string? filename = null, IEnumerable<(string Name, string Email)>? authors = null, string? authorUrl = null, string? licenseExpression = null)
    {
        var codeHeader = CodeHeaderTemplate;
        var authorsCodeHeaderSnippet =
            authors is null || !authors.Any() ?
            "" :
            authors.Count() == 1 ?
            SingleAuthorCodeHeaderTemplate.Replace(AuthorNamePlaceholder, authors.First().Name).Replace(AuthorEmailPlaceholder, $"<{authors.First().Email}>") :
            $"{MultipleAuthorsCodeHeaderCommentTemplate}{Environment.NewLine}{string.Join(Environment.NewLine,
                    authors.Select(author =>
                        MultipleAuthorsCodeHeaderCommentTemplate_PerAuthor.Replace(AuthorNamePlaceholder, author.Name).Replace(AuthorEmailPlaceholder, $"<{author.Email}>")))}";

        codeHeader = codeHeader.Replace(AuthorsPlaceholder, authorsCodeHeaderSnippet);
        codeHeader = codeHeader.Replace(YearPlaceholder, DateTime.Now.Year.ToString());
        codeHeader = codeHeader.Replace(TimestampPlaceholder, DateTime.UtcNow.ToString("yyyy-MM-dd-HH:mm:ss"));
        codeHeader = codeHeader.Replace(FilenamePlaceholder, filename);
        codeHeader = codeHeader.Replace(AuthorUrlPlaceholder, authorUrl);
        codeHeader = codeHeader.Replace(LicensePlaceholder, licenseExpression);
        return codeHeader;
    }
    public static string GenerateAttributeDeclaration(string attributeName, AttributeTargets attributeTargets = AttributeTargets.All, Type baseType = default, params AttributeProperty[] properties)
    {
        baseType ??= typeof(Attribute);
        var result =
        MinimalCodeHeaderTemplate.Template.Render(new { filename = $"{attributeName}.cs", timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm") }) +
        Environment.NewLine +
        AttributeDeclarationTemplate.Template.Render(new AttributeInfo
        (
            attributeName,
            baseType,
            attributeTargets.ToString(),
            properties
        ));

        return result;
    }

    public static string TrimToSentinel(this string codeHeader)
    {
        var sentinelIndex = codeHeader.IndexOf(BeginCodeHeaderSentinel);
        if (sentinelIndex == -1)
        {
            return codeHeader;
        }
        return codeHeader.Substring(sentinelIndex + BeginCodeHeaderSentinel.Length).Trim();
    }

    public static IEnumerable<(string Name, string? Email)> ParseAuthors(string authors)
    {
        var authorsList = authors.Split(';');
        foreach (var author in authorsList)
        {
            var authorParts = author.Split('<');
            var authorName = authorParts.First().Trim();
            var authorEmail = authorParts.Skip(1).FirstOrDefault()?.Trim().TrimEnd('>');
            yield return (authorName, authorEmail);
        }
    }
}
