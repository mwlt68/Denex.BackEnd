using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Exceptions
{
    internal class ValidationException:Exception
    {
        public ValidationException():base("Validation error occured")
        {

        }
        public ValidationException(string message):base (message)
        {

        }
        public ValidationException(Exception ex):base(ex.Message)
        {

        }
    }
}
