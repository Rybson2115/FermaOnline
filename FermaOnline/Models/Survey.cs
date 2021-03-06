using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FermaOnline.Models
{
    public class Survey
    {
        [Key]
        public int SurveyId { get; set; }

        public int ExperimentId { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SurveyDate { get; set; }//Data pomiaru
        [Required]
        [DisplayName("Day of life")]
        public int DayOfLife { get; set; }//Dzień życia zwierzęcia 
        public float AverageBodyWeight { get; set; } //Średnia masa ciała kg/szt 
        [Required]
        [DisplayName("Loculus quantity")]
        public int LoculusQuantity { get; set; } //Liczba sztuk na komorze
        public int GroupId { get; set; } //id grupy zwierząt 
        public int DaysFromFirstWeight { get; set; }  //Ilość dni od pierwszego ważenia
        [Required]
        [DisplayName("Loculus feed intake")]
        public float LoculusFeedInTake { get; set; } // Pobranie paszy przez komorę
        [DisplayName("Feed intake weekly")]
        public float FeedIntakeWeekly { get; set; } //Pobranie paszy, kg/tydzień na komorę
        [Required]
        [DisplayName("Feed intak daily")]
        public float FeedIntakDaily { get; set; } //Pobranie paszy, kg/dzien na komorę
        public float FeedConversionRatio { get; set; } // Wykorzystanie paszy, kg/kg
        public float AverageWeightGainFromCages { get; set; } //Średni przyrost z 2 klatek, kg/dzień
        public float AverageWeightGainFromLastSurvey { get; set; } //Średni przyrost z 2 klatek, od ost ważenia, kg/dzień

        public List<CageSurvey> Cages { get; set; }
        private Survey LastSurvey { get; set; } //Ostatni pomiar

        public Survey()
        {
            ExperimentId = 0;
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
            Cages = new List<CageSurvey>();

            AverageWeightGainFromLastSurvey = 0.0f;

        }
        public Survey(int experymentId) : this()
        {
            ExperimentId = experymentId;
        }
        public Survey(Survey newSurvey) : this()
        {
            ExperimentId = newSurvey.ExperimentId;
            SurveyDate = newSurvey.SurveyDate;
            LoculusQuantity = newSurvey.LoculusQuantity;
            Cages = newSurvey.Cages;
            foreach (var cage in Cages)
                cage.IndividualBodyWeight = (float)Math.Round(cage.GetIndividualBodyWeight(), 2);
            DayOfLife = newSurvey.DayOfLife;
            LoculusFeedInTake = (float)Math.Round((float)newSurvey.LoculusFeedInTake, 2);
            AverageBodyWeight = (float)Math.Round(GetAverageBodyWeight(), 2);
        }

        public Survey(Survey newSurvey, Survey lastSurvey, List<float> cageFirstIndividualBodyWeight, int cageCount) : this(newSurvey)
        {
            LastSurvey = lastSurvey;
            DayOfLife = GetDayOfLife();
            DaysFromFirstWeight = GetDaysFromFirstWeight();
            for (int i = 0; i < cageCount; i++)
            {
                Cages[i].DifferenceInBodyWeight = (float)Math.Round(Cages[i].GetDifferenceInBodyWeight(LastSurvey.Cages[i].IndividualBodyWeight), 2);
                Cages[i].WeightGainFromLastSurvey = (float)Math.Round(Cages[i].GetWeightGainFromLastSurvey(LastSurvey.Cages[i].IndividualBodyWeight, DaysFromFirstWeight, LastSurvey.DaysFromFirstWeight), 2);
                Cages[i].WeightGainFromStart = (float)Math.Round(Cages[i].GetWeightGainFromStart(DaysFromFirstWeight, cageFirstIndividualBodyWeight[i]), 2);
            }
            AverageWeightGainFromCages = (float)Math.Round(GetAverageWeightGain(), 2);
            FeedIntakeWeekly = (float)Math.Round(GetFeedIntakeWeekly(), 2);
            FeedIntakDaily = (float)Math.Round(GetFeedIntakDaily(), 2);
            FeedConversionRatio = (float)Math.Round(GetFeedConversionRatio(), 2);
            AverageWeightGainFromLastSurvey = (float)Math.Round(GetAverageWeightGainFromLastSurvey(), 2);
        }

        private int GetDayOfLife()
        {
            return (int)LastSurvey.DayOfLife + (int)((DateTime)SurveyDate - (DateTime)LastSurvey.SurveyDate).TotalDays;//Dzien zycia z ostatniego pomiaru + ilość dni między pomiarami 
        }
        private int GetDaysFromFirstWeight()
        {
            return LastSurvey.DaysFromFirstWeight + (int)((DateTime)SurveyDate - (DateTime)LastSurvey.SurveyDate).TotalDays;//Ilość dni od pierwszego ważenia z ostatniego pomiaru + ilość dni między pomiarami 
        }
        private float GetAverageBodyWeight()
        {
            return Cages.Sum(c => c.IndividualBodyWeight) / Cages.Count;
        }
        private float GetFeedIntakeWeekly()
        {
            return (float)LoculusFeedInTake - (float)LastSurvey.LoculusFeedInTake;
        }
        private float GetFeedIntakDaily()
        {
            return (float)(FeedIntakeWeekly / LoculusQuantity) / (DaysFromFirstWeight - LastSurvey.DaysFromFirstWeight);
        }
        private float GetAverageWeightGain()
        {
            return Cages.Sum(c => c.WeightGainFromStart) / Cages.Count;
        }
        private float GetFeedConversionRatio()
        {
            return (float)FeedIntakDaily / AverageWeightGainFromCages;
        }
        private float GetAverageWeightGainFromLastSurvey()
        {
            return Cages.Sum(c => c.WeightGainFromLastSurvey) / Cages.Count;
        }
    }
}