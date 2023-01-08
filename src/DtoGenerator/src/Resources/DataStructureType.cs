/*
 * DataStructureType.cs
 *
 *   Created: 2023-01-03-05:50:10
 *   Modified: 2023-01-03-05:50:10
 *
 *   Author: Justin Chase <justin@justinwritescode.com>
 *
 *   Copyright © 2022-2023 Justin Chase, All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

internal enum DataStructureType
{
    /// <summary>Generate a standard C# DTO class</summary>
    Class,
    /// <summary>Generate a DTO record class</summary>
    RecordClass,
    /// <summary>Generate a standard C# DTO struct</summary>
    Struct,
    /// <summary>Generate a DTO record struct</summary>
    RecordStruct
}
