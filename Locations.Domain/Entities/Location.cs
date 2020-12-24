using Locations.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Locations.Domain.Entities
{
    public class Location : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public City City { get; set; }
        public Guid CityId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
