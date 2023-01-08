/*
 * AttributeInfo.cs
 *
 *   Created: 2022-12-10-02:14:13
 *   Modified: 2022-12-10-02:14:13
 *
 *   Author: Justin Chase <justin@justinwritescode.com>
 *
 *   Copyright © 2022-2023 Justin Chase, All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

using System;

namespace JustinWritesCode.CodeGeneration;

public record struct AttributeInfo(string attribute_class_name, Type attribute_base_type, string attribute_targets, AttributeProperty[] attribute_properties);
