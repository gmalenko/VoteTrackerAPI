using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteTrackerAPI.Services.Cache.Models;

namespace VoteTrackerAPI.Services.Cache
{
    public class CacheOperation : ICacheOperation
    {
        public string EnvironmentName { get; set; }

        private IMemoryCache cache;
        private List<string> cacheKeyList;

        public CacheOperation()
        {
            //put initialize code here
            Initilize();
        }

        public void Initilize()
        {
            cache = new MemoryCache(new MemoryCacheOptions
            {
                //1 gig of memory.  Will this be enough?
                SizeLimit = 1024
            });

            cacheKeyList = new List<string>();
        }

        public Boolean Save(object value, string key, int timeoutTime = 0, TimeUnit timeUnit = TimeUnit.Minutes, Boolean storeKey = false)
        {
            var result = false;
            if (timeoutTime == 0)
            {
                timeoutTime = 5;
                timeUnit = TimeUnit.Minutes;
            }
            try
            {
                var generatedKey = this.GenerateKey(key);
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSize(1).SetAbsoluteExpiration(this.GenerateOffSet(timeoutTime, timeUnit));
                cache.Set(generatedKey, value, cacheEntryOptions);
                result = true;

                if (storeKey)
                {
                    if (cacheKeyList.Contains(generatedKey))
                    {
                        cacheKeyList.Remove(generatedKey);
                    }
                    cacheKeyList.Add(generatedKey);
                }


            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }

        public Object Get(string key)
        {
            var result = new Object();
            try
            {
                result = cache.Get(this.GenerateKey(key));
            }
            catch (Exception e)
            {
                result = null;
            }
            return result;
        }

        public Boolean Delete(string key, Boolean keyGenerated = false)
        {
            var result = false;
            try
            {
                var tempKey = "";
                if (!keyGenerated)
                {
                    tempKey = this.GenerateKey(key);
                }
                else
                {
                    tempKey = key;
                }
                cache.Remove(tempKey);
                cacheKeyList.Remove(tempKey);
                result = true;
            }
            catch (Exception e)
            {
                result = true;
            }
            return result;
        }

        public Boolean DeleteContainingKey(string key)
        {
            var result = false;
            try
            {
                var containingKeyList = cacheKeyList.Where(x => x.Contains(key)).ToList();
                foreach (var tempkey in containingKeyList)
                {
                    this.Delete(tempkey, true);
                };
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }


        public void Dispose()
        {
            cache.Dispose();
        }

        private string GenerateKey(string key)
        {
            var sb = new StringBuilder();
            sb.Append("Voter-");
            sb.Append(this.EnvironmentName);
            sb.Append("-");
            sb.Append(key);

            return sb.ToString();
        }

        private DateTimeOffset GenerateOffSet(int timoutTime, TimeUnit timeUnit)
        {
            var result = new DateTimeOffset();
            switch (timeUnit)
            {
                case TimeUnit.Hours:
                    result = DateTime.UtcNow.AddHours(timoutTime);
                    break;
                case TimeUnit.Milliseconds:
                    result = DateTime.UtcNow.AddMilliseconds(timoutTime);
                    break;
                case TimeUnit.Minutes:
                    result = DateTime.UtcNow.AddMinutes(timoutTime);
                    break;
                case TimeUnit.Seconds:
                    result = DateTime.UtcNow.AddSeconds(timoutTime);
                    break;
                case TimeUnit.Days:
                    result = DateTime.UtcNow.AddDays(timoutTime);
                    break;
            }
            return result;
        }
    }
}
