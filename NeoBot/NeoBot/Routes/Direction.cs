using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace NeoBot.Routes
{
    public class Direction
    {
        public static string GetUrlDirection (double fromLatitude, double fromLongitude, double toLatitude, double toLongitude)
        {   // return string-url with route
            return $"www.google.com/maps/dir/{fromLatitude},{fromLongitude}/{toLatitude},{toLongitude}";
        }
    }
}