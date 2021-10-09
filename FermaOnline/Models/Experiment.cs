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
        public string Code { get; set; } //kod doświadczenia 
        [Required]
        [StringLength(256), MinLength(5)]
        public string Name { get; set; } //tutuł
        public bool Status { get; set; }
        [Required]
        public string Species { get; set; }
        [Required]
        public string Description { get; set; } // opis 
        [Required]
        public string ShortDescription { get; set; } // streszczenie 

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }//Data pomiaru

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }
        
        [NotMapped]
        public List<float> CageFirstIndividualBodyWeight { get; set; }
 

        public List<Survey> SurveysList { get; set; }
        public List<Image> Images { get; set; }
        [Required]
        public int? CageNumber { get; set; }
        public Experiment()
        {
            Name = string.Empty;
            Status = false;//brak dodanych pomiarów nie rozpoczety
            Species = string.Empty;
            Description = string.Empty;
            Start = new DateTime(0001, 01, 01, 00, 00, 00);//ta data jako null
            End = new DateTime(0001, 01, 01, 00, 00, 00);
            SurveysList = null;
            CageFirstIndividualBodyWeight = new List<float>();
            Images = new List<Image>();
            CageNumber = 0;
            Code = string.Empty;
            ShortDescription = string.Empty;
        }
        public Experiment(string name,string description, string shortDescription, string species,int cageNumber) : base()
        {
            Name = name;
            Description = description;
            Species = species;
            CageNumber = cageNumber;
            ShortDescription = shortDescription;
        }
        public Experiment(string name, string description, string shortDescription, string species,int cageNumber, List<Image> images) : this( name,  description, shortDescription,  species, cageNumber)
        {
            Images = images;
        }

    }
}
