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

        //what laptop (laptop id ...)

        [Required]
        [ForeignKey( nameof(Laptop) )]
        public int LaptopId { get; set; }

        [Required]
        public DateTime TimeOfTheOrder { get; set; }

        [Required]
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{this.NameOfTheOrderer} {this.TimeOfTheOrder} {this.LaptopId} {this.Quantity}";
        }
    }
}
