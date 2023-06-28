using Autofac;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Application.Mapper;
using WordGame.Application.Services;
using WordGame.Core.Context;
using WordGame.Core.Services;
using WordGame.Core.UnitOfWorks;
using WordGame.Infrastructure.Data;
using WordGame.Infrastructure.Repository;
using WordGame.Infrastructure.UnitOfWork;

namespace WordGame.Application.ConfigurationModules
{
	public class ContainerConfigurations : Module
	{
        protected override void Load(ContainerBuilder builder)
        {

           
            //builder.Register(c =>
            //{
            //    var config = c.Resolve<IConfiguration>();

            //    var opt = new DbContextOptionsBuilder<WordGameContext>();
            //    opt.UseNpgsql(config.GetConnectionString("DefaultConnection"));

            //return new WordGameContext(opt.Options);
            //}).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<WordGameContext>().As<BaseContext>();

            builder.RegisterType<UnitOfWork>().As<IUnitofWork>().InstancePerLifetimeScope();
            #region AutoMapperSection
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WordGameDtoMapper());
            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
        .As<IMapper>()
        .InstancePerLifetimeScope();
			#endregion

			#region Services Section
			// Scan an assembly for services
			builder.RegisterAssemblyTypes(typeof(WordService).Assembly)
				   .Where(t => t.Name.EndsWith("Service"))
				   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
			#endregion

			#region Repository Section

			// Scan an assembly for repository
			builder.RegisterAssemblyTypes(typeof(GrammerRepository).Assembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
			#endregion
		}
	}
}
