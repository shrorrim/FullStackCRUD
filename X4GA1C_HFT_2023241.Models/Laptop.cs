using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4GA1C_HFT_2023241.Models
{
    [Table("Laptops")]
    public class Laptop : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ModelName { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Processor { get; set; }

        [Required]
        public int RAM { get; set; }

        [Required]
        public int Storage { get; set; }

        [Required]
        public bool RAM_Upgradeable { get; set; }

        [Required]
        public int Price { get; set; }


        [NotMapped]
        public virtual Brand Brand { get; set; }//navigation prop for the brands


        [ForeignKey( nameof(Brand) )]
        public int BrandId { get; set; }

        [NotMapped]
        public virtual ICollection<Orderer> Orderers { get; set; }//navigation prop for orderers

        [NotMapped]
        public virtual ICollection<Order> Orders { get; set; }//navigation prop for orders

        public override string ToString()
        {
            string answer = "";
            if (this.RAM_Upgradeable)
            {
                answer = "yes";
            }
            else
            {
                answer = "no";
            }

            return $"Id: {this.Id}, {this.Brand.Name}, {this.ModelName}, {this.Processor}," +
                $" {this.RAM}, {this.Storage}, RAM upgradeable: {answer}, {this.Price}, BrandId: {this.BrandId} ({this.Brand})";
        }

    }
}
