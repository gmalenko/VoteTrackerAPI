using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteTrackerAPI.Database.DBContext;

namespace VoteTrackerAPI.Database
{
    public class SelfRegistrationDB
    {
        public SelfRegistrationDB()
        {

        }

        public async Task<List<Models.Database.VoteSelfRegistration>> GetRegistredVoters()
        {
            var voterList = new List<Models.Database.VoteSelfRegistration>();
            using (var context = new toafcContext())
            {
                var temp = (from tempVoterRegistration in context.VoteSelfRegistrations
                            join tempVotePeriod in context.VotePeriods on tempVoterRegistration.VotePeriod equals tempVotePeriod.Id into votePeriodJoin
                            from votePeriodJoinVal in votePeriodJoin.DefaultIfEmpty()
                            select new Models.Database.VoteSelfRegistration()
                            {
                                Firstname = tempVoterRegistration.Firstname,
                                Createdbyuser = tempVoterRegistration.Createdbyuser,
                                Createdon = tempVoterRegistration.Createdon,
                                Id = tempVoterRegistration.Id,                                
                                Lastname = tempVoterRegistration.Lastname,
                                Modifiedbyuser = tempVoterRegistration.Modifiedbyuser,
                                Modifiedon = tempVoterRegistration.Modifiedon,
                                VotePeriod = votePeriodJoinVal.Id,
                                Email = tempVoterRegistration.Email
                            }).ToList();
                voterList = temp;
            }
            return voterList;
        }

        public async Task<Models.Database.VoteSelfRegistration> CreateRegistration(Models.Database.VoteSelfRegistration voteRegistration)
        {
            using (var context = new toafcContext())
            {
                context.VoteSelfRegistrations.Add(voteRegistration);
                await context.SaveChangesAsync();
            }
            return voteRegistration;
        }

        public async Task<Models.Database.VoteSelfRegistration> UpdateRegistration(Models.Database.VoteSelfRegistration voteRegistration)
        {
            using (var context = new toafcContext())
            {
                context.VoteSelfRegistrations.Update(voteRegistration);
                await context.SaveChangesAsync();
            }
            return voteRegistration;
        }

    }
}
