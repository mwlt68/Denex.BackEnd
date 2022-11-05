using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Wrappers
{
    public abstract class BaseResponse
    {
        public BaseResponse(bool success=true, string? message=null)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
