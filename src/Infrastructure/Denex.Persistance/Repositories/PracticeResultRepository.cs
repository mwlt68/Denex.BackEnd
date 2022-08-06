using Denex.Application.Exceptions;
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

        public async Task<bool> CheckCurrentUserCanAccess(string? userId, string practiceResultId)
        {
            if (userId != null)
            {
                var practiceResult = await GetAsync(x => x.Id == practiceResultId && x.UserId == userId);
                if (practiceResult != null)
                {
                    return true;
                }
                else throw new PracticeResultNotFoundException();
            }
            else throw new UserNotFoundException();
        }

    }
}
