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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Thinktecture.IdentityServer.Core.Services;
using Thinktecture.IdentityServer.MongoDb.Entities;

namespace Thinktecture.IdentityServer.MongoDb
{
    public class ClientConfigurationCorsPolicyService : ICorsPolicyService
    {
        readonly MongoCollection<Client> _context;

        public ClientConfigurationCorsPolicyService(MongoCollection<Client> ctx)
        {
            _context = ctx;
        }

        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            IMongoQuery queryCors = Query.And(Query.Exists("allowed_cors_origins"));

            var clientsResult = _context
                .Find(queryCors)
                .SetFields("allowed_cors_origins")
                .ToList();

            if (!clientsResult.Any())
                return false;

            var corsList = clientsResult.Where(a=> a.AllowedCorsOrigins != null && a.AllowedCorsOrigins.Any())
                .Select(a => a.AllowedCorsOrigins)
                .ToList();
            
            if (!corsList.Any())
                return false;

            var originsList = new List<string>();
            foreach (var client in corsList)
            {
                if(client == null)
                    continue;

                var clientOrigins = client.Select(c => c.Origin.GetOrigin())
                    .Where(c => !string.IsNullOrEmpty(c)).ToList();

                if(!originsList.Any())
                    continue;

                originsList.AddRange(clientOrigins);
            }

            if (!originsList.Any())
                return false;
            
            var result = originsList.Distinct().Contains(origin, StringComparer.OrdinalIgnoreCase);
            
            return result;
        }
    }
}
