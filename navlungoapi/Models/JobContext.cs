using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace navlungoapi.Models
{
    public class JobContext:DbContext
    {
        public JobContext(DbContextOptions<JobContext> options)
            :base(options)
        {

        }
        public DbSet<Job> Jobs { get; set; }
    }
}
