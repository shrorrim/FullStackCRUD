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

        [StringLength(100)]
        [Required]
        public string Name { get; set; }


        [Required]
        public int YearOfAppearance { get; set; }

        [NotMapped]
        public virtual ICollection<Laptop> Laptops { get; set; }



        public Brand()
        {
            this.Laptops = new HashSet<Laptop>();
        }



        public override string ToString()
        {
            return $"{this.Name} {this.YearOfAppearance}";
        }

    }
}
