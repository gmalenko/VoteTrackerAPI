using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteTrackerAPI.Database.DBContext;

namespace VoteTrackerAPI.Database
{
    public class VoteRegistrationDB
    {
        public VoteRegistrationDB()
        {

        }

        public async Task<List<Models.Database.VoteRegistration>> GetRegistredVoters()
        {
            var voterList = new List<Models.Database.VoteRegistration>();
            using (var context = new toafcContext())
            {
                var temp = (from tempVoterRegistration in context.VoteRegistrations
                            join tempVotePeriod in context.VotePeriods on tempVoterRegistration.VotePeriod equals tempVotePeriod.Id into votePeriodJoin
                            from votePeriodJoinVal in votePeriodJoin.DefaultIfEmpty()
                            select new Models.Database.VoteRegistration()
                            {
                                Firstname = tempVoterRegistration.Firstname,
                                Createdbyuser = tempVoterRegistration.Createdbyuser,
                                Createdon = tempVoterRegistration.Createdon,
                                Id = tempVoterRegistration.Id,
                                IsElgible = tempVoterRegistration.IsElgible,
                                Lastname = tempVoterRegistration.Lastname,
                                Modifiedbyuser = tempVoterRegistration.Modifiedbyuser,
                                Modifiedon = tempVoterRegistration.Modifiedon,
                                VotePeriod = votePeriodJoinVal.Id
                            }).ToList();
                voterList = temp;
            }
            return voterList;
        }

        public async Task<Models.Database.VoteRegistration> CreateRegistration(Models.Database.VoteRegistration voteRegistration)
        {
            using (var context = new toafcContext())
            {
                context.VoteRegistrations.Add(voteRegistration);
                await context.SaveChangesAsync();
            }
            return voteRegistration;
        }

        public async Task<Models.Database.VoteRegistration> UpdateRegistration(Models.Database.VoteRegistration voteRegistration)
        {
            using (var context = new toafcContext())
            {
                context.VoteRegistrations.Update(voteRegistration);
                await context.SaveChangesAsync();
            }
            return voteRegistration;
        }

    }
}
