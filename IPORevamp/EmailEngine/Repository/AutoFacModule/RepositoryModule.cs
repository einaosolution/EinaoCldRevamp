using Autofac;
using EmailEngine.Base.Entities;
using EmailEngine.Repository.Base;
using EmailEngine.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EmailEngine.Repository.AutoFacModule
{
    public class RepositoryModule<TIPOEmailLog, TIPOEmailTemplate, TDbContext>:Module where TIPOEmailLog : IPOEmailLog where TIPOEmailTemplate : IPOEmailTemplate where TDbContext :DbContext
    {
        protected override void Load(ContainerBuilder builder)
        {



            builder.RegisterType<Repository<TIPOEmailLog, TIPOEmailTemplate, TDbContext>>()
                .As<IRepository<TIPOEmailLog, TIPOEmailTemplate>>()
                .InstancePerLifetimeScope();                        

            builder.RegisterAssemblyTypes(typeof(IAutoDependencyRegister).Assembly)
                .AssignableTo<IAutoDependencyRegister>()
                .As<IAutoDependencyRegister>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}