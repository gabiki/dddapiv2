using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dddapi.ServiceLibrary.Contracts.Models
{
    public class IsRetiredTeacher
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool Retired { get; set; }
        public bool isRetired { get; set; }
        public string Course { get; set; }
        public string Telephone { get; set; }
        public string Country { get; set; }
        public int YearsToRetire { get; set; }
    }
}
