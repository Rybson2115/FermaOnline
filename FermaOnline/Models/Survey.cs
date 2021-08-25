using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DisplayName("Day of life")]
        public int DayOfLife { get; set; }//Dzień życia zwierzęcia 
        public float AverageBodyWeight { get; set; } //Średnia masa ciała kg/szt 

        [DisplayName("Loculus quantity")]
        public int LoculusQuantity { get; set; } //Liczba sztuk na komorze
        public int GroupId { get; set; } //id grupy zwierząt 
        public int DaysFromFirstWeight { get; set; }  //Ilość dni od pierwszego ważenia
        [DisplayName("Loculus feed intake")]
        public float LoculusFeedInTake { get; set; } // Pobranie paszy przez komorę
        public float FeedIntakeWeekly { get; set; } //Pobranie paszy, kg/tydzień na komorę
        public float FeedIntakDaily { get; set; } //Pobranie paszy, kg/dzien na komorę
        public float FeedConversionRatio { get; set; } // Wykorzystanie paszy, kg/kg
        public float AverageWeightGainFromCages { get; set; } //Średni przyrost z 2 klatek, kg/dzień
        public float AverageWeightGainFromLastSurvey { get; set; } //Średni przyrost z 2 klatek, od ost ważenia, kg/dzień
         public int ACageId { get; set; }
        public int BCageId { get; set; }

        public CageSurvey A { get; set; }  //kojec A
        public CageSurvey B { get; set; } //kojec B
        private Survey LastSurvey { get; set; } //Ostatni pomiar

        public Survey()
        {
            ExperymentId = 0;
            SurveyDate = new DateTime(0001, 01, 01, 00, 00, 00);
            DayOfLife = 0;
            AverageBodyWeight = 0.0f;
            LoculusQuantity = 0;
            GroupId = 0;
            DaysFromFirstWeight = 0;
            LoculusFeedInTake = 0.0f;
            FeedIntakeWeekly = 0.0f;
            FeedIntakDaily = 0.0f;
            FeedConversionRatio = 0.0f;
            AverageWeightGainFromCages = 0.0f;
            A = new CageSurvey();
            B = new CageSurvey();
            AverageWeightGainFromLastSurvey = 0.0f;
           ACageId = A.CageId;
           BCageId = B.CageId;
        }
        public Survey(int experymentId) : this()
        {
            ExperymentId = experymentId;
        }
        public Survey(Survey newSurvey) : this()
        {
            ExperymentId = newSurvey.ExperymentId;
            SurveyDate = newSurvey.SurveyDate;
            LoculusQuantity = newSurvey.LoculusQuantity;
            A.CageQuantity = newSurvey.A.CageQuantity;
            B.CageQuantity = newSurvey.B.CageQuantity;
            A.GroupWeight = newSurvey.A.GroupWeight;
            B.GroupWeight = newSurvey.B.GroupWeight;
            DayOfLife = newSurvey.DayOfLife;
            LoculusFeedInTake = newSurvey.LoculusFeedInTake;
            A.IndividualBodyWeight = A.GetIndividualBodyWeight();
            B.IndividualBodyWeight = B.GetIndividualBodyWeight();
            AverageBodyWeight = GetAverageBodyWeight();
        }

        public Survey(Survey newSurvey, Survey lastSurvey, float AFirstIndividualBodyWeight, float BFirstIndividualBodyWeight) : this(newSurvey)
        {
            LastSurvey = lastSurvey;
            DayOfLife = GetDayOfLife();
            DaysFromFirstWeight = GetDaysFromFirstWeight();
            A.DifferenceInBodyWeight = A.GetDifferenceInBodyWeight(LastSurvey.A.IndividualBodyWeight);
            B.DifferenceInBodyWeight = B.GetDifferenceInBodyWeight(LastSurvey.B.IndividualBodyWeight);
            A.WeightGainFromLastSurvey = A.GetWeightGainFromLastSurvey(LastSurvey.A.IndividualBodyWeight, DaysFromFirstWeight, LastSurvey.DaysFromFirstWeight);
            B.WeightGainFromLastSurvey = B.GetWeightGainFromLastSurvey(LastSurvey.B.IndividualBodyWeight, DaysFromFirstWeight, LastSurvey.DaysFromFirstWeight);
            A.WeightGainFromStart = A.GetWeightGainFromStart(DaysFromFirstWeight, AFirstIndividualBodyWeight);
            B.WeightGainFromStart = B.GetWeightGainFromStart(DaysFromFirstWeight, BFirstIndividualBodyWeight);
            AverageWeightGainFromCages = GetAverageWeightGain();
            FeedIntakeWeekly = GetFeedIntakeWeekly();
            FeedIntakDaily = GetFeedIntakDaily();
            FeedConversionRatio = GetFeedConversionRatio();
            AverageWeightGainFromLastSurvey = GetAverageWeightGainFromLastSurvey();
        }

        private int GetDayOfLife()
        {
            return LastSurvey.DayOfLife + (int)(SurveyDate - LastSurvey.SurveyDate).TotalDays;//Dzien zycia z ostatniego pomiaru + ilość dni między pomiarami 
        }
        private int GetDaysFromFirstWeight()
        {
            return LastSurvey.DaysFromFirstWeight + (int)(SurveyDate - LastSurvey.SurveyDate).TotalDays;//Ilość dni od pierwszego ważenia z ostatniego pomiaru + ilość dni między pomiarami 
        }
        private float GetAverageBodyWeight()
        {
            return (A.IndividualBodyWeight + B.IndividualBodyWeight) / 2;
        }
        private float GetFeedIntakeWeekly()
        {
            return LoculusFeedInTake - LastSurvey.LoculusFeedInTake;
        }
        private float GetFeedIntakDaily()
        {
            return FeedIntakeWeekly / LoculusQuantity / DaysFromFirstWeight;
        }
        private float GetAverageWeightGain()
        {
            return (A.WeightGainFromStart + B.WeightGainFromStart) / 2;
        }
        private float GetFeedConversionRatio()
        {
            return FeedIntakDaily / AverageWeightGainFromCages;
        }
        private float GetAverageWeightGainFromLastSurvey()
        {
            return (A.WeightGainFromLastSurvey + B.WeightGainFromLastSurvey) / 2;
        }
    }
}
