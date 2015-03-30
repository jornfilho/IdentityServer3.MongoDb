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
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Thinktecture.IdentityServer.MongoDb.Entities
{
    [BsonIgnoreExtraElements]
    public class Token
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual BsonObjectId Key { get; set; }

        [BsonElement("token_type")]
        [BsonRepresentation(BsonType.String)]
        public virtual TokenType TokenType { get; set; }

        [BsonElement("subject_id")]
        [BsonRepresentation(BsonType.String)]
        public virtual string SubjectId { get; set; }

        [BsonElement("client_id")]
        [BsonRepresentation(BsonType.String)]
        [BsonRequired]
        public virtual string ClientId { get; set; }

        [BsonElement("json_code")]
        [BsonRepresentation(BsonType.String)]
        [BsonRequired]
        public virtual string JsonCode { get; set; }

        [BsonElement("expiry")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonRequired]
        public virtual DateTimeOffset Expiry { get; set; }
    }
}
