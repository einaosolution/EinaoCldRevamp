﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IPORevamp.Data.Entity.Interface;
using NACC.Data.UserManagement.Model;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.Email;
using EmailEngine.Base.Entities;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Event;
using IPORevamp.Data.Entities.Payment;
using IPORevamp.Data.Entities.Modules;
using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.TempModel;
using System.Data.Entity.ModelConfiguration.Configuration;
using IPORevamp.Data.Entities.LGAs;
using IPORevamp.Data.Entity.Interface.Entities.Auth;
using IPORevamp.Data.Entity.Interface.Entities.Role;
using IPORevamp.Data.Entities.Menus;

namespace IPORevamp.Data
{
    public class IPOContext : IdentityDbContext<ApplicationUser,ApplicationRole,int,ApplicationUserClaim,ApplicationUserRole,ApplicationUserLogin, IdentityRoleClaim<int>,IdentityUserToken<int>>
    {

        #region MyDBSetRegion
        
         public DbSet<Sector> Sector { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
        public DbSet<ApplicationUserLogin> ApplicationUserLogins { get; set; }
        public DbSet<EmailLog> EmailLog { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
     
        public DbSet<AuditTrail> AuditTrails { get; set; }
         public DbSet<BillLog> BillLogs{ get; set; }
        public DbSet<PaymentLog> PaymentLogs { get; set; }

       
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Data.Entities.Country.Country> Country { get; set; }
        public DbSet<UserVerificationTemp> UserVerificationTemp { get; set; }
        
        public DbSet<DSApplicationStatus> DSApplicationStatus { get; set; }
        public DbSet<Data.Entities.LGAs.LGA> LGAs { get; set; }
        
        public DbSet<DSApplicationStatus> dSApplicationStatuses { get; set; }
        public DbSet<State> States { get; set; }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<TMApplicationStatus> tMApplicationStatuses { get; set; }


       
        public virtual DbSet<RoleManager> RoleManager { get; set; }
 
       
        public virtual DbSet<LinkRolesMenus> LinkRolesMenus { get; set; }
        public virtual DbSet<MenuManager> MenuManager { get; set; }


        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer("Server=5.77.54.44; Database=IPORevampDB;User ID=TestUser;Password=Password12345;MultipleActiveResultSets=true;", b => b.MigrationsAssembly("IPORevamp.Data"));
        }
        public IPOContext(DbContextOptions<IPOContext> options) :base(options)
        {
           // Database.Migrate();
            //this.Confi
            //options.Configuration.ProxyCreationEnabled = false;

            //Configuration.LazyLoadingEnabled = false;
            // this.Database.Log = (e) => Debug.WriteLine(e);

        }                 

        #region MyDateCreated&DateUpdateRegion
        
        
        public override int SaveChanges()
        {
            try
            {
                Audits();
                return base.SaveChanges();
            }
            catch (DbUpdateException filterContext)
            {
                if (typeof(DbUpdateException) == filterContext.GetType())
                {

                    Debug.WriteLine("Property: {0}", filterContext.Message);
                    
                }

                throw;
            }
        }        


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
               Audits();
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException filterContext)
            {
                  Debug.WriteLine("Concurrency Error: {0}",filterContext.Message);
                return await Task.FromResult(0);

            }
            
        }

        protected override  void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          

            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<EventEventFeature>()
            //.HasKey(t => new { t.EventId, t.FeatureId });

            //modelBuilder.Entity<EventEventFeature>()
            //    .HasOne(pt => pt.EventInfo)
            //    .WithMany(p => p.Features)
            //    .HasForeignKey(pt => pt.FeatureId);





        }




        //add to manage Audit
        private void Audits()
        {

            var entities = ChangeTracker.Entries().Where(x => (x.Entity is IEntity || x.Entity is IAudit || x.Entity is EntityBase) && (x.State == EntityState.Added || x.State == EntityState.Modified));
            try
            {
                var userId = Environment.UserName;
                foreach (var entity in entities)
                {
                    if (entity.State == EntityState.Added)
                    {

                        if (entity.Entity is IAudit)
                        {
                            ((IAudit) entity.Entity).DateCreated = DateTime.Now;
                            ((IAudit) entity.Entity).CreatedBy = userId;
                            ((IAudit) entity.Entity).IsActive = true;
                            ((IAudit) entity.Entity).IsDeleted = false;
                            ((IAudit) entity.Entity).DateCreated = DateTime.Now;
                            ((IAudit) entity.Entity).IsActive = true;
                            ((IAudit) entity.Entity).IsDeleted = false;
                        }

                    }
                    else if (entity.State == EntityState.Modified)
                    {
                        if (entity.Entity is IAudit)
                        {
                            ((IAudit) entity.Entity).LastUpdateDate = DateTime.Now;
                            ((IAudit) entity.Entity).UpdatedBy = userId;
                            if (((IAudit) entity.Entity).IsDeleted)
                            {
                                ((IAudit) entity.Entity).DeletedBy = userId;

                            }
                        }
                    }


                }
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }
            
        }

        

        #endregion
    }
}
