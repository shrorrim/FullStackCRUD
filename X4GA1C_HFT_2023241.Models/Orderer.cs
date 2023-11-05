using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4GA1C_HFT_2023241.Models
{
    [Table("Orderers")]
    public class Orderer : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        // who ordered
        [Required]
        [StringLength(100)]
        public string Name { get; set; }


        [StringLength(50)]
        public string PhoneNumber { get; set; }


        [NotMapped]
        public virtual ICollection<Laptop> OrderedLaptops { get; set; } // nav prop

        [NotMapped]
        public virtual ICollection<Order> Orders { get; set; } // nav prop fro orders


        public Orderer()
        {
            this.OrderedLaptops = new List<Laptop>();
            this.Orders = new List<Order>();
        }


        public override string ToString()
        {
            return $"{this.Id} {this.Name} {this.PhoneNumber}";
        }
    }
}
