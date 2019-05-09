using Autofac;
using IPORevamp.Repository.CoreRepository;
using IPORevamp.Repository.Interface;
using IPORevamp.Data;
using IPORevamp.Repository.Interface;

namespace IPORevamp.Repository.AutoFacModule
{
    public class RepositoryModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterType<IPOContext>().InstancePerLifetimeScope();
           

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
            //builder.RegisterType<NACCPaymentJobRepository>().As<INACCPaymentJobRepository>().InstancePerMatchingLifetimeScope();
            //builder.RegisterType<EmailJobRepositorycs>().As<IEmailJobRepository>().InstancePerMatchingLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IAutoDependencyRegister).Assembly)
                .AssignableTo<IAutoDependencyRegister>()
                .As<IAutoDependencyRegister>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}