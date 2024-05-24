// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AppCenter.Ingestion.Models;
using Microsoft.AppCenter.Ingestion.Models.Serialization;

namespace Microsoft.AppCenter.Ingestion.Http
{
    internal sealed class IngestionHttp : IIngestion
    {
        internal const string DefaultBaseUrl = "https://in.appcenter.ms";
        internal const string ApiVersion = "/logs?api-version=1.0.0";
        internal const string AppSecret = "App-Secret";
        internal const string InstallId = "Install-ID";

        private const int MaximumCharactersDisplayedForAppSecret = 8;

        private string _baseLogUrl;

        private readonly IHttpNetworkAdapter _httpNetwork;

        public IngestionHttp(IHttpNetworkAdapter httpNetwork)
        {
            _httpNetwork = httpNetwork;
        }

        public IServiceCall Call(string appSecret, Guid installId, IList<Log> logs)
        {
            var call = new ServiceCall(appSecret, installId, logs);
            if (!IsEnabled)
            {
                call.SetException(new NetworkIngestionException(new Exception("SDK is in offline mode.")));
                return call;
            }
            CallAsync(logs, call.CancellationToken).ContinueWith(task =>
            {
                // Cancellation token is already shared.
                if (task.IsCanceled)
                {
                    return;
                }

                // If task is faulted.
                if (task.IsFaulted)
                {
                    call.SetException(task.Exception?.InnerException);
                    return;
                }

                // If task is succeeded.
                call.SetResult(task.Result);
            });
            return call;
        }

        /// <exception cref="IngestionException"/>
        private async Task<string> CallAsync(IList<Log> logs, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            // Create request content.
            var requestContent = CreateLogsContent(logs);
            AppCenterLog.Verbose(AppCenterLog.LogTag, requestContent);
            // Send request.
            return await _httpNetwork.SendAsync("https://localhost:54478/api/Errors/Log", "POST", requestContent, token).ConfigureAwait(false);
        }

        public void Close()
        {
            // No-op
        }

        public void SetLogUrl(string logUrl)
        {
            _baseLogUrl = logUrl;
        }

        private static string GetRedactedAppSecret(string appSecret)
        {
            var endHidingIndex = Math.Max(appSecret.Length - MaximumCharactersDisplayedForAppSecret, 0);
            var redactedAppSecret = "";
            for (var i = 0; i < endHidingIndex; ++i)
            {
                redactedAppSecret += '*';
            }
            redactedAppSecret += appSecret.Substring(endHidingIndex);
            return redactedAppSecret;
        }

        internal IDictionary<string, string> CreateHeaders(string appSecret, Guid installId)
        {
            return new Dictionary<string, string>
            {
                { AppSecret, appSecret },
                { InstallId, installId.ToString() }
            };
        }

        private string CreateLogsContent(IList<Log> logs)
        {
            var logContainer = new LogContainer(logs);

            // Serialize request.
            var requestContent = LogSerializer.Serialize(logContainer);
            return requestContent;
        }

        public void Dispose()
        {
            _httpNetwork.Dispose();
        }

        public bool IsEnabled 
        { 
            get => AppCenter.PlatformIsNetworkRequestsAllowed; 
        }
    }
}
