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


        
        [NotMapped]
        public virtual Brand Brand { get; set; }//navigation prop for the brands

        [ForeignKey( nameof(Brand) )]
        public int BrandId { get; set; }



        [NotMapped]
        public virtual Order Order { get; set; }//navigation prop for the orders

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }


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

            return $"{this.Brand}, {this.ModelName}, {this.Processor}, {this.RAM}, {this.Storage}, RAM upgradeable: {answer}";
        }

    }
}
