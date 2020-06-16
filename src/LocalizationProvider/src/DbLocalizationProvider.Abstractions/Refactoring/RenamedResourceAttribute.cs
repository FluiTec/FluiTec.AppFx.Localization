// Copyright © 2017 Valdis Iljuconoks.
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace FluiTec.DbLocalizationProvider.Abstractions.Refactoring
{
    /// <summary>   Attribute for renamed resource. </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class RenamedResourceAttribute : Attribute
    {
        /// <summary>   Default constructor. </summary>
        public RenamedResourceAttribute()
        {
        }

        /// <summary>   Constructor. </summary>
        /// <exception cref="ArgumentException">
        ///     Thrown when one or more arguments have unsupported or
        ///     illegal values.
        /// </exception>
        /// <param name="oldName">      The name of the old. </param>
        /// <param name="oldNamespace"> The old namespace. </param>
        public RenamedResourceAttribute(string oldName, string oldNamespace)
        {
            if (string.IsNullOrWhiteSpace(oldName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(oldName));

            OldName = oldName;
            OldNamespace = oldNamespace;
        }

        /// <summary>   Constructor. </summary>
        /// <param name="oldName">  The name of the old. </param>
        public RenamedResourceAttribute(string oldName) : this(oldName, null)
        {
        }

        /// <summary>   Gets or sets the name of the old. </summary>
        /// <value> The name of the old. </value>
        public string OldName { get; set; }

        /// <summary>   Gets or sets the old namespace. </summary>
        /// <value> The old namespace. </value>
        public string OldNamespace { get; set; }
    }
}