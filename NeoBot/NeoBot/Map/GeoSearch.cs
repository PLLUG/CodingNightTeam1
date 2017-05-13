using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace NeoBot.Routes
{
    public class GeoSearch
    {
        public static string GetUrlDirection (double fromLatitude, double fromLongitude, double toLatitude, double toLongitude)
        {   // return string-url with route
            return $"www.google.com/maps/dir/{fromLatitude},{fromLongitude}/{toLatitude},{toLongitude}";
        }

        public async static Task<string> GetPlacesNearBy(double latitude, double longtitude, int radius, string type, string keyword)
        {
            using (var client = new HttpClient())
            {
                var taskResult = await client.GetStringAsync($"https://maps.googleapis.com/maps/api/place/radarsearch/json?location={latitude},{longtitude}&radius={radius}&type={type}&keyword={keyword}&key=AIzaSyAAbEr0yr0AP-a7wbmghSyKHKGiyvlNCuA");
                Dictionary<string, object> values =
                JsonConvert.DeserializeObject<Dictionary<string, object>>(taskResult);
                JArray certainValue = values["results"] as JArray;

                List<Data_Base.GeoObject> cafes = new List<Data_Base.GeoObject>();
                foreach (var item in certainValue)
                {
                    double lat = (double)(item)["geometry"]["location"]["lat"];
                    double lng = (double)(item)["geometry"]["location"]["lng"];
                    string name = (string)((item)["name"]);
                    cafes.Add(new Data_Base.GeoObject(name, string.Empty, string.Empty, lat, lng));
                }
                return taskResult;
            }
        }
    }
}