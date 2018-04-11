using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WeighrDAL.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        public int Status { get; set; }
        public Guid rowguid { get; set; }

    }
}
