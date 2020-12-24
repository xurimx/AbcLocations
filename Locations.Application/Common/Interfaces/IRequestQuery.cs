using System;
using System.Collections.Generic;
using System.Text;

namespace Locations.Application.Common.Interfaces
{
    public interface IRequestQuery
    {
        string? Search { get; set; }
        string? OrderBy { get; set; }
        string? OrderDirection { get; set; }
        int? Page { get; set; }
        int? Limit { get; set; }
    }
}
