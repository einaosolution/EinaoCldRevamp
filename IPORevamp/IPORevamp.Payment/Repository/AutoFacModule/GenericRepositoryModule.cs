using Autofac;
using EmailEngine.Base.Entities;
using EmailEngine.Repository.Base;
using EmailEngine.Repository.EmailRepository;
using EmailEngine.Repository.Interface;
using IPORevamp.Payment.Base.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Payment.Repository.AutoFacModule
{
    public class GenericRepositoryModule<TEntity, TDbContext> : Module where TEntity : class,IGenericEntity where TDbContext : DbContext
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<GenericRepository<TEntity, TDbContext>>()
                .As<IGenericRepository<TEntity>>()
                .InstancePerLifetimeScope();                       
            
            builder.RegisterAssemblyTypes(typeof(IAutoDependencyRegister).Assembly)
                .AssignableTo<IAutoDependencyRegister>()
                .As<IAutoDependencyRegister>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }


    public class BillRepositoryModule<TBill, TPayment, TUser, TUserKey, TDbContext> : Module where TBill : Bill<TUser, TUserKey, TPayment> where TPayment : PaymentLog<TUser, TUserKey> where TUser: IdentityUser<TUserKey> where TUserKey: IEquatable<TUserKey> where TDbContext:DbContext
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GenericRepository<TBill, TDbContext>>()
                .As<IGenericRepository<TBill>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GenericRepository<TPayment, TDbContext>>()
                .As<IGenericRepository<TPayment>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Billing<TBill,TPayment,TUser, TUserKey>>()
                .As<IBilling<TBill, TPayment, TUser, TUserKey>>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IAutoDependencyRegister).Assembly)
                .AssignableTo<IAutoDependencyRegister>()
                .As<IAutoDependencyRegister>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            base.Load(builder);
        }
    } 

    
}
