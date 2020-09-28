using Architecture.BusinessLogic.CustomerDTOFactories;
using Architecture.BusinessLogic.CustomerDTOs;
using Architecture.BusinessLogic.CustomerLogics;
using Architecture.BusinessLogic.CustomerLogics.Infra;
using Architecture.BusinessLogic.CustomerMappers;
using Architecture.DataAccess.CustomerEntities;
using Architecture.DataAccess.CustomerFactories;
using Architecture.DataAccess.CustomerRepositories;
using Architecture.DataAccess.CustomerRepositories.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.Core.CompositionService
{
    public static class CompositionRoot
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<DbContext>(
                        (srvprov) =>
                        {
                            //todo: build a MutableModel without using an ugly DbContext derivative

                            //use loaded config and above build model to configure EF
                            return new CompositionModel(
                                    new DbContextOptionsBuilder<DbContext>()
                                        .UseSqlServer(configuration.GetConnectionString("BankDatabase"))
                                        //.UseModel(null)//supresses DbContext.onModelCreating in favor of custom code
                                        .Options
                                );
                        }
                    );

            //register Entities
            services.AddTransient<Customer>();
            //register EntityFactories
            services.AddTransient<CustomerFactory>();
            //register Repositories
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            //register DTOs
            services.AddTransient<CustomerDTO>();
            //register DTOFactories
            services.AddTransient<CustomerDTOFactory>();
            //register BusinessLogic
            services.AddSingleton<ICustomerLogic, CustomerLogic>();

            //register DTOToEntityMappers
            services.AddSingleton<CustomerEntityDTOMapper>();
            //register EntityToDTOMappers
            services.AddSingleton<CustomerDTOEntityMapper>();

        }
    }
}
