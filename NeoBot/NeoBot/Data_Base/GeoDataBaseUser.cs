using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoBot.Data_Base
{
    public class GeoDataBaseUser
    {
        // Properties
        public GeoDataBase DataBase { get; set; }

        private double UserLat { get; set; }
        private double UserLon { get; set; }

        // Constructors
        public GeoDataBaseUser()
        {
            DataBase = new GeoDataBase();
            DataBase.FormDataFromFile("Data.txt");
        }

        // Members
        public void UserLocation(double latitude, double longetude)
        {
            UserLat = latitude;
            UserLon = longetude;
        }

        public Path GetPoints()
        {
            return new Path(DataBase.FindClosest(UserLat, UserLon), UserLat, UserLon);
        }
    }

    public class Path
    {
        // Properties
        public GeoObject Place { get; set; }
        public double StartLatitude { get; set; }
        public double StartLongitude { get; set; }

        // Constructor
        public Path(GeoObject place, double latitude, double longitude)
        {
            Place = place;
            StartLatitude = latitude;
            StartLongitude = longitude;
        }
    }
}