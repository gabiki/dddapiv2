using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dddapi.Infrastructure.Contracts.Models
{
    public class TeacherRepository
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool Retired { get; set; }
        public string Course { get; set; }
        public string Telephone { get; set; }
        public string Country { get; set; }
    }
}
