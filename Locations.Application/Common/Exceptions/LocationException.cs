using Locations.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Application.Common.Exceptions
{
    public class LocationException : Exception
    {
        public List<Error> Errors { get; set; } = new List<Error>();
        public int Status { get; set; } = 400;

        public LocationException() : base() { }

        public LocationException(string message, int status, List<Error> errors) : base(message)
        {
            this.Status = status;
            this.Errors = errors;
        }

        public LocationException(string message, int status) : base(message)
        {
            this.Status = status;
        }

        public LocationException(string message) : base(message) { }

        public LocationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
