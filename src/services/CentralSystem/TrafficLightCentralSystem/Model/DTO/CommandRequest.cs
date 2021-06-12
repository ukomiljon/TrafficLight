using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrafficLightCentralSystem.Model.DTO
{
    public enum ProccessCommand { Run, Stop, Reset }
    public class CommandRequest
    {
        [Required]
        [EnumDataType(typeof(ProccessCommand))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProccessCommand Command { get; set; }
    }
}
