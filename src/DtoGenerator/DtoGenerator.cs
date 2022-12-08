/*
 * DtoGenerator.cs
 *
 *   Created: 2022-12-06-07:42:23
 *   Modified: 2022-12-06-07:42:24
 *
 *   Author: Justin Chase <justin@justinwritescode.com>
 *
 *   Copyright © 2022 Justin Chase, All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */
#pragma warning disable
namespace JustinWritesCode.CodeGeneration.DtoGenerator;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

[Generator]
public class DtoGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context) 
    {
        var typeDeclarations = context.SyntaxProvider.ForAttributeWithMetadataName(Constants.GenerateInsertDtoAttributeMetadataName, (syntaxNode, _) => syntaxNode is TypeDeclarationSyntax, (typeDeclaration, _) => (typeDeclaration.Attributes, typeDeclaration.TargetNode, typeDeclaration.TargetSymbol, typeDeclaration.SemanticModel)).Collect();
        var syntax = context.CompilationProvider.Combine(typeDeclarations);
        context.RegisterSourceOutput(syntax, (context, typeDeclarations) => GenerateInsertDto(context, typeDeclarations));
    }

    public virtual void GenerateInsertDto(SourceProductionContext context, (Compilation compilation, ImmutableArray<(AttributeData AttributeData, SyntaxNode SyntaxNode, ISymbol Symbol, SemanticModel SemanticModel)> typeDeclarations) values)
    {
        var compilation = values.compilation;
        var typeDeclarations = values.typeDeclarations;
        var syntaxTree = compilation.SyntaxTrees.First();
        var model = compilation.GetSemanticModel(syntaxTree);
        var root = syntaxTree.GetRoot();
        var namespaceDeclaration = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().First();
        var namespaceName = namespaceDeclaration.Name.ToString();
        var insertDtoNamespace = namespaceName + ".Dtos";
        var insertDtoNamespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(insertDtoNamespace));
        var insertDtoClassDeclarations = new List<ClassDeclarationSyntax>();
        foreach (var typeDeclaration in typeDeclarations)
        {
            var typeSymbol = model.GetDeclaredSymbol(typeDeclaration.SyntaxNode) as ITypeSymbol;
            // var typeSymbol = typeDeclaration.Symbol;
            var insertDtoClassDeclaration = SyntaxFactory.ClassDeclaration((typeDeclaration.SyntaxNode as TypeDeclarationSyntax).Identifier.Text + "InsertDto");
            var insertDtoClassProperties = new List<PropertyDeclarationSyntax>();
            foreach (var property in typeSymbol.GetMembers().OfType<IPropertySymbol>())
            {
                var propertyType = property.Type;
                var propertyTypeSyntax = SyntaxFactory.ParseTypeName(propertyType.ToDisplayString());
                var propertyDeclaration = SyntaxFactory.PropertyDeclaration(propertyTypeSyntax, property.Name);
                insertDtoClassProperties.Add(propertyDeclaration);
            }
            insertDtoClassDeclaration = insertDtoClassDeclaration.AddMembers(insertDtoClassProperties.ToArray());
            insertDtoClassDeclarations.Add(insertDtoClassDeclaration);
        }
        insertDtoNamespaceDeclaration = insertDtoNamespaceDeclaration.AddMembers(insertDtoClassDeclarations.ToArray());
        var newRoot = root.AddMembers(insertDtoNamespaceDeclaration);
        context.AddSource("InsertDto.cs", newRoot.ToFullString());
    }
    {

    }
}
