/*
 * AttributeProperty.cs
 *
 *   Created: 2022-12-10-02:05:58
 *   Modified: 2022-12-10-02:05:58
 *
 *   Author: Justin Chase <justin@justinwritescode.com>
 *
 *   Copyright © 2022 Justin Chase, All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace JustinWritesCode.CodeGeneration;

public record struct AttributeProperty(System.Type property_type, string property_name, string default_value = "null", bool is_required = false);
