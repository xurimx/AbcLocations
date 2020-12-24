using Locations.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Locations.Domain.Entities
{
    public class City : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
