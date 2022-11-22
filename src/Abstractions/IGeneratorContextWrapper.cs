/* 
 * IGeneratorContextWrapper.cs
 * 
 *   Created: 2022-11-02-12:39:17
 *   Modified: 2022-11-12-12:19:58
 * 
 *   Author: Justin Chase <justin@justinwritescode.com>
 *   
 *   Copyright © 2022 Justin Chase, All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */ 

namespace JustinWritesCode.CodeGeneration.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
public interface IGeneratorContextWrapper
{
    void AddSource(string fileName, string source);
    void AddSource(string fileName, SourceText source);
    void ReportDiagnostic(Diagnostic diagnostic);
    void ReportDiagnostic(DiagnosticDescriptor descriptor, Location location, params object[] args);
    void ReportDiagnostic(DiagnosticDescriptor descriptor, Location location, IReadOnlyDictionary<string, string> properties, params object[] args);
    void ReportDiagnostic(DiagnosticDescriptor descriptor, Location location, IReadOnlyDictionary<string, string> properties, IReadOnlyCollection<Location> additionalLocations, params object[] args);
    void ReportDiagnostic(DiagnosticDescriptor descriptor, Location location, IReadOnlyDictionary<string, string> properties, IReadOnlyCollection<Location> additionalLocations, IReadOnlyCollection<Diagnostic> relatedDiagnostics, params object[] args);
    void ReportDiagnostic(DiagnosticDescriptor descriptor, Location location, IReadOnlyDictionary<string, string> properties, IReadOnlyCollection<Location> additionalLocations, IReadOnlyCollection<Diagnostic> relatedDiagnostics, bool isSuppressed, params object[] args);
    void ReportDiagnostic(DiagnosticDescriptor descriptor, Location location, IReadOnlyDictionary<string, string> properties, IReadOnlyCollection<Location> additionalLocations, IReadOnlyCollection<Diagnostic> relatedDiagnostics, bool isSuppressed, int warningLevel, params object[] args);
}
