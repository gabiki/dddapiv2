using dddapi.Infrastructure.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dddapi.Infrastructure.Impl.Context
{
    public class DataContext : DbContext // cuando descargue EF
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<StudentRepository>? Students { get; set; }
        public DbSet<TeacherRepository>? Teachers { get; set; }
    }
}
