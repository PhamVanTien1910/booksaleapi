using BookSale.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Exceptions
{
    public class CustomExceptions : Exception
    {
        public int StatusCode { get; }
        public ErrorCodeEnums ErrorCode { get; }

        public CustomExceptions(int statuscode, ErrorCodeEnums errorCode, string message) : base(message) 
        {
            StatusCode = statuscode;
            ErrorCode = errorCode;
        }
        public CustomExceptions(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = ErrorCodeEnums.ServerError;
        }
    }
}
