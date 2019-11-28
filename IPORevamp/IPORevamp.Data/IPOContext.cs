using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entities.AuditTrail;

using IPORevamp.Data.Entities.Payment;
using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.TempModel;
using IPORevamp.Data.Entity.Interface.Entities.Role;
using IPORevamp.Data.Entities.Menus;
using IPORevamp.Data.Entities.DSApplicationStatus;
using IPORevamp.Data.Entities.TMApplicationStatus;
using IPORevamp.Data.Entities.PTApplicationStatus;
using IPORevamp.Data.Entities.Fee;
using IPORevamp.Data.Entity.Interface.Entities.Product;
using IPORevamp.Data.Entity.Interface.Entities.Department;
using IPORevamp.Data.Entity.Interface.Entities.Ministry;
using IPORevamp.Data.Entity.Interface.Entities.Unit;
using IPORevamp.Data.Entity.Interface.Entities.Sms;
using IPORevamp.Data.Entity.Interface.Entities.RemitaPayment;
using IPORevamp.Data.Entity.Interface.Entities.FeeDetail;
using IPORevamp.Data.Entity.Interface.Entities.Twallet;
using IPORevamp.Data.Entity.Interface.Entities.Payment;
using IPORevamp.Data.Entity.Interface.ApplicationType;
using IPORevamp.Data.Entity.Interface.Entities.Pwallet;
using IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.Comments;
using IPORevamp.Data.Entity.Interface.Entities.MarkInfo;
using IPORevamp.Data.Entity.Interface.PreliminarySearch;
using IPORevamp.Data.Entity.Interface.Entities.National_Class;
using Microsoft.Extensions.Configuration;
using IPORevamp.Data.Entity.Interface.Entities.TrademarkType;
using IPORevamp.Data.Entity.Interface.Entities.TrademarkLogo;
using IPORevamp.Data.Entity.Interface.Entities.Opposition;
using IPORevamp.Data.Entity.Interface.Entities.Certificate;
using IPORevamp.Data.Entity.Interface.Entities.Recordal;
using IPORevamp.Data.Entity.Interface.Entities.Search;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Data.Entity.Interface.Entities.PatentType;
using IPORevamp.Data.Entity.Interface.Entities.PatentInformation;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.PatentAssignment;
using IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation;
using IPORevamp.Data.Entity.Interface.Entities.PatentInvention;
using IPORevamp.Data.Entity.Interface.Entities.AddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DelegateJob;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;
using IPORevamp.Data.Entity.Interface.Entities.DesignType;
using IPORevamp.Data.Entity.Interface.Entities.DesignInformation;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.DesignAssigment;
using IPORevamp.Data.Entity.Interface.Entities.DesignInvention;
using IPORevamp.Data.Entity.Interface.Entities.DesignPriority;
using IPORevamp.Data.Entity.Interface.Entities.DesignAddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignCoApplicant;
using IPORevamp.Data.Entity.Interface.Entities.Setting;
using IPORevamp.Data.Entity.Interface.Entities.UserView;

namespace IPORevamp.Data
{
    public class IPOContext : IdentityDbContext<ApplicationUser,ApplicationRole,int,ApplicationUserClaim,ApplicationUserRole,ApplicationUserLogin, IdentityRoleClaim<int>,IdentityUserToken<int>>
    {

        #region MyDBSetRegion

