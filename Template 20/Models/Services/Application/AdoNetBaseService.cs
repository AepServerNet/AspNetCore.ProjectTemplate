using Microsoft.Extensions.Logging;
using Template_SqlServer_AdoNet.Models.Services.Infrastructure;

namespace Template_SqlServer_AdoNet.Models.Services.Application
{
    public class AdoNetBaseService : IBaseService
    {
        private readonly ILogger<AdoNetBaseService> logger;
        private readonly IDatabaseAccessor db;
        public AdoNetBaseService(ILogger<AdoNetBaseService> logger, IDatabaseAccessor db)
        {
            this.logger = logger;
            this.db = db;
        }
    }
}