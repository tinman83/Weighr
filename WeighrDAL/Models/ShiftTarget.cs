using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WeighrDAL.Models
{
   public class ShiftTarget
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ShiftTargetId { get; set; }

        [ForeignKey("Shift")]
        public int ShiftId { get; set; }
        public DateTime ShiftDate { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int TargetUnits { get; set; }
        public int UnitsDone { get; set; }


    }
}
