using Autofac;
using WordGame.Application.Services;
using WordGame.Core.Repositories.Base.Interfaces.RepositoryProvider;
using WordGame.Core.Repositories.CustomRepositories;
using WordGame.Core.Services;
using WordGame.Core.Services.Base;
using WordGame.Core.UnitOfWorks;
using WordGame.Infrastructure.Data;
using WordGame.Infrastructure.Repository;
using WordGame.Infrastructure.UnitOfWork;

namespace WordGame.WEB
{
	public class AutofacModule: Module
	{
        protected override void Load(ContainerBuilder builder)
        {


            // Other Lifetime
            // Transient
            builder.RegisterType<WordService>().As<IWordService>()
                .InstancePerLifetimeScope();

            // Scoped
            builder.RegisterType<GrammerService>().As<IGrammerService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OptionService>().As<IOptionsService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StatisticService>().As<IStatisticService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<WordRepository>().As<IWordRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GrammerRepository>().As<IGrammerRepository>().InstancePerLifetimeScope();


            // Scan an assembly for components
            //builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
            //       .Where(t => t.Name.EndsWith("Service"))
            //       .AsImplementedInterfaces();
        }
    }
}
