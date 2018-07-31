using Microsoft.AspNet.Identity.EntityFramework;
using Store.Domain;
using Store.DomainN;
using Store.Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Text;

namespace ClassLibrary1
{
    public class User : IdentityUser, IClothAppUser
    {


    }


    public class ClothDbContext : IdentityDbContext<User>
    {

        //public ClothDbContext()
        //: base("name=MyConnection")
        //{
        //    Database.SetInitializer<ClothDbContext>(new MigrateDatabaseToLatestVersion<ClothDbContext, Configuration>());
        //    base.Configuration.ProxyCreationEnabled = false;
        //}



        public DbSet<Cloth> Clothes { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //   // modelBuilder.Configurations.Add(new MyConfiguration());

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
