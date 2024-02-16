public int count_intruders(double intruder_dist, double[] dist_UAV, int team_count)
        {
            int x;
            int count = 0;
            for (x = 0; x < team_count; x++)
            {
                if (dist_UAV[x] <= intruder_dist)
                    count++;
            }
            return count;
        }

        public int[] get_intruderID(double intruder_dist, double[] dist_UAV, int team_count)
        {
            int[] ID_arr = new int[30];
            int counter = 0;
            for (int x = 0; x < team_count; x++)
            {
                if (dist_UAV[x] <= intruder_dist)
                {
                    ID_arr[counter] = x;
                    counter++;
                }
            }
            return ID_arr;
        }

        public double get_3d_dist(double d1, double alt1, double alt2)
        {
            double result = 0;
            result = Math.Sqrt(d1 * d1 + (alt1 - alt2) * (alt1 - alt2));
            return result;

        }

        //Destination point given distance and bearing from start point :  http://www.movable-type.co.uk/scripts/latlong.html
        public double get_escape_lat(double lat1, double dist1, double bearing)
        {
            double ang_dist = (Math.PI / 180) * (dist1 / 6371);
            double result_lat = 0;
            var brng_rd = (Math.PI / 180) * bearing;
            var lat_rd = lat1 * (Math.PI / 180);


            result_lat = Math.Asin(Math.Sin(lat_rd) * Math.Cos(ang_dist) + Math.Cos(lat_rd) * Math.Sin(ang_dist) * Math.Cos(brng_rd));

            return result_lat;
        }

        //Destination point given distance and bearing from start point :  http://www.movable-type.co.uk/scripts/latlong.html
        public double get_escape_long(double long1, double lat1, double dist1, double bearing)
        {
            double ang_dist = (Math.PI / 180) * (dist1 / 6371);
            double result_long = 0;
            var brng_rd = (Math.PI / 180) * bearing;
            var long1_rd = long1 * (Math.PI / 180);
            var lat1_rd = lat1 * (Math.PI / 180);
            var lat2_rd = get_escape_lat(lat1, dist1, bearing) * (Math.PI / 180);


            result_long = (180 / Math.PI) * (long1_rd + Math.Atan2(Math.Sin(brng_rd) * Math.Sin(ang_dist) * Math.Cos(lat1_rd), Math.Cos(ang_dist) - Math.Sin(lat1_rd) * Math.Sin(lat2_rd)));

            return result_long;
        }

        public double get_bearing(double team_heading)
        {
            double intruderHeading = team_heading % 360;
            double myHeading = MainV2.comPort.MAV.cs.yaw % 360;

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
                return (360 + bearing);
            else if (bearing > 360)
                return (bearing % 360);
            else
                return bearing;
        }
        
        public void distuav_label(double[] dist_uav, int team_count, string[] tm_usrnames)
        {

            int cnt_100m = count_intruders(100, dist_uav, team_count);
            int cnt_200m = count_intruders(200, dist_uav, team_count);
            int cnt_300m = count_intruders(300, dist_uav, team_count);
            int[] id_100m = get_intruderID(100, dist_uav, team_count);
            int[] id_200m = get_intruderID(200, dist_uav, team_count);
            int[] id_300m = get_intruderID(300, dist_uav, team_count);


            if (cnt_100m >= 0)
            {
                if (cnt_100m == 0)
                {
                    dist_100m_label.Text = "UAVs < 100m: ";
                }
                else if (cnt_100m > 0)
                {
                    for (int x = 0; x < cnt_100m; x++)
                    {
                        if (txt_username.Text.ToString() != tm_usrnames[id_100m[x]])
                        {
                            dist_100m_label.Text = "UAVs < 100m: " + "\n" + dist_uav[id_100m[x]] + "m";
                        }
                    }
                }
            }

            if (cnt_200m >= 0)
            {
                if (cnt_200m == 0)
                {
                    dist_200m_label.Text = "UAVs < 200m: ";
                }
                else if (cnt_200m > 0)
                {
                    for (int x = 0; x < cnt_200m; x++)
                    {
                        if (txt_username.Text.ToString() != tm_usrnames[id_200m[x]])
                        {
                            dist_200m_label.Text = "UAVs < 200m: " + "\n" + dist_uav[id_200m[x]] + "m";
                        }

                    }
                }
            }

            if (cnt_300m >= 0)
            {
                if (cnt_300m == 0)
                {
                    dist_300m_label.Text = "UAVs < 300m: ";
                }
                else if (cnt_300m > 0)
                {
                    for (int x = 0; x < cnt_300m; x++)
                    {
                        if (txt_username.Text.ToString() != tm_usrnames[id_300m[x]])
                        {
                            dist_300m_label.Text = "UAVs < 300m: " + "\n" + dist_uav[id_300m[x]] + "m";
                        }

                    }
                }
            }
        }

        public void escape_to_here(double team_lat, double team_long, double team_alt, double team_heading) {
            
            bearing = get_bearing(team_heading);

            MainV2.comPort.MAV.cs.mode = "Guided";
            CMB_modes.Text = "Guided";

            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                CustomMessageBox.Show(Strings.PleaseConnect, Strings.ERROR);
                return;
            }

            MainV2.comPort.MAV.GuidedMode.z = MainV2.comPort.MAV.cs.alt;

            Locationwp escapetohere = new Locationwp();
            escapetohere.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
            escapetohere.alt = MainV2.comPort.MAV.GuidedMode.z;
            escapetohere.lat = get_escape_lat(team_lat, desired_dist, bearing);
            escapetohere.lng = get_escape_long(team_long, team_lat, desired_dist, bearing);

            if (MainV2.comPort.MAV.cs.failsafe)
            {
                if (CustomMessageBox.Show("You are in failsafe, are you sure?", "Failsafe", MessageBoxButtons.YesNo) != (int)DialogResult.Yes)
                {
                    return;
                }
            }
            MainV2.comPort.setMode(CMB_modes.Text);

            try
            {
                MainV2.comPort.setGuidedModeWP(escapetohere);
            }
            catch (Exception ex)
            {
                MainV2.comPort.giveComport = false;
                CustomMessageBox.Show(Strings.CommandFailed + ex.Message, Strings.ERROR);
            }
        }

        public void set_Auto() {

            MainV2.comPort.MAV.cs.mode = "Auto";
            CMB_modes.Text = "Auto";

            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                CustomMessageBox.Show(Strings.PleaseConnect, Strings.ERROR);
                return;
            }

            if (MainV2.comPort.MAV.cs.failsafe)
            {
                if (CustomMessageBox.Show("You are in failsafe, are you sure?", "Failsafe", MessageBoxButtons.YesNo) != (int)DialogResult.Yes)
                {
                    return;
                }
            }
            MainV2.comPort.setMode(CMB_modes.Text);
        }

        int num_id;
        double dist1;
        double[] dist_uav = new double[30];
        double old_dist;
        double max_intruder_dist = 50;       
        double bearing = 0;
        double[,] old_wp = new double[30, 2];
        double desired_dist = 5;
        bool[] inGuided = new bool[30];
        bool[] escapingModeON = new bool[30];
        bool multiple_uav;
        string[] team_usernames = new string[30];

        private void Tmr_get_teams_Tick(object sender, EventArgs e)
        {
            MyWebRequest get_teams = new MyWebRequest("http://" + IP + "/api/teams", "GET");
            string a = get_teams.GetResponse();
            if (a != "")
            {
                List<Get_Teams> team = JsonConvert.DeserializeObject<List<Get_Teams>>(a);

                for (int i = 0; i < team.Count; i++)
                {
                    try
                    {                        
                        /*
                        if (old_wp[i, 0] == team[i].telemetry.latitude && old_wp[i, 1] == team[i].telemetry.longitude)
                            continue;
                        else
                        {
                            old_wp[i, 0] = team[i].telemetry.latitude;
                            old_wp[i, 1] = team[i].telemetry.longitude;
                        } 
                        */

                        PointLatLng point = new PointLatLng(team[i].telemetry.latitude, team[i].telemetry.longitude);
                        PointLatLng p1 = new PointLatLng(MainV2.comPort.MAV.cs.lat, MainV2.comPort.MAV.cs.lng);

                        dist1 = gmap.MapProvider.Projection.GetDistance(p1, point) * 1000;
                        dist_uav[i] = get_3d_dist(dist1, MainV2.comPort.MAV.cs.alt, team[i].telemetry.altitude);
                        team_usernames[i] = team[i].team.username;

                        //  && dist_uav <= ConvertToDouble(txt_dist_max.Text.ToString())

                        if (txt_username.Text.ToString() != team[i].team.username)
                        {
                            distuav_label(dist_uav, team.Count, team_usernames);

                            Get_Team_Plane(point, team[i], i);
                            bearing = get_bearing(team[i].telemetry.heading);                           

                            if (dist_uav[i] <= max_intruder_dist)
                            {
                                try {
                                    if (old_dist <= dist_uav[i])
                                    {
                                        if (inGuided[i] == false && escapingModeON[i] == false && multiple_uav == false)
                                        {
                                            escape_to_here(team[i].telemetry.latitude, team[i].telemetry.longitude, team[i].telemetry.altitude, team[i].telemetry.heading);

                                            escapingModeON[i] = true;
                                            inGuided[i] = true;
                                            old_dist = dist_uav[i];
                                            num_id = i;
                                        }
                                    }
                                    else if (old_dist > dist_uav[i])
                                    {
                                        if (inGuided[i] == false && escapingModeON[i] == false)
                                        {
                                            escape_to_here(team[i].telemetry.latitude, team[i].telemetry.longitude, team[i].telemetry.altitude, team[i].telemetry.heading);

                                            escapingModeON[i] = true;
                                            inGuided[i] = true;
                                            escapingModeON[num_id] = false;
                                            inGuided[num_id] = false;
                                            multiple_uav = true;
                                        }
                                    }
                                }

                                catch (Exception ex)
                                {
                                    MessageBox.Show(Convert.ToString(ex));
                                }

                            }

                            else if (dist_uav[i] > max_intruder_dist)
                            {
                                try
                                {
                                    if (inGuided[i] == true && escapingModeON[i] == true)
                                    {

                                        set_Auto();

                                        multiple_uav = false;
                                        inGuided[i] = false;
                                        escapingModeON[i] = false;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(Convert.ToString(ex));
                                }
                            }

                        }

                        /*
                        MAVLinkInterface.altitude = team[i].telemetry.altitude;
                        MAVLinkInterface.latitude = team[i].telemetry.latitude;
                        MAVLinkInterface.longitude = team[i].telemetry.longitude;
                        MAVLinkInterface.heading = Convert.ToSingle(team[i].telemetry.heading);
                        gMapControl1.Refresh();
                        */

                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(Convert.ToString(ex));
                    }

                }
            }
        }