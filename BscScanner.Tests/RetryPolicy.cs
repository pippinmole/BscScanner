using Polly;
using Polly.Retry;
using System;
using System.Net;
using System.Net.Http;

namespace BscScanner.Tests
{
    internal class RetryPolicy
    {
        //Free plan = Limit 5 calls per second
        public static AsyncRetryPolicy BscScan = Policy
            .Handle<HttpRequestException>(e => e.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(5, attempt => TimeSpan.FromSeconds(1));
    }
}
