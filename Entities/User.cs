using HemenBiletProje.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace HemenBiletProje.Entities
{
    public class User
    {
        [Key] // Birincil anahtar olduğunu belirtir
        public int UserId { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı gereklidir")] // Zorunlu alan
        [StringLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olabilir")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir")] // Zorunlu alan
        [StringLength(100, ErrorMessage = "Şifre en fazla 100 karakter olabilir")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email gereklidir")] // Zorunlu alan
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi girin")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarası girin")]
        public string Phone { get; set; }
        public virtual ICollection<Seat> ReservedSeats { get; set; }
        public User()
        {
            ReservedSeats = new List<Seat>();
        }
    }
}
