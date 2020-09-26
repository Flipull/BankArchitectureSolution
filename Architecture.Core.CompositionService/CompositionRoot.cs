using Architecture.BusinessLogic.CustomerDTOs;
using Architecture.BusinessLogic.CustomerLogics;
using Architecture.BusinessLogic.CustomerLogics.Infra;
using Architecture.BusinessLogic.CustomerMappers;
using Architecture.Core.MappingService;
using Architecture.DataAccess.CustomerEntities;
using Architecture.DataAccess.CustomerRepositories;
using Architecture.DataAccess.CustomerRepositories.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture.Core.CompositionService
{
    static class CompositionRoot
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddTransient<DbContext>(
                        (srvprov) => new DbContext(
                                new DbContextOptionsBuilder<DbContext>()
                                .UseSqlServer( configuration.GetConnectionString("BankDatabase") )
                                .UseModel(null)
                                .Options
                            )
                    );

            services.AddSingleton<IMapperService, MapperService>(
                        (srvprov) =>
                        {
                            var mappings = new MapperServicecollection();
                            mappings.RegisterMapping<Customer, CustomerDTO>(
                                    () => new CustomerDTOMapper()
                                );
                            mappings.RegisterMapping<CustomerDTO, Customer>(
                                    () => new CustomerEntityMapper()
                                );
                            return new MapperService(mappings);
                        }
                    );

            services.AddTransient<Customer>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<ICustomerLogic, CustomerLogic>();
            services.AddTransient<CustomerDTO>();
        }
    }
}
