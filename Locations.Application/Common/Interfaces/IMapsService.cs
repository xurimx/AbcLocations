using Locations.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Application.Common.Interfaces
{
    public interface IMapsService
    {
        Task<string> GetCityNameAsync(double longitude, double latitude);
    }
}
