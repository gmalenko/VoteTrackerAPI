using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteTrackerAPI.Database.DBContext;

namespace VoteTrackerAPI.Database
{
    public class VoteCandidateDB
    {
        private readonly DbContextOptions<toafcContext> _toafcContextOptions;
        public VoteCandidateDB()
        {
            //_toafcContextOptions = toafcContextOptions;
        }

        public async Task<List<Models.Database.VoteCandidate>> GetCandidates()
        {
            var candidateList = new List<Models.Database.VoteCandidate>();
            using (var context = new toafcContext())
            {
                var temp = (from tempCandidate in context.VoteCandidates
                            join tempVotePeriod in context.VotePeriods on tempCandidate.VotePeriod equals tempVotePeriod.Id into votePeriodJoin
                            from votePeriodJoinVal in votePeriodJoin.DefaultIfEmpty()
                            select new Models.Database.VoteCandidate()
                            {
                                Firstname = tempCandidate.Firstname,
                                IsDeleted = tempCandidate.IsDeleted,
                                Createdbyuser = tempCandidate.Createdbyuser,
                                Createdon = tempCandidate.Createdon,
                                Id = tempCandidate.Id,
                                Lastname = tempCandidate.Lastname,
                                Modifiedbyuser = tempCandidate.Modifiedbyuser,
                                Modifiedon = tempCandidate.Modifiedon,
                                VotePeriod = votePeriodJoinVal.Id
                            }).ToList();
                candidateList = temp;
            }
            return candidateList;
        }

        public async Task<Models.Database.VoteCandidate> SaveCandidate(Models.Database.VoteCandidate voteCandidate)
        {
            using (var context = new toafcContext())
            {
                context.VoteCandidates.Add(voteCandidate);
                await context.SaveChangesAsync();
            }
            return voteCandidate;
        }

        public async Task<Models.Database.VoteCandidate> UpdateCandidate(Models.Database.VoteCandidate voteCandidate)
        {
            using (var context = new toafcContext())
            {
                context.VoteCandidates.Update(voteCandidate);
                await context.SaveChangesAsync();
            }
            return voteCandidate;
        }
    }
}