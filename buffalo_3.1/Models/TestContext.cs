using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace buffalo_3._1.Models
{
    public class TestContext : DbContext
    {
        public TestContext() { }

        public TestContext(DbContextOptions<TestContext> options) : base(options) { }

        public DbSet<Burialmain> Burials { get; set; }
    }
}
