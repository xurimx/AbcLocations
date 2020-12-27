using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Infrastructure.Common.Responses
{
    public class GeocodeResponse
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }
    public partial class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public partial class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public partial class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public partial class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public partial class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public partial class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public partial class PlusCode
    {
        public string compound_code { get; set; }
        public string global_code { get; set; }
    }

    public partial class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public PlusCode plus_code { get; set; }
        public List<string> types { get; set; }
    }
}