        public DbSet<Sector> Sector { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<SmsLog> SmsLog { get; set; }
        public DbSet<UserView> UserView { get; set; }
        public DbSet<MigratedUsers> MigratedUsers { get; set; }
        
        public DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
        public DbSet<ApplicationUserLogin> ApplicationUserLogins { get; set; }
        public DbSet<DesignPublication> DesignPublication { get; set; }

        
        public DbSet<EmailLog> EmailLog { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<TrademarkType> TrademarkType { get; set; }

        public DbSet<DesignDataCount> DesignDataCount { get; set; }

        

        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<CounterOpposition> CounterOpposition { get; set; }
        public DbSet<BillLog> BillLogs { get; set; }
        public DbSet<PaymentLog> PaymentLogs { get; set; }
        public DbSet<PaymentReport> PaymentReport { get; set; }


        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<RecordalMerger> RecordalMerger { get; set; }

        public DbSet<RecordalDesignRenewal> RecordalDesignRenewal { get; set; }

        
        public DbSet<Ministry> Ministry { get; set; }
        public DbSet<PreviousComments> PreviousComments { get; set; }
        public DbSet<Units> Units { get; set; }

        public DbSet<FeeList> FeeList { get; set; }
        public DbSet<MarkInformation> MarkInformation{ get; set; }
        public DbSet<PreliminarySearch> PreliminarySearch { get; set; }

      
        public DbSet<TrademarkApplicationHistory> TrademarkApplicationHistory { get; set; }

        public DbSet<TrademarkComments> TrademarkComments { get; set; }
        public DbSet<Twallet> TradeMarkwallet { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<DesignDataResult> DesignDataResult { get; set; }
        public DbSet<PayCertificate> PayCertificate { get; set; }
        public DbSet<ApplicationType> ApplicationType { get; set; }
        public DbSet<NationalClass> NationalClass { get; set; }


        public DbSet<Application> Application { get; set; }

        public DbSet<TrademarkLogo> TrademarkLogo { get; set; }
        public DbSet<RecordalRenewal> RecordalRenewal { get; set; }
        public DbSet<NoticeOfOpposition> NoticeOfOpposition { get; set; }
        public DbSet<DataResult> DataResult { get; set; }
        public DbSet<PatentDataResult> PatentDataResult { get; set; }

        
        public DbSet<Data.Entities.Country.Country> Country { get; set; }
        public DbSet<UserVerificationTemp> UserVerificationTemp { get; set; }

        public DbSet<DSApplicationStatus> DSApplicationStatus { get; set; }
        public DbSet<DelegateJob> DelegateJob { get; set; }
        public DbSet<DelegateDesignJob> DelegateDesignJob { get; set; }

        
        public DbSet<Product> Product { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Data.Entities.LGAs.LGA> LGAs { get; set; }
        public DbSet<IPORevamp.Data.Entity.Interface.Entities.Batch.PublicationBatch> PublicationBatch { get; set; }

        public DbSet<TMApplicationStatus> TMApplicationStatus { get; set; }
        public DbSet<State> States { get; set; }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<PTApplicationStatus> PTApplicationStatus { get; set; }



        public virtual DbSet<RoleManager> RoleManager { get; set; }


        public virtual DbSet<LinkRolesMenus> LinkRolesMenus { get; set; }
        public virtual DbSet<MenuManager> MenuManager { get; set; }



        #endregion

        #region Design
       
        public DbSet<DesignApplication> DesignApplication { get; set; }
        public DbSet<DesignType> DesignType { get; set; }
        public DbSet<DesignInformation> DesignInformation { get; set; }
        public DbSet<DesignApplicationHistory> DesignApplicationHistory { get; set; }
        public DbSet<DesignAssignment> DesignAssignment { get; set; }
        public DbSet<DesignCoApplicant> DesignCoApplicant { get; set; }

        

        public DbSet<DesignInvention> DesignInvention { get; set; }

        public DbSet<DesignPriority> DesignPriority { get; set; }

        public DbSet<DesignAddressOfService> DesignAddressOfService { get; set; }

        








        #endregion

        #region Patent
        public DbSet<PatentApplication> PatentApplication { get; set; }
        public DbSet<PatentType> PatentType { get; set; }
        public DbSet<PatentInformation> PatentInformation { get; set; }
        public DbSet<PatentApplicationHistory> PatentApplicationHistory { get; set; }
        public DbSet<PatentAssignment> PatentAssignment { get; set; }
        public DbSet<PatentPriorityInformation> PatentPriorityInformation { get; set; }
        public DbSet<AddressOfService> AddressOfService { get; set; }

        

        public DbSet<PatentInvention> PatentInvention { get; set; }

        

        #endregion

        #region Remitta Payment
        public virtual DbSet<RemitaPayment> RemitaPayments { get; set; }
        public virtual DbSet<LineItem> LineItems { get; set; }
        public virtual DbSet<CustomField> CustomFields { get; set; }
        public DbSet<RemitaBankCode> RemitaBankCode { get; set; }

        public DbSet<RemitaAccountSplit> RemitaAccountSplit { get; set; }

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
