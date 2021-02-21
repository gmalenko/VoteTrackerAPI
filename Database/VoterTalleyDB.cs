using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteTrackerAPI.Database.DBContext;

namespace VoteTrackerAPI.Database
{
    public class VoterTalleyDB
    {
        public VoterTalleyDB() { }

        public async Task<List<Models.Database.VoterTally>> GetVoterTally()
        {
            var voterTalley = new List<Models.Database.VoterTally>();
            using (var context = new toafcContext())
            {
                var temp = context.VoterTallies.Where(x=> x.IsDeleted == false).ToList();
                voterTalley = temp;
            }
            return voterTalley;
        }

        public async Task<Models.Database.VoterTally> InsertTally(Models.Database.VoterTally voterTally)
        {
            voterTally.IsDeleted = false;
            using (var context = new toafcContext())
            {
                context.VoterTallies.Add(voterTally);
                await context.SaveChangesAsync();
            }
            return voterTally;
        }

    }
}
