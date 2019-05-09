using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IPORevamp.WebAPI.Models
{
    public class ApiResponse
    {
        public string Message;
        public HttpStatusCode StatusCode;
        public object Content;
        public bool Error;

        public ApiResponse(string message, HttpStatusCode statusCode = HttpStatusCode.OK, object content = null, bool error = false)
        {
            Message = message;
            StatusCode = statusCode;
            Content = content;
            Error = error;
        }

        public ApiResponse()
        {

        }
    }
}
