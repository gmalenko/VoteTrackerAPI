using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteTrackerAPI.Database.DBContext;

namespace VoteTrackerAPI.Database
{
    public class VotineEnabledDB
    {
        public async Task<Boolean> IsVotingEnabled()
        {
            using (var context = new toafcContext())
            {
                return context.VotingEnableds.FirstOrDefault().Status;
            }
        }

        public async Task<Boolean> Update(Boolean input)
        {
            using (var context = new toafcContext())
            {
                context.VotingEnableds.Add(new Models.Database.VotingEnabled()
                {
                    Status = input
                });
                await context.SaveChangesAsync();
                return input;
            }
        }
    }
}
