using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WeighrDAL.Models
{
   public class AccumulatedWeight
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AccumulatedWeightId { get; set; }
        public decimal Weight { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}
