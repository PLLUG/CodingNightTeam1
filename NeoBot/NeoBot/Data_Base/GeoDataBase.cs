using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoBot.Data_Base
{
    public class GeoDataBase
    {
        // Properties
        List<GeoObject> Data = new List<GeoObject>();

        // Constructors
        public GeoDataBase()
        {
            // Empty
        }

        // Methods
        public void FormDataFromFile(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader stream = new StreamReader(file, System.Text.Encoding.UTF8);

            string line = "";

            while((line = stream.ReadLine()) != null)
            {
                string[] args = line.Split(' ');
                if (args.Length != 4)
                {
                    throw new Exception("Wrong input!");
                }
                Data.Add(new GeoObject(args[0], args[1], double.Parse(args[2]), double.Parse(args[3])));
            }

            stream.Close();
            file.Close();
        }

        public GeoObject FindClosest(double latitude, double longitude) // For example 49.841879, 24.018330
        {
            int index = 0;
            double minLength = double.MaxValue;

            if (Data.Count == 0)
            {
                throw new Exception("Data Base is empty!");
            }

            for (int i = 0; i < Data.Count; i++)
            {
                double currLength = Math.Sqrt(Math.Pow(latitude - Data[i].Latitude, 2) + Math.Pow(longitude - Data[i].Longitude, 2));
                if (minLength > currLength)
                {
                    index = i;
                    minLength = currLength;
                }
            }

            return Data[index];
        }
    }
}