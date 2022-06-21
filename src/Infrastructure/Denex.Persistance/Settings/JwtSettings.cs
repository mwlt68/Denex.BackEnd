using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Persistance.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public int ValidateLifetime { get; set; }
//        public string ValidIssuer { get; set; }
//        public string ValidAudience { get; set; }
    }
}
