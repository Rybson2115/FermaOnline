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
 

        public Survey()
        {
            ExperymentId = 0;
            SurveyDate = new DateTime(0001, 01, 01, 00, 00, 00);
            DayOfLife = 0;
            AverageBodyWeight = 0.0f;
            LoculusQuantity = 0;
            GroupId = 0;
            DaysFromFirstWeight = 0;
            LoculusFeedIntake = 0.0f;
            FeedIntakeWeekly = 0.0f;
            FeedIntakDaily = 0.0f;
            FeedConversionRatio = 0.0f;
            AverageWeightGain = 0.0f;
            A = new CageSurvey();
            B = new CageSurvey();
        }
        public Survey(int experymentId):base()
        {
            ExperymentId = experymentId;
            
        }
    }
}
