using Denex.Application.Repository;
using Denex.Domain.Entities;
using Denex.Persistance.Settings;
using Microsoft.Extensions.Options;

namespace Denex.Persistance.Repositories
{
    internal class UserRespository : GenericRepository<User>, IUserRepository
    {
        public UserRespository(IOptions<MongoDbSettings> options) : base(options)
        {
        }

    }
}
