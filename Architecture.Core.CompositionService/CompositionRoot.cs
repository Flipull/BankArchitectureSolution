using Architecture.BusinessLogic.BankDTOs;
using Architecture.BusinessLogic.BankFactories;
using Architecture.BusinessLogic.BankLogics;
using Architecture.BusinessLogic.BankLogics.Infra;
using Architecture.BusinessLogic.BankMappers;
using Architecture.BusinessLogic.BankSROs;
using Architecture.BusinessLogic.CustomerDTOs;
using Architecture.BusinessLogic.CustomerFactories;
using Architecture.BusinessLogic.CustomerLogics;
using Architecture.BusinessLogic.CustomerLogics.Infra;
using Architecture.BusinessLogic.CustomerMappers;
using Architecture.BusinessLogic.CustomerSROs;
using Architecture.DataAccess.BankEntities;
using Architecture.DataAccess.BankFactories;
using Architecture.DataAccess.BankRepositories;
using Architecture.DataAccess.BankRepositories.Infra;
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
        //order of inserting is irrelevant for workings;
        //but useful for understanding code
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

            //I WANT collections of these per component per layer, plz?
            //calling CustomerComposite.ConfigureServices(services)
            //so each layer can register itself, by my (WepApp's) request

            //register Entities
            services.AddTransient<Customer>();
            services.AddTransient<BankAccount>();
            services.AddTransient<BankTransaction>();
            //register EntityFactories
            services.AddSingleton<CustomerFactory>();
            services.AddSingleton<BankAccountFactory>();
            services.AddSingleton<BankTransactionFactory>();
            //register Repositories
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IBankAccountRepository, BankAccountRepository>();
            services.AddTransient<IBankTransactionRepository, BankTransactionRepository>();

            //register SROs
            services.AddTransient<CustomerCreateSRO>();
            services.AddTransient<CustomerUpdateSRO>();
            services.AddTransient<BankTransactionExecuteSRO>();
            services.AddTransient<BankTransactionSearchSRO>();
            //register DTOs
            services.AddTransient<CustomerDTO>();
            services.AddTransient<BankAccountDTO>();
            services.AddTransient<BankTransactionDTO>();
            services.AddTransient<BankTransactionSearchResultDTO>();

            //register Factories
            services.AddSingleton<CustomerDTOFactory>();
            services.AddSingleton<BankAccountDTOFactory>();
            services.AddSingleton<BankTransactionSearchSROFactory>();
            services.AddSingleton<BankTransactionDTOFactory>();
            services.AddSingleton<BankTransactionSearchResultDTOFactory>();
            //register BusinessLogic
            services.AddSingleton<ICustomerLogic, CustomerLogic>();
            services.AddSingleton<IBankAccountLogic, BankAccountLogic>();
            services.AddSingleton<IBankTransactionLogic, BankTransactionLogic>();

            //register SROToEntityMappers
            services.AddSingleton<CustomerCreateEntityMapper>();
            services.AddSingleton<CustomerUpdateEntityMapper>();

            //register DTOToEntityMappers; probably never used
            //as clients talk with SROs only
            services.AddSingleton<CustomerEntityMapper>();
            services.AddSingleton<BankAccountEntityMapper>();
            services.AddSingleton<BankTransactionDepositsSearchResultDTOMapper>();
            services.AddSingleton<BankTransactionWithdrawalsSearchResultDTOMapper>();
            services.AddSingleton<BankTransactionExecuteEntityMapper>();
            //register EntityToDTOMappers
            services.AddSingleton<CustomerDTOMapper>();
            services.AddSingleton<BankAccountDTOMapper>();
            services.AddSingleton<BankTransactionDTOMapper>();

        }
    }
}
