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
        public int SurveyId { get; set; }
        [Required]
        [DisplayName("Cage quantity")]
        public int CageQuantity { get; set; } //Liczba sztuk w klatce 
        [Required]
        [DisplayName("Group weight")]
        public float GroupWeight { get; set; } //Masa ciała grupy klatki, kg/grupę
        public int DeathCount { get; set; }
        public float IndividualBodyWeight { get; set; } // Masa ciała sztuki, kg/szt.
        public float DifferenceInBodyWeight { get; set; } // Różnica w wadze, kg/tydzień
        public float WeightGainFromStart { get; set; } //Przyrost od wstawienia, kg/dzień
        public float WeightGainFromLastSurvey { get; set; } //Przyrost od poprzedniego ważenia, kg/dzień
   
        public CageSurvey(CageSurveyDTO cageSurveyDTO)
        {
            CageQuantity = (int)cageSurveyDTO.CageQuantity;
            GroupWeight = (int)cageSurveyDTO.GroupWeight;
            DeathCount = cageSurveyDTO.DeathCount;
            IndividualBodyWeight = cageSurveyDTO.IndividualBodyWeight;
            DifferenceInBodyWeight = cageSurveyDTO.DifferenceInBodyWeight;
            WeightGainFromStart = cageSurveyDTO.WeightGainFromStart;
            WeightGainFromLastSurvey = cageSurveyDTO.WeightGainFromLastSurvey;
        }
    }
}