using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Interfaces.Service
{
    public interface IJwtService
    {
        public String CreateToken(string userId);
    }
}
