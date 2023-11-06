using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace X4GA1C_HFT_2023241.Models
{
    [Table("Brands")]
    public class Brand : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }


        [Required]
        public int YearOfAppearance { get; set; } 


        [NotMapped]
        public virtual ICollection<Laptop> Laptops { get; set; } // "container" for notebooks

        public Brand()
        {
            this.Laptops = new List<Laptop>();
        }



        public override string ToString()
        {
            return $"Id: {this.Id} {this.Name} {this.YearOfAppearance}";
        }

    }
}
