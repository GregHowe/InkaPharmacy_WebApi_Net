﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EnterprisePatterns.Api.Common.Infrastructure.Persistence.NHibernate;
using EnterprisePatterns.Api.BankAccounts.Domain.Repository;
using EnterprisePatterns.Api.Accounts.Infrastructure.Persistence.NHibernate.Repository;
using EnterprisePatterns.Api.Customers.Domain.Repository;
using EnterprisePatterns.Api.Customers.Infrastructure.Persistence.NHibernate.Repository;
using AutoMapper;
using EnterprisePatterns.Api.BankAccounts.Application.Assembler;
using EnterprisePatterns.Api.Movies.Domain.Repository;
using EnterprisePatterns.Api.Movies.Infrastructure.Persistence.NHibernate.Repository;
using EnterprisePatterns.Api.Movies.Application.Assembler;
using EnterprisePatterns.Api.Common.Application;
using EnterprisePatterns.Api.Security.Application.Assembler;
using EnterprisePatterns.Api.Security.Domain.Repository;
using EnterprisePatterns.Api.Customers.Application.Assembler;
using EnterprisePatterns.Api.Products.Application.Assembler;
using EnterprisePatterns.Api.Product.Domain.Repository;
using EnterprisePatterns.Api.Employee.Domain.Repository;
using EnterprisePatterns.Api.Employee.Infrastructure.Persistence.NHibernate.Repository;

namespace EnterprisePatterns.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton(new SessionFactory(Environment.GetEnvironmentVariable("InkaPharmacyBD")));
            var serviceProvider = services.BuildServiceProvider();
            var mapper = serviceProvider.GetService<IMapper>();

            services.AddSingleton(new ProductAssembler(mapper));
            services.AddSingleton(new CustomerAssembler(mapper));
            services.AddSingleton(new EmployeeAssembler(mapper));
            services.AddSingleton(new BankAccountCreateAssembler(mapper));
            services.AddSingleton(new MovieAssembler(mapper));

            services.AddScoped<IUnitOfWork, UnitOfWorkNHibernate>();

            services.AddTransient<IProductRepository, ProductNHibernateRepository>((ctx) =>
            {
                IUnitOfWork unitOfWork = ctx.GetService<IUnitOfWork>();
                return new ProductNHibernateRepository((UnitOfWorkNHibernate)unitOfWork);
            });

            services.AddTransient<IEmployeeRepository, EmployeeNHibernateRepository>((ctx) =>
            {
                IUnitOfWork unitOfWork = ctx.GetService<IUnitOfWork>();
                return new EmployeeNHibernateRepository((UnitOfWorkNHibernate)unitOfWork);
            });

            services.AddTransient<ISecurityRepository, SecurityNHibernateRepository>((ctx) =>
            {
                IUnitOfWork unitOfWork = ctx.GetService<IUnitOfWork>();
                return new SecurityNHibernateRepository((UnitOfWorkNHibernate)unitOfWork);
            });


            services.AddTransient<IBankAccountRepository, BankAccountNHibernateRepository>((ctx) =>
            {
                IUnitOfWork unitOfWork = ctx.GetService<IUnitOfWork>();
                return new BankAccountNHibernateRepository((UnitOfWorkNHibernate)unitOfWork);
            });

            services.AddTransient<ICustomerRepository, CustomerCustomerNHibernateRepository>((ctx) =>
            {
                IUnitOfWork unitOfWork = ctx.GetService<IUnitOfWork>();
                return new CustomerCustomerNHibernateRepository((UnitOfWorkNHibernate)unitOfWork);
            });

            services.AddTransient<IMovieRepository, MovieNHibernateRepository>((ctx) =>
            {
                IUnitOfWork unitOfWork = ctx.GetService<IUnitOfWork>();
                return new MovieNHibernateRepository((UnitOfWorkNHibernate)unitOfWork);
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
