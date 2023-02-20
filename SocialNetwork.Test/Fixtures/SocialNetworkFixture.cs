using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Data.Context;
using SocialNetwork.Data.Repositories.Implementations;
using SocialNetwork.Data.UnitOfWork.Implementations;

namespace SocialNetwork.Test.Fixtures
{
    public class SocialNetworkFixture : WebApplicationFactory<Program>
    {
        private SocialNetworkContext _inMemoryDbContext { get; set; } = null!;
        private IServiceProvider _serviceProvider = null!;

        public SocialNetworkContext InMemoryDbContext
        {
            get
            {
                if (_inMemoryDbContext == null)
                {
                    _inMemoryDbContext = _serviceProvider.GetService<SocialNetworkContext>()!;
                }

                return _inMemoryDbContext;
            }
        }

        public SocialNetworkFixture() : base()
        {

        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var contextDescriptor = services.FirstOrDefault(desc => desc.ServiceType == typeof(SocialNetworkContext))!;
                var contextOptionsDescriptor = services.FirstOrDefault(desc => desc.ServiceType == typeof(DbContextOptions<SocialNetworkContext>))!;

                services.Remove(contextDescriptor);
                services.Remove(contextOptionsDescriptor);

                services.AddDbContext<SocialNetworkContext>(opt =>
                opt
                .UseLazyLoadingProxies()
                .UseInMemoryDatabase("SocialNetworkTest"));

                _serviceProvider = services.BuildServiceProvider();
            });


        }

        public static UnitOfWork GetUnitOfWork(SocialNetworkContext context)
        {
            return new UnitOfWork(context,
                new UserRepository(context),
                new PersonalUserRepository(context),
                new EnterpriseUserRepository(context));
        }
    }
}
