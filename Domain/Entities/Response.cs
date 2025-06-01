using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Response<T>
    {
        public Response(T data, bool succeded = true, string message = null)
        {
            Succeded = succeded;
            Message = message;
            Result = data;
        }

        public Response(string message)
        {
            Succeded = false;
            Message = message;
            Result = default;
        }

        public bool Succeded { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
