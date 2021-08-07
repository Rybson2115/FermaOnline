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

        public string Name { get; set; }
        public bool Status { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }//Data rozpoczęcia /Ryba
 

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }//Data zakończenia /Ryba
        public float AFirstIndividualBodyWeight { get; set; } //Co to, to chyba tutaj niekoniecznie chyba /Ryba
        public float BFirstIndividualBodyWeight { get; set; }
        public List<Survey> SurveysList { get; set; }

        public Experiment()
        {
            Name = string.Empty;
            Status = false;//brak dodanych pomiarów nie rozpoczety
            Start = new DateTime(0001, 01, 01, 00, 00, 00);//ta data jako null
            End = new DateTime(0001, 01, 01, 00, 00, 00);
            SurveysList = null;
            AFirstIndividualBodyWeight = 0.0f;
            BFirstIndividualBodyWeight = 0.0f;
        }
        public Experiment(string name):base()
        {
            // ExperymentId = this.GetHashCode(); pokminic nad id czy my sami nadajemy?
            // Można np  Guid.NewGuid() nie wiem jaka jest szansa na powtórzenie, Przykładowy guid: 2e63341a-e627-48ac-bb1a-9d56e2e9cc4f /Ryba
            Name = name;
        }
    }
}
