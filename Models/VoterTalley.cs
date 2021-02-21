using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoteTrackerAPI.Models
{
    public class VoterTalley
    {
        public Guid Id { get; set; }
        public Models.Database.VoteSelfRegistration VoteSelfRegistration { get; set; }
        public Models.Database.VoteCandidate VoteCandidate { get; set; }
        public DateTime? Createdon { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
