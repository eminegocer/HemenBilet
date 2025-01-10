using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Ticket
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } // Her bilet kartının benzersiz bir ID'si olacak
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int FlightId { get; set; } // Uçuşu bağlamak için
    public int SeatId { get; set; }   // Koltuğu bağlamak için
    public DateTime CreatedAt { get; set; } // Bilet kartı oluşturulma tarihi
}
