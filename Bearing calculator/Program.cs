using System;

namespace Bearing_calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            int bearing = 0;
            for (int intruderHeading = 0; intruderHeading < 360; intruderHeading += 10) {
                for (int myHeading = 0; myHeading < 360; myHeading += 10) {
                    if (myHeading > intruderHeading)
                    {
                        if (myHeading - intruderHeading <= 180)
                            bearing = (myHeading + 180) % 360 - 90;
                        else if (myHeading - intruderHeading > 180)
                            bearing = (myHeading + 180) % 360 + 90;
                    }
                    else if (myHeading <= intruderHeading)
                    {
                        if (intruderHeading - myHeading <= 180)
                            bearing = (myHeading + 180) % 360 + 90;
                        else if (intruderHeading - myHeading > 180)
                            bearing = (myHeading + 180) % 360 - 90;
                    }

                    if (bearing < 0)
                        Console.WriteLine("my heading: " + myHeading + " | intruder heading: " + intruderHeading + " = bearing: " + (bearing+360));
                    else if (bearing > 360)
                        Console.WriteLine("my heading: " + myHeading + " | intruder heading: " + intruderHeading + " = bearing: " + (bearing % 360));                    
                    else
                        Console.WriteLine("my heading: " + myHeading + " | intruder heading: " + intruderHeading + " = bearing: " + bearing);
                }
                Console.WriteLine("");
            }
            
        }
    }
}
