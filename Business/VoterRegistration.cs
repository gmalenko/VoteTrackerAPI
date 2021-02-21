﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteTrackerAPI.Database;
using VoteTrackerAPI.Services.Cache;

namespace VoteTrackerAPI.Business
{
    public class VoterRegistration
    {
        private readonly ICacheOperation _cacheOperation;
        private VoteRegistrationDB voteRegistrationDB;
        private string cacheKey = "VoterRegistration";
        public VoterRegistration(ICacheOperation cacheOperation)
        {
            _cacheOperation = cacheOperation;
            voteRegistrationDB = new VoteRegistrationDB();
        }


        public async Task<List<Models.Database.VoteRegistration>> GetVoterRegistrations()
        {
         
            var voterRegistration = JsonConvert.DeserializeObject<List<Models.Database.VoteRegistration>>((_cacheOperation.Get(cacheKey) ?? "").ToString()) ?? new List<Models.Database.VoteRegistration>();
            if (voterRegistration.Count == 0)
            {
                voterRegistration = await voteRegistrationDB.GetRegistredVoters();
                _cacheOperation.Save(JsonConvert.SerializeObject(voterRegistration), cacheKey, 12, Services.Cache.Models.TimeUnit.Hours);
            }

            return voterRegistration;
        }

        public async Task<Models.Database.VoteRegistration> CreateVoterRegistration(Models.Database.VoteRegistration voteRegistration)
        {
            Exceptions.TestForNull(voteRegistration, nameof(voteRegistration));

            voteRegistration = await voteRegistrationDB.CreateRegistration(voteRegistration);
            _cacheOperation.Delete(cacheKey);
            return voteRegistration;
        }

        public async Task<Models.Database.VoteRegistration> UpdateVoterRegistration(Models.Database.VoteRegistration voteRegistration)
        {
            Exceptions.TestForNull(voteRegistration, nameof(voteRegistration));

            voteRegistration = await voteRegistrationDB.UpdateRegistration(voteRegistration);
            _cacheOperation.Delete(cacheKey);
            return voteRegistration;
        }

    }
}
