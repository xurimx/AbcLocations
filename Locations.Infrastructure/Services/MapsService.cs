using Locations.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Infrastructure.Services
{
    public class MapsService : IMapsService
    {
        private readonly IHttpClientFactory clientFactory;

        public MapsService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        public Task<string> GetCityNameAsync(double longitude, double latitude)
        {
            throw new NotImplementedException();
        }
    }
}
