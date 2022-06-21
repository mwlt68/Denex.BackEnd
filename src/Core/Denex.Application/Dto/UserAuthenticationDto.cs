using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Dto
{
    public class UserAuthenticationDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
