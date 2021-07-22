using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FermaOnline.Models
{
    public class Experiment
    {
        [Key]
        public int Id { get; set; }
        
        [DisplayName("Name")]
        public string Name { get; set; }
        public bool Status { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }//Data pomiaru
 

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }//Data pomiaru
 
        public List<Survey> SurveysList { get; set; }

        public Experiment()
        {
            Name = string.Empty;
            Status = false;//brak dodanych pomiarów nie rozpoczety
            Start = new DateTime(0001, 01, 01, 00, 00, 00);//ta data jako null
            End = new DateTime(0001, 01, 01, 00, 00, 00);
            SurveysList = null;
        }
        public Experiment(string name):base()
        {
           // ExperymentId = this.GetHashCode(); pokminic nad id czy my sami nadajemy? 
            Name = name;
        }
    }
}
