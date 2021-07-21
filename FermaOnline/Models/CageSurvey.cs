using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FermaOnline.Models
{
    public class CageSurvey
    {
        [Key]
        public int CageId { get; set; }
        public int CageQuantity { get; set; } //Liczba sztuk w klatce 
        public float GroupWeight { get; set; } //Masa ciała grupy klatki, kg/grupę
        public int DeathCount { get; set; }
        public float IndividualBodyWeight { get; set; } // Masa ciała sztuki, kg/szt.
        public float DifferenceInBodyWeight { get; set; } // Różnica w wadze, kg tydzień
        public float WeightGainFromStart { get; set; } //Przyrost od wstawienia, kg/dzień
        public float WeightGainFromLastSurvey { get; set; } //Przyrost od poprzedniego ważenia, kg/dzień
        public float AverageWeightGainFromStart { get; set; } //Średni przyrost z 2 klatek, od ost ważenia, kg/dzień

        public CageSurvey()
        {
            CageId = this.GetHashCode();
        }

    }
}
