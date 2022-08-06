using Denex.Application.Repository;
using Denex.Domain.Entities;

namespace Denex.Application.Interfaces.Repository
{
    public interface IPracticeResultRepository : IGenericRepository<PracticeResult,string>
    {

        Task<bool> CheckCurrentUserCanAccess(string? userId, string practiceResultId);
    }
}
