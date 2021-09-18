using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FermaOnline.Models
{
    public class CageFirstIndividualBodyWeight
    {
        [Key]
        public int Id { get; set;}
        public int ExperimentId { get; set; }
        public int CageId { get; set; }
        public float FirstIndividualBodyWeight { get; set; }

        public CageFirstIndividualBodyWeight(int experimentId, int cageId , float firstIndividualBodyWeight)
        {
            ExperimentId = experimentId;
            CageId = cageId;
            FirstIndividualBodyWeight = firstIndividualBodyWeight;
        }

    }
}
