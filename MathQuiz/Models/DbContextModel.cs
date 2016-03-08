using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MathQuiz.Models
{
    public class DbContextModel : DbContext
    {
        public DbContextModel() : base("name=CMSC495Entities1") {
            Database.SetInitializer<DbContextModel>(null);
        }

        public virtual DbSet<Problem> Problems { get; set; }
        public virtual DbSet<UserTest> UserTests { get; set; }
    }
}