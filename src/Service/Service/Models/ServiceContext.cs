using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class ServiceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ServiceContext() : base("name=ServiceContext")
        {
        }

        public System.Data.Entity.DbSet<Service.Models.Receptor> Receptors { get; set; }

        public System.Data.Entity.DbSet<Service.Models.Person> People { get; set; }

        public System.Data.Entity.DbSet<Service.Models.Segment> Segments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Receptor>()
                        .HasMany(r => r.VSegments)
                        .WithMany(s => s.Receptors)
                        .Map(v =>
                        {
                            v.ToTable("VSegments");
                            v.MapLeftKey("ReceptorId");
                            v.MapRightKey("SegmentId");
                        });

            modelBuilder.Entity<Receptor>()
                        .HasMany(r => r.DSegments)
                        .WithMany(s => s.Receptors)
                        .Map(v =>
                        {
                            v.ToTable("DSegments");
                            v.MapLeftKey("ReceptorId");
                            v.MapRightKey("SegmentId");
                        });

            modelBuilder.Entity<Receptor>()
                        .HasMany(r => r.JSegments)
                        .WithMany(s => s.Receptors)
                        .Map(v =>
                        {
                            v.ToTable("JSegments");
                            v.MapLeftKey("ReceptorId");
                            v.MapRightKey("SegmentId");
                        });
        }
    
    }
}
