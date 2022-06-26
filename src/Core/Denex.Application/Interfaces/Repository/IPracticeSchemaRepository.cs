using Denex.Application.Repository;
using Denex.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Interfaces.Repository
{
    public interface IPracticeSchemaRepository : IGenericRepository<PracticeSchema,string>
    {
    }
}
