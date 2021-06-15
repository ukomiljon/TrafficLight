using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrafficLightCentralSystem.Model.DTO
{
    public class SignalStayTimeRequest
    {
        [Required]
        [Range(0, Int32.MaxValue)]
        [DefaultValue(4)]
        public int Red { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        [DefaultValue(5)]
        public int Yellow { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        [DefaultValue(20)]
        public int Green { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int RightTurnGreen { get; set; }
    }
}
