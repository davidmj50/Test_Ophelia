using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.Common.Models
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public List<MessageResult> Message { get; set; }
        public T Result { get; set; }
        public Response()
        {
            Message = new List<MessageResult>();
        }

    }
    public class MessageResult
    {
        public string Message { get; set; }

    }
    public class ErrorMessage
    {
        public string Message { get; set; }

    }
}
