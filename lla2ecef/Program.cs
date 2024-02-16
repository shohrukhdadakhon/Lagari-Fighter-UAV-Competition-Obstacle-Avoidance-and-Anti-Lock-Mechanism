using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lla2ecef
{
    class Program
    {
        public static double a = 6378137;
        public static double e = 8.1819190842622 / 100;
        public static double[] tm_lat = new double[3];
        public static double[] tm_long = new double[3];
        public static double[] tm_alt = new double[3];
        public static double Mylat = 5423;
        public static double Mylng = 982;
        public static double Myalt = 123;
        public static int myID = 2;


        public static double lla2ecef_x(int id)
        {
            var lat1 = ConvertDegreesToRadians(tm_lat[id]);
            var lon1 = ConvertDegreesToRadians(tm_long[id]);
            double alt1 = tm_alt[id];

            double N = a / Math.Sqrt(1 - Math.Pow(e, 2) * Math.Pow(Math.Sin(lat1), 2));
            double x = (N + alt1) * Math.Cos(lat1) * Math.Cos(lon1);
            return x;
        }
        public static double lla2ecef_y(int id)
        {
            var lat1 = ConvertDegreesToRadians(tm_lat[id]);
            var lon1 = ConvertDegreesToRadians(tm_long[id]);
            double alt1 = tm_alt[id];

            double N = a / Math.Sqrt(1 - Math.Pow(e, 2) * Math.Pow(Math.Sin(lat1), 2));
            double y = (N + alt1) * Math.Cos(lat1) * Math.Sin(lon1);
            return y;
        }
        public static double lla2ecef_z(int id)
        {
            var lat1 = ConvertDegreesToRadians(tm_lat[id]);
            var lon1 = ConvertDegreesToRadians(tm_long[id]);
            double alt1 = tm_alt[id];

            double N = a / Math.Sqrt(1 - Math.Pow(e, 2) * Math.Pow(Math.Sin(lat1), 2));
            double z = ((1 - Math.Pow(e, 2)) * N + alt1) * Math.Sin(lat1);
            return z;
        }

        public static double ConvertDegreesToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static double ConvertRadiansToDegrees(double angle)
        {
            return 180.0 * angle / Math.PI;
        }

        public static double get_yaw_btw2pnts(int id)
        {
            double dX = lla2ecef_x(myID) - lla2ecef_x(id);
            double dY = lla2ecef_y(myID) - lla2ecef_y(id);
            double dZ = lla2ecef_z(myID) - lla2ecef_z(id);

            var yaw = Math.Atan2(dZ, dX);

            return (ConvertRadiansToDegrees(yaw) + 360) % 360;
        }

        public static double get_bearing_btw2pnts(int id)
        {
            var lat1 = ConvertDegreesToRadians(tm_lat[id]);
            var long1 = ConvertDegreesToRadians(tm_long[id]);
            var lat2 = ConvertDegreesToRadians(Mylat);
            var long2 = ConvertDegreesToRadians(Mylng);
            var dLon = long2 - long1;

            var y = Math.Sin(dLon) * Math.Cos(lat2);
            var x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);
            var brng = Math.Atan2(y, x);

            return (ConvertRadiansToDegrees(brng) + 360) % 360;
        }

        static void Main(string[] args)
        {
            tm_lat[1] = 1345;
            tm_long[1] = 532;
            tm_alt[1] = 234;
            tm_lat[2] = Mylat;
            tm_long[2] = Mylng;
            tm_alt[2] = Myalt;
            Console.WriteLine(get_bearing_btw2pnts(1) + "\n" + get_yaw_btw2pnts(1));
            Console.ReadLine();
        }

        
    }
}
