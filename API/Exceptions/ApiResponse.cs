using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Exceptions
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string message = null) {
            StatusCode = statusCode;
            Message = message ?? GedDefaultMessageForStatusCode(statusCode);
        }

        private string GedDefaultMessageForStatusCode(int statusCode) {
            return statusCode switch {
                400 => "A bad request, you gave made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Server internal Error",
                _ => null
            };
        }
    }
}
