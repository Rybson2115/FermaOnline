using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FermaOnline.Models
{
    public class FileModel
    {
        [Key]
        public int Id { get; set; }
        public int ExperimentId { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }// path ~/img/ExperimentImages/@fileName 
        public string FilePath { get; set; }
    }
}
