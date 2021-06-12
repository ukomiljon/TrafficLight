using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
 
using System.Threading.Tasks;

namespace TrafficLightCentralSystem.Model.DTO
{
    public enum TrafficLightBound { West, East, North, South }
    public class TrafficLighBoundRequest
    {       
        public IEnumerable<PickHour> PickHours { get; set; }

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
