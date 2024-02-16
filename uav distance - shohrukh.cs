        double[,] old_wp1 = new double[100, 3];
        private void Tmr_get_teams_distance(object sender, EventArgs e)
        {
            MyWebRequest get_teams = new MyWebRequest("http://" + IP + "/api/teams", "GET");
            string a = get_teams.GetResponse();
            if (a != "")
            {
                List<Get_Teams> team = JsonConvert.DeserializeObject<List<Get_Teams>>(a);

                for (int x = 0; x < team.Count; x++)
                {
                    try
                    {
                        if (old_wp1[x, 0] == team[x].telemetry.latitude && old_wp1[x, 1] == team[x].telemetry.longitude && old_wp[x, 2] == team[x].telemetry.altitude)
                            continue;
                        else
                        {
                            old_wp1[x, 0] = team[x].telemetry.latitude;
                            old_wp1[x, 1] = team[x].telemetry.longitude;
                            old_wp1[x, 2] = team[x].telemetry.altitude;
                        }

                        PointLatLng point = new PointLatLng(team[x].telemetry.latitude, team[x].telemetry.longitude);
                        PointLatLng p1 = new PointLatLng(MainV2.comPort.MAV.cs.lat, MainV2.comPort.MAV.cs.lng);
                        string username = team[x].team.username;
                        dist_uav = gmap.MapProvider.Projection.GetDistance(p1, point) * 1000;


                        if (txt_username.Text.ToString() != team[x].team.username && dist_uav <= 5 && (MainV2.comPort.MAV.cs.alt - team[x].telemetry.altitude <= 5))
                        {
                            Get_Team_Plane(point, team[x], x);
                            string warning = "Approaching UAV!";
                            string caption = "Danger!";
                            string uavwarning = warning + "\n" + team[x].team.username + ":\nDistance: " + dist_uav + "\n" + team[x].telemetry;
                            MessageBox.Show(uavwarning, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Convert.ToString(ex));
                    }

                }

            }
        }