using System;
using System.Collections.Generic;

#nullable disable

namespace HRPortal.WebApi.Models
{
    public partial class User
    {
        public long Id { get; set; }
        public long? LoginId { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool? Active { get; set; }
    }
}
