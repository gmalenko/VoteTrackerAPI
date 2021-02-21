using System;
using VoteTrackerAPI.Services.Cache.Models;

namespace VoteTrackerAPI.Services.Cache
{
    public interface ICacheOperation: IDisposable
    {
        string EnvironmentName { get; }
        Boolean Save(object value, string key, int timeoutTime, TimeUnit timeUnit, Boolean storeKey = false);
        Object Get(string key);
        Boolean Delete(string key, Boolean keyGenerated = false);
        Boolean DeleteContainingKey(string key);
    }
}