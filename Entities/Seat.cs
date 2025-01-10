using HemenBiletProje.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HemenBiletProje.Models;

namespace HemenBiletProje.Entities
{

    public class Seat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatId { get; set; }

        [Required]
        [StringLength(3)]
        public string SeatNumber { get; set; }

        public bool IsReserved { get; set; }

        public int? UserId { get; set; }

        [Required]
        public int FlightId { get; set; }
        // Navigation properties
        [ForeignKey("FlightId")]
        public virtual Flight Flight { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }

}