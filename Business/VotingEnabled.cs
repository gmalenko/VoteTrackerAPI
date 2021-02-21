using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteTrackerAPI.Services.Cache;

namespace VoteTrackerAPI.Business
{
    public class VotingEnabled
    {
        private readonly ICacheOperation _cacheOperation;
        private string cacheKey = "enabled";
        public VotingEnabled(ICacheOperation cacheOperation)
        {
            _cacheOperation = cacheOperation;
        }

        public async Task<Boolean> UpdateFlag(Boolean input)
        {
            _cacheOperation.Delete(cacheKey);
            _cacheOperation.Save(input, cacheKey, 10, Services.Cache.Models.TimeUnit.Hours);
            return input;
        }

        public async Task<Boolean> GetStatus()
        {
            var result = false;
            var currentState = _cacheOperation.Get(cacheKey);
            if (currentState == null)
            {
                result = false;
                _cacheOperation.Save(result, cacheKey, 10, Services.Cache.Models.TimeUnit.Hours);
            }
            else
            {
                result = Convert.ToBoolean(currentState);
            }
            return result;
        }


    }
}
