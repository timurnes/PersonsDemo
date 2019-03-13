using Autofac;
using Autofac.Features.ResolveAnything;
using Model;
using Persistence;

namespace PersonDemo {
    public class AutofacModule : Module {

        protected override void Load(ContainerBuilder builder) {

            builder
                .RegisterType<UnitOfWork>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<EfPersons>()
                .As<IPersons>()
                .InstancePerLifetimeScope();

            builder.RegisterSource(
                new AnyConcreteTypeNotAlreadyRegisteredSource()
                    .WithRegistrationsAs(b => b.InstancePerLifetimeScope()));
        }
    }
}
