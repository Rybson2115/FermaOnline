using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FermaOnline.Models
{
    public class CageIndex
    {   
        [Key]
        public int Id { get; set; }
        public int CageId { get; set; }
        public int SurveyId { get; set; }
    }
}
