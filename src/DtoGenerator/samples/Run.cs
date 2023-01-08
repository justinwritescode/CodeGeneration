/*
 * Run.cs
 *
 *   Created: 2023-01-06-01:43:40
 *   Modified: 2023-01-06-01:43:40
 *
 *   Author: Justin Chase <justin@justinwritescode.com>
 *
 *   Copyright © 2022-2023 Justin Chase, All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */
#if EXE
using JustinWritesCode.CodeGeneration;
using Microsoft.CodeAnalysis;

var incGenerator = new JustinWritesCode.CodeGeneration.DtoGenerator();
var generator = incGenerator.AsSourceGenerator();

#endif
