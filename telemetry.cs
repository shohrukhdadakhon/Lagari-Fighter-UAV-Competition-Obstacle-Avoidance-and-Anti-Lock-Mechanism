using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MissionPlanner
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class telemetry
    {
        [JsonProperty("takim_numarasi")]
        public long Id { get; set; }

        [JsonProperty("IHA_enlem")]
        public double Latitude { get; set; }

        [JsonProperty("IHA_boylam")]
        public double Longitude { get; set; }

        [JsonProperty("IHA_irtifa")]
        public double Altitude { get; set; }

        [JsonProperty("IHA_dikilme")]
        public double Pitch { get; set; }

        [JsonProperty("IHA_yonelme")]
        public double Heading { get; set; }

        [JsonProperty("IHA_yatis")]
        public double Roll { get; set; }

        [JsonProperty("IHA_hiz")]
        public double UAV_vel { get; set; }

        [JsonProperty("IHA_batarya")]
        public double UAV_bat { get; set; }

        [JsonProperty("IHA_kilitlenme")]
        public int UAV_locked { get; set; }

        [JsonProperty("Hedef_merkez_X")]
        public double Target_X { get; set; }

        [JsonProperty("Hedef_merkez_Y")]
        public double Target_Y { get; set; }

        [JsonProperty("Hedef_genislik")]
        public double Target_width { get; set; }

        [JsonProperty("Hedef_yukseklik")]
        public double Target_height { get; set; }

        [JsonProperty("Hedef_genislik")]
        public double Target_width { get; set; }       
    }

    public partial class telemetry
    {
        public static telemetry FromJson(string json) => JsonConvert.DeserializeObject<telemetry>(json, MissionPlanner.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this telemetry self) => JsonConvert.SerializeObject(self, MissionPlanner.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
