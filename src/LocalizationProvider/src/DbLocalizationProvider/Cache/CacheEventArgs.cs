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

namespace FluiTec.DbLocalizationProvider.Cache
{
    /// <summary>   (Serializable) additional information for cache events. </summary>
    [Serializable]
    public class CacheEventArgs
    {
        /// <summary>   Static readonly cache event information. </summary>
        public static readonly CacheEventArgs Empty =
            new CacheEventArgs(CacheOperation.None, string.Empty, string.Empty);

        /// <summary>   Constructor. </summary>
        /// <param name="operation">    The operation. </param>
        /// <param name="cacheKey">     The cache key. </param>
        /// <param name="resourceKey">  The resource key. </param>
        public CacheEventArgs(CacheOperation operation, string cacheKey, string resourceKey)
        {
            Operation = operation;
            CacheKey = cacheKey;
            ResourceKey = resourceKey;
        }

        /// <summary>   Gets the operation. </summary>
        /// <value> The operation. </value>
        public CacheOperation Operation { get; }

        /// <summary>   Gets the cache key. </summary>
        /// <value> The cache key. </value>
        public string CacheKey { get; }

        /// <summary>   Gets the resource key. </summary>
        /// <value> The resource key. </value>
        public string ResourceKey { get; }
    }
}
