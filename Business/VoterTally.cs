using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteTrackerAPI.Database;
using VoteTrackerAPI.Models;
using VoteTrackerAPI.Services.Cache;

namespace VoteTrackerAPI.Business
{
    public class VoterTally
    {
        private readonly ICacheOperation _cacheOperation;
        private VoterTalleyDB voterTallyDB = null;
        private string cacheKey = "VoterTallys";
        private SelfRegistration selfRegistration = null;
        private VoteCandidate voteCandidate = null;
        public VoterTally(ICacheOperation cacheOperation)
        {
            _cacheOperation = cacheOperation;
            voterTallyDB = new VoterTalleyDB();
            selfRegistration = new SelfRegistration(cacheOperation);
            voteCandidate = new VoteCandidate(cacheOperation);
        }

        public async Task<List<Models.VoterTalley>> GetVoterTallies()
        {
            var voterTallies = JsonConvert.DeserializeObject<List<Models.VoterTalley>>((_cacheOperation.Get(cacheKey) ?? "").ToString()) ?? new List<Models.VoterTalley>();
            if (voterTallies.Count == 0)
            {
                var dbVoterTallies = await voterTallyDB.GetVoterTally();
                if (dbVoterTallies.Count > 0)
                {
                    var selfRegisteredVoters = await selfRegistration.GetVoterRegistrations();
                    var candidates = await voteCandidate.GetCandidates();
                    voterTallies = new List<Models.VoterTalley>();
                    foreach (var tempDbTally in dbVoterTallies)
                    {
                        var tempVoterTally = new Models.VoterTalley()
                        {
                            Id = tempDbTally.Id,
                            IsDeleted = tempDbTally.IsDeleted,
                            Createdon = tempDbTally.Createdon,
                            VoteCandidate = candidates.Where(x => x.Id == tempDbTally.VoteCandidateId).FirstOrDefault(),
                            VoteSelfRegistration = selfRegisteredVoters.Where(x => x.Id == tempDbTally.VoteSelfRegistrationId).FirstOrDefault()
                        };
                        voterTallies.Add(tempVoterTally);
                    }
                    _cacheOperation.Save(JsonConvert.SerializeObject(voterTallies), cacheKey, 10, Services.Cache.Models.TimeUnit.Minutes);
                }
            }
            return voterTallies;
        }

        public async Task<Models.VoterTalley> InsertVoterTally(Models.VoterTalley voterTalley)
        {
            Exceptions.TestForNull(voterTalley, nameof(voterTalley));

            var dbVoterTally = new Models.Database.VoterTally()
            {
                IsDeleted = voterTalley.IsDeleted,
                Createdon = DateTime.Now,
                VoteCandidateId = voterTalley.VoteCandidate.Id,
                VoteSelfRegistrationId = voterTalley.VoteSelfRegistration.Id
            };
            dbVoterTally = await voterTallyDB.InsertTally(dbVoterTally);
            _cacheOperation.Delete(cacheKey);
            voterTalley.Id = dbVoterTally.Id;
            return voterTalley;
        }

        public async Task<List<VoterSummary>> GetVoterSummaries()
        {
            var voterTallies = await this.GetVoterTallies();
            var voterSummaryList = new List<VoterSummary>();


            var voterTalliesGroup = voterTallies.GroupBy(x => x.VoteCandidate.Id);
            foreach (var group in voterTalliesGroup)
            {
                var tempVoterSummary = new VoterSummary()
                {
                    Totals = group.Count(),
                    VoteCandidate = voterTallies.Where(x => x.VoteCandidate.Id == group.Key).FirstOrDefault().VoteCandidate
                };
                voterSummaryList.Add(tempVoterSummary);
            }
            voterSummaryList = voterSummaryList.OrderByDescending(x => x.Totals).ToList();

            return voterSummaryList;
        }
    }
}
