using System.Security.AccessControl;
/*
 * GeneratorExecutionContextWrapper.cs
 *
 *   Created: 2022-11-02-12:35:41
 *   Modified: 2022-11-12-12:19:46
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

public class GeneratorExecutionContextWrapper : IGeneratorContextWrapper
{
    private readonly GeneratorExecutionContext _context;

    public GeneratorExecutionContextWrapper(GeneratorExecutionContext context)
    {
        _context = context;
    }

    public void ReportDiagnostic(Diagnostic diagnostic)
    {
        _context.ReportDiagnostic(diagnostic);
    }

    public void ReportDiagnostic(DiagnosticDescriptor descriptor, Location location, params object[] args)
    {
        _context.ReportDiagnostic(Diagnostic.Create(descriptor, location, args));
    }

    public void ReportDiagnostic(DiagnosticDescriptor descriptor, Location location, IReadOnlyDictionary<string, string> properties, params object[] args)
    {
        _context.ReportDiagnostic(Diagnostic.Create(descriptor, location, properties, args));
    }

    public void ReportDiagnostic(DiagnosticDescriptor descriptor, Location location, IReadOnlyDictionary<string, string> properties, IReadOnlyCollection<Location> additionalLocations, params object[] args)
    {
        _context.ReportDiagnostic(Diagnostic.Create(descriptor, location, properties, additionalLocations, args));
    }

    public void ReportDiagnostic(DiagnosticDescriptor descriptor, Location location, IReadOnlyDictionary<string, string> properties, IReadOnlyCollection<Location> additionalLocations, IReadOnlyCollection<Diagnostic> relatedDiagnostics, params object[] args)
    {
        _context.ReportDiagnostic(Diagnostic.Create(descriptor, location, properties, additionalLocations, relatedDiagnostics, args));
    }

    public void ReportDiagnostic(DiagnosticDescriptor descriptor, Location location, IReadOnlyDictionary<string, string> properties, IReadOnlyCollection<Location> additionalLocations, IReadOnlyCollection<Diagnostic> relatedDiagnostics, bool isSuppressed, params object[] args)
    {
        _context.ReportDiagnostic(Diagnostic.Create(descriptor, location, properties, additionalLocations, relatedDiagnostics, isSuppressed, args));
    }

    public void ReportDiagnostic(DiagnosticDescriptor descriptor, Location location, IReadOnlyDictionary<string, string> properties, IReadOnlyCollection<Location> additionalLocations, IReadOnlyCollection<Diagnostic> relatedDiagnostics, bool isSuppressed, int warningLevel, params object[] args)
    {
        _context.ReportDiagnostic(Diagnostic.Create(descriptor, location, properties, additionalLocations, relatedDiagnostics, isSuppressed, warningLevel, args));
    }
    public void AddSource(string fileName, string source)
    {
        _context.AddSource(fileName, source);
    }
    public void AddSource(string fileName, SourceText source)
    {
        _context.AddSource(fileName, source);
    }
}
