using Microsoft.Extensions.Logging;
using Template_SQLite_EfCore.Models.Services.Infrastructure;

namespace Template_SQLite_EfCore.Models.Services.Application
{
    public class EfCoreBaseService : IBaseService
    {
        private readonly ILogger<EfCoreBaseService> logger;
        private readonly MyBaseDbContext dbContext;
        public EfCoreBaseService(ILogger<EfCoreBaseService> logger, MyBaseDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }
    }
}