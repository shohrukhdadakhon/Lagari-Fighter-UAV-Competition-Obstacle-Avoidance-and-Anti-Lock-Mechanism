using System;

namespace get_bearing_between_two_lat_long_points
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(get_bearing_btw2pnts());

        }

        public static double get_bearing_btw2pnts()
        {
            var lat1 = ConvertDegreesToRadians(43.19717);
            var long1 = ConvertDegreesToRadians(24.60938);
            var lat2 = ConvertDegreesToRadians(40.74726);
            var long2 = ConvertDegreesToRadians(29.66309);
            var dLon = long2 - long1;

            var y = Math.Sin(dLon) * Math.Cos(lat2);
            var x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);
            var brng = Math.Atan2(y, x);

            return (ConvertRadiansToDegrees(brng) + 360) % 360;
        }
        public static double ConvertDegreesToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
        public static double ConvertRadiansToDegrees(double angle)
        {
            return 180.0 * angle / Math.PI;
        }
    }
}
