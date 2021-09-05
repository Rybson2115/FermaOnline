using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FermaOnline.Models
{
    public class Experiment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(256),MinLength(5)]
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Species { get; set; }  
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }//Data pomiaru

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }
        public float AFirstIndividualBodyWeight { get; set; }
        public float BFirstIndividualBodyWeight { get; set; }

        public List<Survey> SurveysList { get; set; }
        public List<Image> Images { get; set; }

        public Experiment()
        {
            Name = string.Empty;
            Status = false;//brak dodanych pomiarów nie rozpoczety
            Species = string.Empty;
            Description = string.Empty;
            Start = new DateTime(0001, 01, 01, 00, 00, 00);//ta data jako null
            End = new DateTime(0001, 01, 01, 00, 00, 00);
            SurveysList = null;
            AFirstIndividualBodyWeight = 0.0f;
            BFirstIndividualBodyWeight = 0.0f;
            Images = new List<Image>();


        }
        public Experiment(string name,string description, string species) : base()
        {
            Name = name;
            Description = description;
            Species = species;
        }
        public Experiment(string name, string description, string species, List<Image> images) : this( name,  description,  species)
        {
            Images = images;
        }

    }
}
