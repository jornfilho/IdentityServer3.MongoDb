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
using System.Configuration;
using MongoDB.Driver;
using Thinktecture.IdentityServer.Core.Models;

namespace Thinktecture.IdentityServer.MongoDb
{
    public static class MongoContext
    {
        public static MongoDatabase GetDatabase(string connectionName = EfConstants.ConnectionName)
        {
            if(string.IsNullOrEmpty(connectionName))
                throw new ArgumentException("Invalid connection string name");

            var connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("Invalid connection string value");

            var databaseName = MongoUrl.Create(connectionString).DatabaseName;
            MongoServer server = new MongoClient(connectionString).GetServer();
            MongoDatabase databese = server.GetDatabase(databaseName);
            return databese;
        }

        public static MongoCollection<Token> GetTokensCollection(string collectionName = EfConstants.TableNames.Token)
        {
            return GetDatabase().GetCollection<Token>(collectionName);
        }

        public static MongoCollection<Client> GetClientsCollection(string collectionName = EfConstants.TableNames.Client)
        {
            return GetDatabase().GetCollection<Client>(collectionName);
        }
    }
}
