// 
// StringExtensions.cs
// 
//   Created: 2022-10-30-10:35:49
//   Modified: 2022-10-30-10:40:41
// 
//   Author: Justin Chase <justin@justinwritescode.com>
//   
//   Copyright © 2022 Justin Chase, All Rights Reserved
//      License: MIT (https://opensource.org/licenses/MIT)
// 

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace JustinWritesCode.CodeGeneration.Extensions;

public static class StringExtensions
{
    private static readonly Regex ExtractValueBetween = new("(?<=<).*(?=>)", RegexOptions.Compiled);

    public static bool TryGetGenericTypeArguments(this string input, /*[NotNullWhen(true)]*/ out string? genericTypeArgumentValue)
    {
        genericTypeArgumentValue = null;

        var match = ExtractValueBetween.Match(input);

        if (match.Success)
        {
            genericTypeArgumentValue = match.Value;
            return true;
        }

        return false;
    }
}
