using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
