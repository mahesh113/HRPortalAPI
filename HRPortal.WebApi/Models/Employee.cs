using System;
using System.Collections.Generic;

#nullable disable

namespace HRPortal.WebApi.Models
{
    public partial class Employee
    {
        public long Id { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Department { get; set; }
        public int? Status { get; set; }
    }
}
