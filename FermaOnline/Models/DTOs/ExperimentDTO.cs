using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FermaOnline.Models.DTOs
{
    public class ExperimentDTO 
    {
        public int Id { get; set; }
        public string Code { get; set; } //kod doświadczenia 
        public string Name { get; set; } //tutuł
        public bool Status { get; set; }
        public string Author { get; set; }
        public string Species { get; set; }
        public string Description { get; set; } // opis 
        public string ShortDescription { get; set; } // streszczenie 
        public DateTime Start { get; set; }//Data pomiaru
        public DateTime End { get; set; }
        public List<float> CageFirstIndividualBodyWeight { get; set; }
        public List<FileModel> Files { get; set; }
        public List<Survey> SurveysList { get; set; }
        public string VisibleProperties { get; set; }
        public int CageNumber { get; set; }
        public ExperimentDTO()
        {
            Name = string.Empty;
            Status = false;//brak dodanych pomiarów nie rozpoczety
            Species = string.Empty;
            Description = string.Empty;
            Start = new DateTime(0001, 01, 01);//ta data jako null
            End = new DateTime(0001, 01, 01);
            SurveysList = null;
            CageFirstIndividualBodyWeight = new List<float>();
            CageNumber = 0;
            Code = string.Empty;
            ShortDescription = string.Empty;
            Author = string.Empty;
            VisibleProperties = string.Empty;
        }
        public ExperimentDTO(string name, string description, string shortDescription, string species, int cageNumber, string author) : base()
        {
            Name = name;
            Description = description;
            Species = species;
            CageNumber = cageNumber;
            ShortDescription = shortDescription;
            Author = author;
            VisibleProperties = "000000000"; //"111111111"-wszystkie 
        }
    }
}
