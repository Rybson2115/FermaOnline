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
        public List<Survey> SurveysList { get; set; }
    }
}
