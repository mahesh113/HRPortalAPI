using System;
using System.Collections.Generic;

#nullable disable

namespace HRPortal.WebApi.Models
{
    public partial class SessionAudit
    {
        public int? UserId { get; set; }
        public bool? Isloggedin { get; set; }
        public DateTime? Logintime { get; set; }
    }
}
