using FlightFolio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightFolio.Infrastructure
{
    public class FlightFolioContext :DbContext
    {
        
        public FlightFolioContext(DbContextOptions<FlightFolioContext> options) : base(options)
        {

        }

        public FlightFolioContext(): this(new DbContextOptions<FlightFolioContext>()) {
            
        }

        public DbSet<Aeroplane> Aeroplanes { get; set; }

        public DbSet<AeroplaneFolio> AeroplaneFolios   { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
