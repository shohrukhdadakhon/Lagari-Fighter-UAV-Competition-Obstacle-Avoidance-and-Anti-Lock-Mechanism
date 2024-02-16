using System;

namespace test
{
    class Program
    {
        

        static void Main(string[] args)
        {
            int team_count = 10;
            double[] dist_UAV = { 12, 23, 65, 34, 767, 45, 3656, 3, 343, 46456 };
            double max_dist = 50;
            int intruder = get_closest_intruder(dist_UAV, team_count - 1, max_dist);
            Console.WriteLine(intruder);
        }

        public static int closest_id;
        public static int get_closest_intruder(double[] dist_UAV, int team_cnt, double max_dist)
        {

            if (team_cnt >= 0)
            {
                if (dist_UAV[team_cnt] <= max_dist)
                {
                    closest_id = team_cnt;
                    return get_closest_intruder(dist_UAV, team_cnt - 1, dist_UAV[team_cnt]);
                }
                else
                {
                    return get_closest_intruder(dist_UAV, team_cnt - 1, max_dist);
                }
            }
            else
            {
                return closest_id;
            }
        }
    }
}
