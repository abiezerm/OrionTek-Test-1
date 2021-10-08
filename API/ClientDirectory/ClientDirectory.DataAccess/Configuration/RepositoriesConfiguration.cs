using ClientDirectory.Core;
using ClientDirectory.DataAccess.Repositories;
using ClientDirectory.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClientDirectory.DataAccess.Configuration
{
    public static partial class RepositoriesConfiguration
    {
        public static void AddRespositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ClientDirectoryContext>(m =>
            {
                m.UseSqlServer(configuration.GetConnectionString("ClientDirectoryDB"));
            });

            services.AddScoped<IBaseRepository<Client>, BaseRepository<Client>>();
            services.AddScoped<IBaseRepository<Address>, BaseRepository<Address>>();
        }
    }
}
