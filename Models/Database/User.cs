using System;
using System.Collections.Generic;

#nullable disable

namespace VoteTrackerAPI.Models.Database
{
    public partial class User
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public bool? IsDisabled { get; set; }
        public bool? IsDeleted { get; set; }
        public byte[] Salt { get; set; }
    }
}
