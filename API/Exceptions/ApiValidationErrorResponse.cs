using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Exceptions
{
    public class ApiValidationErrorResponse : ApiResponse {

        public IEnumerable<string> Errors { get; set; }

        public ApiValidationErrorResponse() : base(400) {

        }

    }
}
