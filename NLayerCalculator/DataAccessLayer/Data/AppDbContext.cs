using CommonLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class AppDbContext : DbContext
    {
        //public AppDbContext()
        //{

        //}

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CalculationHistory> CalculationHistories{ get; set; }
    }
}
