using System;
using System.Collections.Generic;

#nullable disable

namespace VoteTrackerAPI.Models.Database
{
    public partial class VotePeriod
    {
        public VotePeriod()
        {
            VoteCandidates = new HashSet<VoteCandidate>();
            VoteRegistrations = new HashSet<VoteRegistration>();
            VoteSelfRegistrations = new HashSet<VoteSelfRegistration>();
        }

        public Guid Id { get; set; }
        public int? Year { get; set; }

        public virtual ICollection<VoteCandidate> VoteCandidates { get; set; }
        public virtual ICollection<VoteRegistration> VoteRegistrations { get; set; }
        public virtual ICollection<VoteSelfRegistration> VoteSelfRegistrations { get; set; }
    }
}
