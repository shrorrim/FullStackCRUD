using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Laptop> OrderedLaptops { get; set; } // nav prop

        [JsonIgnore]
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


        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Orderer))
            {
                return false;
            }
            else
            {
                return this.Name == (obj as Orderer).Name
                && this.PhoneNumber == (obj as Orderer).PhoneNumber;
            }
            
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Name, this.PhoneNumber);
        }
    }
}
