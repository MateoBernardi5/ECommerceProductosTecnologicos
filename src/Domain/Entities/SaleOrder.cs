using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SaleOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        [ForeignKey("ClientId")]
        public int ClientId { get; set; } // Clave foránea para la relación con Cliente
        public Client Client { get; set; }
        public ICollection<SaleOrderDetail> SaleOrderDetails { get; set; } = new List<SaleOrderDetail>();

    }
}
