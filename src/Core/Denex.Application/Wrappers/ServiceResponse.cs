using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Wrappers
{
    public class ServiceResponse<T> : BaseResponse
    {
        public T? Value { get; set; }

        public ServiceResponse(bool success = true, string? message = null) : base(success, message)
        {
        }
        public ServiceResponse(T? value, bool success = true, string? message = null):base(success, message)
        {
            Value = value;
        }
    }

    public class VoidServiceResponse : BaseResponse
    {
        public VoidServiceResponse(bool success = true, string? message = null) : base(success, message)
        {
        }
    }
}
