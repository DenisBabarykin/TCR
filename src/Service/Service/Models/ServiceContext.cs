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

        public System.Data.Entity.DbSet<Service.Models.VSegment> VSegments { get; set; }
        public System.Data.Entity.DbSet<Service.Models.DSegment> DSegments { get; set; }
        public System.Data.Entity.DbSet<Service.Models.JSegment> JSegments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VSegment>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("VSegments");
            });
            modelBuilder.Entity<DSegment>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("DSegments");
            });
            modelBuilder.Entity<JSegment>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("JSegments");
            });

            modelBuilder.Entity<VSegment>()
                        .HasMany(v => v.Receptors)
                        .WithMany(r => r.VSegments)
                        .Map(v =>
                        {
                            v.ToTable("VSegmentsReceptors");
                            v.MapLeftKey("SegmentId");
                            v.MapRightKey("ReceptorId");
                        });
            modelBuilder.Entity<DSegment>()
                        .HasMany(v => v.Receptors)
                        .WithMany(r => r.DSegments)
                        .Map(v =>
                        {
                            v.ToTable("DSegmentsReceptors");
                            v.MapLeftKey("SegmentId");
                            v.MapRightKey("ReceptorId");
                        });
            modelBuilder.Entity<JSegment>()
                        .HasMany(v => v.Receptors)
                        .WithMany(r => r.JSegments)
                        .Map(v =>
                        {
                            v.ToTable("JSegmentsReceptors");
                            v.MapLeftKey("SegmentId");
                            v.MapRightKey("ReceptorId");
                        });
        }
    
    }
}
