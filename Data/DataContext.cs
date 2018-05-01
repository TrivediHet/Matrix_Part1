using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matrix.Models;
using Microsoft.EntityFrameworkCore;

namespace Matrix.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }

        public DbSet<User> UserList { get; set; }
    }
}
