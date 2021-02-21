using System;
using System.Collections.Generic;

#nullable disable

namespace VoteTrackerAPI.Models.Database
{
    public partial class Userpassword
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string Password { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
