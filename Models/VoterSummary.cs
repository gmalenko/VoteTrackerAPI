using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoteTrackerAPI.Models
{
    public class VoterSummary
    {
        public Models.Database.VoteCandidate VoteCandidate { get; set; }
        public int Totals { get; set; }
    }
}
