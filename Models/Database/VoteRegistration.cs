using System;
using System.Collections.Generic;

#nullable disable

namespace VoteTrackerAPI.Models.Database
{
    public partial class VoteRegistration
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool? IsElgible { get; set; }
        public Guid? VotePeriod { get; set; }
        public DateTime? Createdon { get; set; }
        public Guid? Createdbyuser { get; set; }
        public DateTime? Modifiedon { get; set; }
        public Guid? Modifiedbyuser { get; set; }

        public virtual VotePeriod VotePeriodNavigation { get; set; }
    }
}
