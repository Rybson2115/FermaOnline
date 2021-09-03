using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FermaOnline.Models
{
    public static class TemplateCurve
    {

        public static Dictionary<int, float> DefaultValue = new Dictionary<int, float>() { { 28,6.4f}, { 31, 8.4f },
            { 35, 10.8f }, { 38, 14f }, { 46, 17.5f }, { 52, 21f }, { 60, 25f },{ 67, 8.4f } };//int-Dzień życia float-Masa ciała
        public static List<float> LiveTimeValue { get; set; } //dane wyliczane na podstawie średniej z doświadczeń 

    }
}
