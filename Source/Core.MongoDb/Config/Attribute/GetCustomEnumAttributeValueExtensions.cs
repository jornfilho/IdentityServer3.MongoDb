/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Diagnostics;
using System.Globalization;

namespace Thinktecture.IdentityServer.MongoDb
{
    /// <summary>
    /// Class to get custom enum option attribute
    /// </summary>
    public static class GetCustomEnumAttributeValueExtensions
    {
        /// <summary>
        /// Get custom enum attribute value
        /// </summary>
        /// <typeparam name="T">Attribute class</typeparam>
        /// <typeparam name="TR">Attribute type</typeparam>
        /// <param name="enum">enumevator</param>
        /// <returns>attribute value</returns>
        public static TR GetCustomEnumAttributeValue<T, TR>(this IConvertible @enum)
        {
            var attributeValue = default(TR);
            try
            {
                if (@enum != null)
                {
                    var fi = @enum.GetType().GetField(@enum.ToString(CultureInfo.InvariantCulture));
                    if (fi != null)
                    {
                        var attributes = fi.GetCustomAttributes(typeof(T), false) as T[];
                        if (attributes != null && attributes.Length > 0)
                        {
                            var attribute = attributes[0] as IAttribute<TR>;
                            if (attribute != null)
                                attributeValue = attribute.Value;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return attributeValue;
        }
    }
}
