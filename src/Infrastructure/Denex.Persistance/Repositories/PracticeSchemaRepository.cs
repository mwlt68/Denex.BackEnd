using Denex.Application.Interfaces.Repository;
using Denex.Domain.Entities;
using Denex.Persistance.Settings;
using Microsoft.Extensions.Options;

namespace Denex.Persistance.Repositories
{
    internal class PracticeSchemaRepository : GenericRepository<PracticeSchema>, IPracticeSchemaRepository
    {
        public PracticeSchemaRepository(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}
