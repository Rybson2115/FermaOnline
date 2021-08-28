using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FermaOnline.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
         public int ExperimentId { get; set; }
        public string FileName { get; set; }// path ~/img/ExperimentImages/@fileName 
        public Image(int experimentId, string fileName)
        {
            ExperimentId = experimentId;
            FileName =  fileName;
        }
    }
}
