using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MobileStore_CodeFirst.Models.MobileEDM
{
    public class MobileContext : DbContext
    {
        public MobileContext() : base("MobileContext") { }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<MobileModel> Models { get; set; }
        public DbSet<OPSystem> OperatingSystems { get; set; }
    }
}