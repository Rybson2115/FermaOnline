using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FermaOnline.Models
{
    public class Survey
    {
        [Key]
        public int SurveyId { get; set; }
        public int TestId { get; set; } 
        public DateTime SurveyDate { get; set; }//Data pomiaru
        public int DayOfLife { get; set; }//Dzień życia zwierzęcia 
        public int LoculusQuantity { get; set; } //Liczba sztuk na komorze
        public int GroupId { get; set; } //id grupy zwierząt 
        public int DaysFromFirstWeight { get; set; }  //Ilość dni od pierwszego ważenia
        public float LoculusFeedIntake { get; set; } // Pobranie paszy przez komorę
        public float FeedIntakeWeekly { get; set; } //Pobranie paszy, kg/tydzień na komorę
        public float FeedIntakDaily { get; set; } //Pobranie paszy, kg/tydzień na komorę
        public float FeedConversionRatio { get; set; } // Wykorzystanie paszy, kg/kg

        public Cage A { get; set; }  //kojec A
        public Cage B { get; set; } //kojec B
      // jest statyczną publiczną klasą    TemplateCurve  

        public Survey()
        {
            DaysFromFirstWeight = 0;
        }
    }
}
