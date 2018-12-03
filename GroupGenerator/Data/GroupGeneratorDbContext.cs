using GroupGenerator.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupGenerator.Data
{
    public class GroupGeneratorDbContext : DbContext
    {
        public GroupGeneratorDbContext(DbContextOptions<GroupGeneratorDbContext> options) : base(options)
        {

        }

        public DbSet<Class> Class { get; set;}
    }
}
