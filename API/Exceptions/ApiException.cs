using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Exceptions
{
    public class ApiException : ApiResponse {

        public string Details { get; set; }


        public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message) {
            this.Details = details;
        }
    }
}
