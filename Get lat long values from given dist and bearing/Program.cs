using System;

namespace Get_lat_long_values_from_given_dist_and_bearing
{
    class Program
    {
        public double GetEscapeLat(double lat1, double dist1, double bearing)
        {
            double ang_dist = (Math.PI / 180) * (dist1 / (6371));
            double result_lat = 0;
            var brng_rd = (Math.PI / 180) * bearing;
            var lat_rd = lat1 * (Math.PI / 180);


            result_lat = (180 / Math.PI) * Math.Asin(Math.Sin(lat_rd) * Math.Cos(ang_dist) + Math.Cos(lat_rd) * Math.Sin(ang_dist) * Math.Cos(brng_rd));

            return result_lat;
        }
        public double GetEscapeLong(double long1, double lat1, double dist1, double bearing)
        {
            double ang_dist = (Math.PI / 180) * (dist1 / (6371));
            double result_long = 0;
            var brng_rd = (Math.PI / 180) * bearing;
            var long1_rd = long1 * (Math.PI / 180);
            var lat1_rd = lat1 * (Math.PI / 180);
            var lat2_rd = GetEscapeLat(lat1, dist1, bearing) * (Math.PI / 180);


            result_long = (180 / Math.PI) * (long1_rd + Math.Atan2(Math.Sin(brng_rd) * Math.Sin(ang_dist) * Math.Cos(lat1_rd), Math.Cos(ang_dist) - Math.Sin(lat1_rd) * Math.Sin(lat2_rd)));

            return result_long;
        }
        static void Main(string[] args)
        {
            Program n = new Program();
            double lat_last = 0;
            double long_last = 0;
            double lat = 41.0912405;
            double longitude = 28.5441104;
            double bearing = 330;
            double distance = 100000;

            lat_last = n.GetEscapeLat(lat, distance, bearing);
            long_last = n.GetEscapeLong(longitude, lat, distance, bearing);
            Console.WriteLine(lat_last + "\n" + long_last);
        }




    }
}

