using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteTrackerAPI.Database;
using VoteTrackerAPI.Services.Cache;

namespace VoteTrackerAPI.Business
{
    public class SelfRegistration
    {
        private readonly ICacheOperation _cacheOperation;
        private SelfRegistrationDB voteRegistrationDB;
        private string cacheKey = "VoterRegistration";
        public SelfRegistration(ICacheOperation cacheOperation)
        {
            _cacheOperation = cacheOperation;
            voteRegistrationDB = new SelfRegistrationDB();
        }


        public async Task<List<Models.Database.VoteSelfRegistration>> GetVoterRegistrations()
        {
         
            var voterRegistration = JsonConvert.DeserializeObject<List<Models.Database.VoteSelfRegistration>>((_cacheOperation.Get(cacheKey) ?? "").ToString()) ?? new List<Models.Database.VoteSelfRegistration>();
            if (voterRegistration.Count == 0)
            {
                voterRegistration = await voteRegistrationDB.GetRegistredVoters();
                _cacheOperation.Save(JsonConvert.SerializeObject(voterRegistration), cacheKey, 12, Services.Cache.Models.TimeUnit.Hours);
            }

            return voterRegistration;
        }

        public async Task<Models.Database.VoteSelfRegistration> CreateVoterRegistration(Models.Database.VoteSelfRegistration voteRegistration)
        {
            Exceptions.TestForNull(voteRegistration, nameof(voteRegistration));

            voteRegistration = await voteRegistrationDB.CreateRegistration(voteRegistration);
            _cacheOperation.Delete(cacheKey);
            return voteRegistration;
        }

        public async Task<Models.Database.VoteSelfRegistration> UpdateVoterRegistration(Models.Database.VoteSelfRegistration voteRegistration)
        {
            Exceptions.TestForNull(voteRegistration, nameof(voteRegistration));

            voteRegistration = await voteRegistrationDB.UpdateRegistration(voteRegistration);
            _cacheOperation.Delete(cacheKey);
            return voteRegistration;
        }

    }
}
