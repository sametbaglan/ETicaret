using Business.Layer;
using System;
using System.ComponentModel.DataAnnotations;

namespace ETicaret.UILayer.Models.IdentityModels
{
    public class RegisterModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DataType(DataType.EmailAddress,ErrorMessage ="E-mail adresinizi giriniz.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Şifrenizle Uyuşmuyor.")]
        public string RePassword { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string PhoneNumber { get; set; }
    }
}
