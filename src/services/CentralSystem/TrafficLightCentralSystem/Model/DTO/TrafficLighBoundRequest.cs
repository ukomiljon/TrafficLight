using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrafficLightCentralSystem.Model.DTO
{
    public enum TrafficLightBound { West, East, North, South }


    public class TrafficLightIntersection
    {
        [Required]
        public string IntersectionName { get; set; }

        [Required]
        public List<TrafficLighBoundRequest> TrafficBounds { get; set; }
    }

    public class TrafficLighBoundRequest
    {
        public List<PickHour> PickHours { get; set; }

        [Required]
        public NormalHour NormalHour { get; set; }

        [Required]
        [EnumDataType(typeof(TrafficLightBound))]
        [JsonConverter(typeof(StringEnumConverter))]
        public TrafficLightBound TrafficLightBound { get; set; }
    }

    public class PickHour : SignalStayTimeRequest
    {
        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid Time.")]
        [DefaultValue("17:00")]
        public string Start { get; set; }
        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid Time.")]
        [DefaultValue("19:00")]
        public string End { get; set; }
    }

    public class NormalHour : SignalStayTimeRequest
    {
    }
}
