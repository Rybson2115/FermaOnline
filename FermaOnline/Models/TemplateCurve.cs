using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FermaOnline.Models
{
    public static class TemplateCurve
    {

        public static Dictionary<int, float> DefaultValuePig = new Dictionary<int, float>() { { 21,5.0f} , { 28,6.6f}, { 35, 8.5f },
            { 42,11.0f }, { 49, 14.2f }, { 56, 18.2f }, { 63, 22.8f }, { 70, 28.2f },{ 77, 34.4f } ,{ 84, 40.7f }, { 91, 47.2f },
            { 98, 53.8f },{ 105, 60.5f },{ 112, 67.3f }, { 119, 74.2f },{ 126, 81.3f },{ 133, 88.8f },{ 140, 96.2f },{ 147, 103.4f },
            { 154, 110.5f}, { 161, 117.5f },{ 168, 124.3f },{ 175, 130.9f},{ 182, 137.3f } };//int-Dzień życia float-Masa ciała
        public static List<float> LiveTimeValue { get; set; } //dane wyliczane na podstawie średniej z doświadczeń 

    }
}
