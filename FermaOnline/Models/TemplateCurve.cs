using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FermaOnline.Models
{
    public static class TemplateCurve
    {
         
        public static List<float> DefaultValue { get; set; }
        public static List<float> LiveTimeValue { get; set; } //dane wyliczane na podstawie średniej z doświadczeń 

    }
}
