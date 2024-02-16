/* Code below writes the lat long heading values of uav to a text file:

                        string pathlong = @"C:\Users\shokh\Desktop\long.txt";
                        string pathlat = @"C:\Users\shokh\Desktop\lat.txt";
                        string pathheading = @"C:\Users\shokh\Desktop\heading.txt";

                        if (old_wp[i, 0] != MainV2.comPort.MAV.cs.lat || old_wp[i, 1] != MainV2.comPort.MAV.cs.lng)
                        {
                            using (StreamWriter sw = File.AppendText(pathlong))
                            {
                                sw.WriteLine(MainV2.comPort.MAV.cs.lng);
                            }
                            using (StreamWriter sw = File.AppendText(pathlat))
                            {
                                sw.WriteLine(MainV2.comPort.MAV.cs.lat);

                            }
                            using (StreamWriter sw = File.AppendText(pathheading))
                            {
                                sw.WriteLine(MainV2.comPort.MAV.cs.yaw);

                            }
                        }
                      
                        old_wp[i, 0] = MainV2.comPort.MAV.cs.lat;
                        old_wp[i, 1] = MainV2.comPort.MAV.cs.lng;
                        */