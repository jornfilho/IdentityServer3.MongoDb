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
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Thinktecture.IdentityServer.Core.Logging;

namespace Thinktecture.IdentityServer.MongoDb
{
    public class TokenCleanup
    {
        private readonly static ILog Logger = LogProvider.GetCurrentClassLogger();

        readonly MongoDbServiceOptions _options;
        CancellationTokenSource _source;
        readonly TimeSpan _interval;

        public TokenCleanup(MongoDbServiceOptions options, int interval = 60)
        {
            if (options == null) 
                throw new ArgumentNullException("options");

            if (interval < 1) 
                throw new ArgumentException("interval must be more than 1 second");

            _options = options;
            _interval = TimeSpan.FromSeconds(interval);
        }

        public void Start()
        {
            if (_source != null) 
                throw new InvalidOperationException("Already started. Call Stop first.");
            
            _source = new CancellationTokenSource();
            Start(_source.Token);
        }
        
        public void Stop()
        {
            if (_source == null) 
                throw new InvalidOperationException("Not started. Call Start first.");

            _source.Cancel();
            _source = null;
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Logger.Info("CancellationRequested");
                    break;
                }

                try
                {
                    await Task.Delay(_interval, cancellationToken);
                }
                catch
                {
                    Logger.Info("Task.Delay exception. exiting.");
                    break;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    Logger.Info("CancellationRequested");
                    break;
                }

                await ClearTokens();
            }
        }

        private async Task ClearTokens()
        {
            try
            {
                Logger.Info("Clearing tokens");

                IMongoQuery queryRemove = Query.And(
                    Query.Exists("expiry"),
                    Query.LT("expiry", new BsonDateTime(DateTimeOffset.UtcNow))
                );

                var tokenCollection = MongoContext.GetTokensCollection();

                var bulk = tokenCollection.InitializeUnorderedBulkOperation();
                bulk.Find(queryRemove).Remove();
                bulk.Execute();
            }
            catch(Exception ex)
            {
                Logger.ErrorException("Exception cleanring tokens", ex);
            }
        }
    }
}
