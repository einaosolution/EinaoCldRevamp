﻿// <auto-generated />
using System;
using IPORevamp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IPORevamp.Data.Migrations
{
    [DbContext(typeof(IPOContext))]
    [Migration("20190508222550_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IPORevamp.Data.Entities.AuditTrail.AuditTrail", b =>
                {
                    b.Property<int>("AuditID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActionTaken");

                    b.Property<int>("ApplicationTypeId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DeletedBy");

                    b.Property<string>("Description");

                    b.Property<string>("Entity");

                    b.Property<int>("Id");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<byte[]>("RowVersion");

                    b.Property<string>("UpdatedBy");

                    b.Property<int>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("AuditID");

                    b.ToTable("AuditTrails");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Email.EmailLog", b =>
                {
                    b.Property<int>("EmailLogID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AttachmentLoc");

                    b.Property<string>("BCC");

                    b.Property<string>("CC");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateSent");

                    b.Property<DateTime>("DateToSend");

                    b.Property<string>("DeletedBy");

                    b.Property<bool>("HasAttachements");

                    b.Property<int>("Id");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("MailBody");

                    b.Property<string>("Receiver");

                    b.Property<byte[]>("RowVersion");

                    b.Property<bool>("SendImmediately");

                    b.Property<string>("Sender");

                    b.Property<int>("Status");

                    b.Property<string>("Subject");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("EmailLogID");

                    b.ToTable("EmailLog");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Email.EmailTemplate", b =>
                {
                    b.Property<int>("EmailTemplateID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DeletedBy");

                    b.Property<string>("EmailBody");

                    b.Property<string>("EmailName");

                    b.Property<string>("EmailSender");

                    b.Property<string>("EmailSubject");

                    b.Property<int?>("EmailTemplateType");

                    b.Property<int>("Id");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<byte[]>("RowVersion");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("EmailTemplateID");

                    b.ToTable("EmailTemplates");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Menus.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Badge");

                    b.Property<string>("BadgeClass");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DeletedBy");

                    b.Property<string>("Description");

                    b.Property<string>("Icon");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsExternalLink");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<int?>("MenuId");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<string>("Roles");

                    b.Property<byte[]>("RowVersion");

                    b.Property<string>("Title");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Menus.RoleMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DeletedBy");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<int?>("MenuId");

                    b.Property<int?>("RoleId");

                    b.Property<byte[]>("RowVersion");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleMenu");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Payment.BillLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<string>("BillRefenceNo");

                    b.Property<int>("BillStatus");

                    b.Property<int>("BillType");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DatePaid");

                    b.Property<string>("DeletedBy");

                    b.Property<int?>("EntityId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<int>("PaymentMethod");

                    b.Property<byte[]>("RowVersion");

                    b.Property<string>("UpdatedBy");

                    b.Property<int>("UserId");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BillLogs");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Payment.PaymentLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<int>("BillId");

                    b.Property<int?>("BillLogId");

                    b.Property<string>("CardNumber");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DeletedBy");

                    b.Property<int?>("EntityId");

                    b.Property<decimal>("FinalAmount");

                    b.Property<decimal>("GateWayAmount");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("MembershipNo");

                    b.Property<int>("PaymenType");

                    b.Property<decimal>("PaymentCharges");

                    b.Property<DateTime?>("PaymentDate");

                    b.Property<string>("PaymentReferenceNo");

                    b.Property<int>("PaymentStatus");

                    b.Property<string>("ResponseCode");

                    b.Property<string>("ResponseDescription");

                    b.Property<string>("ResponsePayload");

                    b.Property<byte[]>("RowVersion");

                    b.Property<decimal>("TechFee");

                    b.Property<string>("UpdatedBy");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BillLogId");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentLogs");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Setting.AccountType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName");

                    b.Property<string>("AccountTypeCode");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DeletedBy");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<byte[]>("RowVersion");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("AccountTypes");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Setting.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DeletedBy");

                    b.Property<int>("EnableForOtherCountry");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("Name");

                    b.Property<byte[]>("RowVersion");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Setting.DSApplicationStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DeletedBy");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<int>("RoleId");

                    b.Property<byte[]>("RowVersion");

                    b.Property<string>("StatusDescription");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("DSApplicationStatus");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Setting.lga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DeletedBy");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LGAName");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<byte[]>("RowVersion");

                    b.Property<int?>("StateId");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("LGAs");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Setting.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DeletedBy");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ItemName");

                    b.Property<string>("ItemValue");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<byte[]>("RowVersion");

                    b.Property<string>("SettingCode");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Setting.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DeletedBy");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<byte[]>("RowVersion");

                    b.Property<string>("StateName");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Setting.TMApplicationStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DeletedBy");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<int>("RoleId");

                    b.Property<byte[]>("RowVersion");

                    b.Property<string>("StatusDescription");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("tMApplicationStatuses");
                });

            modelBuilder.Entity("IPORevamp.Data.UserManagement.Model.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDefault");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("IPORevamp.Data.UserManagement.Model.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Bio");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("CountryCode");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DeletedBy");

                    b.Property<DateTime>("DoB");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("EmployerName");

                    b.Property<string>("FaceBook");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<string>("GooglePlus");

                    b.Property<string>("Instagram");

                    b.Property<string>("Interests");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleName");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("Nationality");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("Occupation");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("ProfilePicLoc");

                    b.Property<string>("ResidentialAddress");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("Title");

                    b.Property<string>("Twitter");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UpdatedBy");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("IPORevamp.Data.UserManagement.Model.ApplicationUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("IPORevamp.Data.UserManagement.Model.ApplicationUserLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("IPORevamp.Data.UserManagement.Model.ApplicationUserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Menus.Menu", b =>
                {
                    b.HasOne("IPORevamp.Data.Entities.Menus.Menu")
                        .WithMany("Submenu")
                        .HasForeignKey("MenuId");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Menus.RoleMenu", b =>
                {
                    b.HasOne("IPORevamp.Data.Entities.Menus.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId");

                    b.HasOne("IPORevamp.Data.UserManagement.Model.ApplicationRole", "Role")
                        .WithMany("Menus")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Payment.BillLog", b =>
                {
                    b.HasOne("IPORevamp.Data.UserManagement.Model.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Payment.PaymentLog", b =>
                {
                    b.HasOne("IPORevamp.Data.Entities.Payment.BillLog")
                        .WithMany("PaymentDetails")
                        .HasForeignKey("BillLogId");

                    b.HasOne("IPORevamp.Data.UserManagement.Model.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Setting.lga", b =>
                {
                    b.HasOne("IPORevamp.Data.Entities.Setting.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("IPORevamp.Data.Entities.Setting.State", b =>
                {
                    b.HasOne("IPORevamp.Data.Entities.Setting.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("IPORevamp.Data.UserManagement.Model.ApplicationUserClaim", b =>
                {
                    b.HasOne("IPORevamp.Data.UserManagement.Model.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IPORevamp.Data.UserManagement.Model.ApplicationUserLogin", b =>
                {
                    b.HasOne("IPORevamp.Data.UserManagement.Model.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IPORevamp.Data.UserManagement.Model.ApplicationUserRole", b =>
                {
                    b.HasOne("IPORevamp.Data.UserManagement.Model.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IPORevamp.Data.UserManagement.Model.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("IPORevamp.Data.UserManagement.Model.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("IPORevamp.Data.UserManagement.Model.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
