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
        public DateTime? SurveyDate { get; set; }//Data pomiaru
        [Required]
        [DisplayName("Day of life")]
        public int? DayOfLife { get; set; }//Dzień życia zwierzęcia 
        public float AverageBodyWeight { get; set; } //Średnia masa ciała kg/szt 
        [Required]
        [DisplayName("Loculus quantity")]
        public int? LoculusQuantity { get; set; } //Liczba sztuk na komorze
        public int GroupId { get; set; } //id grupy zwierząt 
        public int DaysFromFirstWeight { get; set; }  //Ilość dni od pierwszego ważenia
        [Required]
        [DisplayName("Loculus feed intake")]
        public float? LoculusFeedInTake { get; set; } // Pobranie paszy przez komorę
        [DisplayName("Feed intake weekly")]
        public float FeedIntakeWeekly { get; set; } //Pobranie paszy, kg/tydzień na komorę
        [Required]
        [DisplayName("Feed intak daily")]
        public float? FeedIntakDaily { get; set; } //Pobranie paszy, kg/dzien na komorę
        public float FeedConversionRatio { get; set; } // Wykorzystanie paszy, kg/kg
        public float AverageWeightGainFromCages { get; set; } //Średni przyrost z 2 klatek, kg/dzień
        public float AverageWeightGainFromLastSurvey { get; set; } //Średni przyrost z 2 klatek, od ost ważenia, kg/dzień

        public List<CageSurvey> Cages { get; set; }
        private Survey LastSurvey { get; set; } //Ostatni pomiar

        public Survey(SurveyDTO surveyDTO)
        {
            ExperimentId = surveyDTO.ExperimentId;
            SurveyDate = surveyDTO.SurveyDate;
            DayOfLife = surveyDTO.DayOfLife;
            AverageBodyWeight = surveyDTO.AverageBodyWeight;
            LoculusQuantity = surveyDTO.LoculusQuantity;
            GroupId = surveyDTO.GroupId;
            DaysFromFirstWeight = surveyDTO.DaysFromFirstWeight;
            LoculusFeedInTake = surveyDTO.LoculusFeedInTake;
            FeedIntakeWeekly = surveyDTO.FeedIntakeWeekly;
            FeedIntakDaily = surveyDTO.FeedIntakDaily;
            FeedConversionRatio = surveyDTO.FeedConversionRatio;
            AverageWeightGainFromCages =surveyDTO.AverageWeightGainFromCages;
            Cages = surveyDTO.CagesDTOToCages(surveyDTO.Cages);
            AverageWeightGainFromLastSurvey = surveyDTO.AverageWeightGainFromLastSurvey;
        }
    }
}
