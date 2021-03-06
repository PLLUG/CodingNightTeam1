﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoBot.Data_Base
{
    public class GeoObject
    {
        // Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public string Food { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Constructors
        public GeoObject()
        {
            // Empty
        }

        public GeoObject(string name, string description, string food, double latitude, double longitude)
        {
            Name = name;
            Description = description;
            Food = food;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}