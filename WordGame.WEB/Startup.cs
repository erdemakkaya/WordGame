using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using WordGame.Application.Services;
using WordGame.Core.Repositories;
using WordGame.Infrastructure.Data;
using WordGame.Infrastructure.Repository;
using WordGame.Core.Repositories.Base.Interfaces;
using WordGame.Core.Services;
using WordGame.Infrastructure.Repository.Base;
using WordGame.Core.UnitOfWorks;
using WordGame.Infrastructure.UnitOfWork;
using AutoMapper;
using WordGame.Application.Mapper;
using FluentValidation;
using WordGame.Application.Validators;
using Autofac;
using Microsoft.AspNetCore.Http;
using Autofac.Extensions.DependencyInjection;
using WordGame.Application.ConfigurationModules;

namespace WordGame.WEB
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		public IContainer ApplicationContainer { get; private set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddHealthChecks();
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "WordGame.WEB", Version = "v1" });
			});
			services.AddCors(opt =>
			{
				opt.AddPolicy("CorsPolicy", policy =>
				{
					policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000").AllowCredentials();
				});
			});

			//#region AutoMapperSection
			////Auto mapper'ı ekliyoruz
			//var mappingConfig = new MapperConfiguration(mc =>
			//{
			//	mc.AddProfile(new WordGameDtoMapper());
			//});
			//IMapper mapper = mappingConfig.CreateMapper();
			//services.AddSingleton(mapper);

			//#endregion
			services.AddValidatorsFromAssemblyContaining<WordValidator>();


		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WordGame.WEB v1"));
			}

			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

			app.UseCors("CorsPolicy");

			app.UseRouting();

			app.UseAuthorization();

			app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
			{

				ResponseWriter =async (context,report)  =>
				{
					await context.Response.WriteAsync("OK");
				}

			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			#region WAY-1 (Autofac Module)

			// Add modules registrations.

			builder.RegisterModule(new ContainerConfigurations());
			//builder.RegisterModule(new MyAutofacModule2());
			//builder.RegisterModule(new MyAutofacModule3());

			#endregion

			#region WAY-2 (Direct Registration)

			// Add services registrations.

			//builder.RegisterType<MyService>().As<IService>();

			#endregion
		}
	}
}
