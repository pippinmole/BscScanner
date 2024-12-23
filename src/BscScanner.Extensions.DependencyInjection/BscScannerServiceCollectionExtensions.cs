using System;
using BscScanner;

namespace Microsoft.Extensions.DependencyInjection {
    public static class BscScannerServiceCollectionExtensions {

        public static IServiceCollection AddBscScanner(this IServiceCollection services, Action<BscScanClientOptions> options) {
            var opt = new BscScanClientOptions();
            if ( options != null ) options(opt);

            services.AddSingleton<IBscScanClient, BscScanClient>(x => new BscScanClient(opt.ApiKey));

            return services;
        }
    }
}