using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4GA1C_HFT_2023241.Models
{
    [Table("Orders")]
    public class Order : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        // who ordered
        [Required]
        [StringLength(100)]
        public string NameOfTheOrderer { get; set; }

        [NotMapped]
        public virtual ICollection<Laptop> Laptops { get; set; } // "container" for notebooks

        public Order()
        {
            this.Laptops = new HashSet<Laptop>();
        }

        [Required]
        public DateTime TimeOfTheOrder { get; set; }

        [Required]
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{this.NameOfTheOrderer} {this.TimeOfTheOrder} {this.Quantity}";
        }
    }
}
