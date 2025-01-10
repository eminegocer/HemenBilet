using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HemenBiletProje.Entities
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir")]
        public string Password { get; set; }
    }
}