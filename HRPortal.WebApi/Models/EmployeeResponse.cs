using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRPortal.WebApi.Models
{
    public class EmployeeResponse
    {
        public long Id { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
    }
}
