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

namespace Thinktecture.IdentityServer.MongoDb
{
    /// <summary>
    /// This attribute is used to represent a string value
    /// for a value in an enum.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class StringValueAttribute : Attribute, IAttribute<string>
    {
        #region Constructor

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            _value = value;
        }

        #endregion Constructor

        /// <summary>
        /// Holds the currency symbol for a value in an enum.
        /// </summary>
        public string Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        private readonly string _value;
    }
}
