﻿/*
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

namespace Thinktecture.IdentityServer.MongoDb
{
    class EfConstants
    {
        public const string ConnectionName = "Thinktecture.IdentityServer3";

        public class TableNames
        {
            public const string Client = "Clients";
            public const string ClientClaim = "ClientClaims";
            public const string ClientGrantTypeRestriction = "ClientGrantTypeRestrictions";
            public const string ClientIdPRestriction = "ClientIdPRestrictions";
            public const string ClientPostLogoutRedirectUri = "ClientPostLogoutRedirectUris";
            public const string ClientRedirectUri = "ClientRedirectUris";
            public const string ClientScopeRestriction = "ClientScopeRestrictions";
            public const string ClientSecret = "ClientSecrets";
            public const string ClientCorsOrigin = "ClientCorsOrigins";

            public const string Scope = "Scopes";
            public const string ScopeClaim = "ScopeClaims";

            public const string Consent = "Consents";
            public const string Token = "Tokens";
        }
    }
}
