using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FermaOnline.Models
{
    public class CageSurvey
    {
        [Key]
        public int CageId { get; set; }
       
       

        [DisplayName("Cage quantity")]
        public int CageQuantity { get; set; } //Liczba sztuk w klatce 
        [DisplayName("Group weight")]
        public float GroupWeight { get; set; } //Masa ciała grupy klatki, kg/grupę
        public int DeathCount { get; set; }
        public float IndividualBodyWeight { get; set; } // Masa ciała sztuki, kg/szt.
        public float DifferenceInBodyWeight { get; set; } // Różnica w wadze, kg tydzień
        public float WeightGainFromStart { get; set; } //Przyrost od wstawienia, kg/dzień
        public float WeightGainFromLastSurvey { get; set; } //Przyrost od poprzedniego ważenia, kg/dzień
   
        public CageSurvey()
        {
            CageQuantity = 0;
            GroupWeight = 0.0f;
            DeathCount = 0;
            IndividualBodyWeight = 0.0f;
            DifferenceInBodyWeight = 0.0f;
            WeightGainFromStart = 0.0f;
            WeightGainFromLastSurvey = 0.0f;

        }
         public float GetIndividualBodyWeight()
         {
            return GroupWeight / CageQuantity;
         }
        public float GetDifferenceInBodyWeight(float LastIndividualBodyWeight)
        {
            return IndividualBodyWeight - LastIndividualBodyWeight;
        }
        public float GetWeightGainFromStart(int DaysFromFirstWeight,float FirstIndividualBodyWeight)
        {
            return IndividualBodyWeight-FirstIndividualBodyWeight / DaysFromFirstWeight;
        }
        public float GetWeightGainFromLastSurvey(float LastIndividualBodyWeight,int DaysFromFirstWeight, int LastDaysFromFirstWeight)
        {
            return (IndividualBodyWeight-LastIndividualBodyWeight) / (DaysFromFirstWeight - LastDaysFromFirstWeight);
        }
    }
}

