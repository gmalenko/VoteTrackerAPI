using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteTrackerAPI.Services.Cache;

namespace VoteTrackerAPI.Services
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddCustomService(this IServiceCollection services)
        {
            services.AddSingleton<ICacheOperation, CacheOperation>();
            return services;

        }
    }
}
