using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace TrafficLightCentralSystem.Model.DTO
{
    public enum ProccessCommandRequest { Run, Stop, Reset }
    public class CommandRequest
    {
        [Required]
        [EnumDataType(typeof(ProccessCommandRequest))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProccessCommandRequest Command { get; set; }
    }
}
