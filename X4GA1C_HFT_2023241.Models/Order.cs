using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace X4GA1C_HFT_2023241.Models
{
    [Table("Orders")]
    public class Order : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }


        [ForeignKey(nameof(Laptop))]
        public int LaptopId { get; set; }

        [ForeignKey(nameof(Orderer))]
        public int OrdererId { get; set; }


        [NotMapped]
        public virtual Laptop Laptop { get; set; } // nav prop

        [JsonIgnore]
        [NotMapped]
        public virtual Orderer Orderer { get; set; } // nav prop


        public override string ToString()
        {
            return $"OrderId: {this.Id} Date: {this.Date} LaptopId: {this.LaptopId} OrdererId: {this.OrdererId}";
        }

    }
}
