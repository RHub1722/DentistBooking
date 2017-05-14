using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities.Entities;
using Entities.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class EFContext : IdentityDbContext<ApplicationUser>, IDataContextAsync
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Pacient> Pacients { get; set; }
        public DbSet<Procedure> Procedures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Doctor>().Property(x => x.FirstName).HasMaxLength(100);
            modelBuilder.Entity<Doctor>().Property(x => x.SecondName).HasMaxLength(100);
            modelBuilder.Entity<Doctor>().Property(x => x.FirstName).IsRequired();
            modelBuilder.Entity<Doctor>().Property(x => x.SecondName).IsRequired();
            modelBuilder.Entity<Doctor>()
                .HasMany(x => x.Pacients)
                .WithOne(x => x.Doctor)
                .HasForeignKey(x => x.DoctorId);

     

            modelBuilder.Entity<Procedure>().Property(x => x.Type).HasMaxLength(100);
            modelBuilder.Entity<Procedure>().Property(x => x.Type).IsRequired();
   

            modelBuilder.Entity<Pacient>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Pacient>().Property(x => x.Email).IsRequired();
            modelBuilder.Entity<Pacient>().Property(x => x.Name).HasMaxLength(200);
            modelBuilder.Entity<Pacient>().Property(x => x.Email).HasMaxLength(100);
            modelBuilder.Entity<Pacient>().Property(x => x.Phone).HasMaxLength(100);
            modelBuilder.Entity<Pacient>()
                .HasOne(x => x.Doctor)
                .WithMany(x => x.Pacients)
                .HasForeignKey(x => x.DoctorId);

            modelBuilder.Entity<Pacient>()
                .HasOne(x => x.DoctorProcedure)
                .WithMany(x => x.Pacients)
                .HasForeignKey(x => x.DoctorProcedureId);
        }

        public async Task<int> SaveChangesAsync()
        {
            SyncObjectsStatePreCommit();
            var resp = await base.SaveChangesAsync(CancellationToken.None);
            SyncObjectsStatePostCommit();
            return resp;
        }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        public override int SaveChanges()
        {
            SyncObjectsStatePreCommit();
            var changes = base.SaveChanges();
            SyncObjectsStatePostCommit();
            return changes;
        }


        private void SyncObjectsStatePreCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
                dbEntityEntry.State = StateHelper.ConvertState(((IObjectState)dbEntityEntry.Entity).ObjectState);
        }

        public void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
                ((IObjectState)dbEntityEntry.Entity).ObjectState = StateHelper.ConvertState(dbEntityEntry.State);
        }
    }
}
