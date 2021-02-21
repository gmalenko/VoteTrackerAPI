using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteTrackerAPI.Database;
using VoteTrackerAPI.Database.DBContext;
using VoteTrackerAPI.Services.Cache;

namespace VoteTrackerAPI.Business
{
    public class VotePeriod
    {
        private readonly ICacheOperation _cacheOperation;
        private readonly DbContextOptions<toafcContext> _toafcContextOptions;
        private VotePeriodDB votePeriodDB = null;
        public VotePeriod(ICacheOperation cacheOperation)
        {
            _cacheOperation = cacheOperation;            
            votePeriodDB = new VotePeriodDB();
        }
        public async Task<List<Models.Database.VotePeriod>> GetVotePeriods()
        {
            var cacheKey = "VotePeriods";
            var votePeriod = JsonConvert.DeserializeObject<List<Models.Database.VotePeriod>>((_cacheOperation.Get(cacheKey) ?? "").ToString()) ?? new List<Models.Database.VotePeriod>();
            if(votePeriod.Count == 0)
            {
                votePeriod = await votePeriodDB.GetVotePeriods();
                _cacheOperation.Save(JsonConvert.SerializeObject(votePeriod), cacheKey, 12, Services.Cache.Models.TimeUnit.Hours);
            }
            return votePeriod;
        }

        public async Task<Models.Database.VotePeriod> GetCurrentVotePeriod()
        {
            var currentYear = DateTime.Now.Year;
            var votePeriods = await this.GetVotePeriods();
            return votePeriods.Where(x => x.Year == currentYear).FirstOrDefault();
        }



    }
}
