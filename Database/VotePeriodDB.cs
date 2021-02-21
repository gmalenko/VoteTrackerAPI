using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteTrackerAPI.Database.DBContext;

namespace VoteTrackerAPI.Database
{
    public class VotePeriodDB
    {
        private readonly DbContextOptions<toafcContext> _toafcContextOptions;

        public VotePeriodDB()
        {
            
        }


        public async Task<List<Models.Database.VotePeriod>> GetVotePeriods()
        {
            using (var context = new toafcContext())
            {
                return context.VotePeriods.ToList();
            }
        }

        public async Task<Models.Database.VotePeriod> SetVotePeriod(Models.Database.VotePeriod votePeriod)
        {            
            using (var context = new toafcContext())
            {
                context.VotePeriods.Add(votePeriod);
                await context.SaveChangesAsync();
            }
            return votePeriod;
        }
                


    }
}
