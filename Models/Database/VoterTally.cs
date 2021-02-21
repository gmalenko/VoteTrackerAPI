using System;
using System.Collections.Generic;

#nullable disable

namespace VoteTrackerAPI.Models.Database
{
    public partial class VoterTally
    {
        public Guid Id { get; set; }
        public Guid? VoteSelfRegistrationId { get; set; }
        public Guid? VoteCandidateId { get; set; }
        public DateTime? Createdon { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
