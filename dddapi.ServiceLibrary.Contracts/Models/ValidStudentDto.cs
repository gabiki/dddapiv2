using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dddapi.ServiceLibrary.Contracts.Models
{    public class ValidStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool Valid { get; set; } 
    }
}
