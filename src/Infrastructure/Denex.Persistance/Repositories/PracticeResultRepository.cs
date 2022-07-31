using Denex.Application.Interfaces.Repository;
using Denex.Domain.Entities;
using Denex.Persistance.Settings;
using Microsoft.Extensions.Options;

namespace Denex.Persistance.Repositories
{
    internal class PracticeResultRepository : GenericRepository<PracticeResult>, IPracticeResultRepository
    {
        public PracticeResultRepository(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}
