using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FermaOnline.Models
{
    public class Experiment
    {
        [Key]
        public int ExperymentId { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }//Data pomiaru
 

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }//Data pomiaru
 
       
      
        public List<Survey> SurveysList { get; set; }
    }
}
