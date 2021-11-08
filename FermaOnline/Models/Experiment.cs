using FermaOnline.Models.DTOs;
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
    public class Experiment : PropertyChangedModel
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; } //kod doświadczenia 
        public string Name { get; set; } //tutuł
        public bool Status { get; set; }
        public string Author { get; set; }
        public string Species { get; set; }  
        public string Description { get; set; } // opis 
        public string ShortDescription { get; set; } // streszczenie 

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }//Data pomiaru

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }
 
        [NotMapped]
        public List<float> CageFirstIndividualBodyWeight { get; set; }
        [NotMapped]
        public List<FileModel> Files { get; set; }
        public List<Survey> SurveysList { get; set; }
        public  string  VisibleProperties { get; set; }
        public int CageNumber { get; set; }
        public Experiment(ExperimentDTO experimentDTO)
        {
            Name = experimentDTO.Name;
            Status = experimentDTO.Status;
            Species = experimentDTO.Species;
            Description = experimentDTO.Description;
            Start = experimentDTO.Start;
            End = experimentDTO.End;
            SurveysList = experimentDTO.SurveysList;
            CageFirstIndividualBodyWeight = experimentDTO.CageFirstIndividualBodyWeight;
            CageNumber = experimentDTO.CageNumber;
            Code = experimentDTO.Code;
            ShortDescription = experimentDTO.ShortDescription;
            Author = experimentDTO.Author;
            VisibleProperties = experimentDTO.VisibleProperties;
        }                       
    }
}
