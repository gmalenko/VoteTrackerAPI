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
    public class VoteCandidate
    {
        private readonly ICacheOperation _cacheOperation;
        private readonly DbContextOptions<toafcContext> _toafcContextOptions;
        private VoteCandidateDB voteCandidateDB = null;

        public VoteCandidate(ICacheOperation cacheOperation)
        {
            _cacheOperation = cacheOperation;
            //_toafcContextOptions = toafcContextOptions;
            voteCandidateDB = new VoteCandidateDB();
        }

        public VoteCandidate()
        {
            //_cacheOperation = cacheOperation;
            //_toafcContextOptions = toafcContextOptions;
            //voteCandidateDB = new VoteCandidateDB(toafcContextOptions);
        }

        public async Task<List<Models.Database.VoteCandidate>> GetCandidates()
        {
            var cacheKey = "VoteCandidates";
            var voteCandidate = JsonConvert.DeserializeObject<List<Models.Database.VoteCandidate>>((_cacheOperation.Get(cacheKey) ?? "").ToString()) ?? new List<Models.Database.VoteCandidate>();
            if (voteCandidate.Count == 0)
            {
                voteCandidate = await voteCandidateDB.GetCandidates();
                _cacheOperation.Save(JsonConvert.SerializeObject(voteCandidate), cacheKey, 12, Services.Cache.Models.TimeUnit.Hours);
            }
            return voteCandidate;
        }

        public async Task<Models.Database.VoteCandidate> CreateCandidate(Models.Database.VoteCandidate candidate)
        {
            Exceptions.TestForNull(candidate, nameof(candidate));

            candidate = await voteCandidateDB.SaveCandidate(candidate);
            var cacheKey = "VoteCandidates";
            _cacheOperation.Delete(cacheKey);
            return candidate;
        }

        public async Task<Models.Database.VoteCandidate> UpdateCandidate(Models.Database.VoteCandidate candidate)
        {
            Exceptions.TestForNull(candidate, nameof(candidate));
            candidate = await voteCandidateDB.UpdateCandidate(candidate);
            var cacheKey = "VoteCandidates";
            _cacheOperation.Delete(cacheKey);
            return candidate;
        }

    }
}
