using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
        public class CalculationModel
        {
            public int Id { get; set; }

            [Required]
            public string? Type { get; set; }

            [Required]
            public string? Expression { get; set; }

            [Required]
            public DateTime CreationDate { get; set; }

            public double Result { get; set; }
        }
 }

