
using ETicaret.Entity.Featur;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ETicaret.EntityLayer
{
    public class Users:Featurs
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth{ get; set; }
        public string PhoneNumber { get; set; }
        public bool IsConfirmEmail { get; set; }
        public string role { get; set; }
        public List<Adresses> adresses{ get; set; }
    }
}