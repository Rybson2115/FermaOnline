using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FermaOnline.Models
{
    public class Survey
    {
        [Key]
        public int SurveyId { get; set; }
        public int ExperymentId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SurveyDate { get; set; }//Data pomiaru
        public int DayOfLife { get; set; }//Dzień życia zwierzęcia 
        public float AverageBodyWeight { get; set; } //Średnia masa ciała kg/szt

        public int LoculusQuantity { get; set; } //Liczba sztuk na komorze
        public int GroupId { get; set; } //id grupy zwierząt 
        public int DaysFromFirstWeight { get; set; }  //Ilość dni od pierwszego ważenia
        public float LoculusFeedIntake { get; set; } // Pobranie paszy przez komorę
        public float FeedIntakeWeekly { get; set; } //Pobranie paszy, kg/tydzień na komorę
        public float FeedIntakDaily { get; set; } //Pobranie paszy, kg/tydzień na komorę
        public float FeedConversionRatio { get; set; } // Wykorzystanie paszy, kg/kg
        public float AverageWeightGain { get; set; } //Średni przyrost z 2 klatek, kg/dzień

        public CageSurvey A { get; set; }  //kojec A
        public CageSurvey B { get; set; } //kojec B
                                          // jest statyczną publiczną klasą    TemplateCurve  

        public Survey()
        {
            DaysFromFirstWeight = 0;
        }
    }
}
