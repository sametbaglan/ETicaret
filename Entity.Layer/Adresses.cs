using System.ComponentModel.DataAnnotations;

namespace ETicaret.EntityLayer
{
    public class Adresses
    {
        [Key]
        public int AdressId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastname { get; set; }
        public int PhoneNumber { get; set; }
        public string AdressTitle { get; set; }
        public string Adress { get; set; }
        public int Userid { get; set; }
        public Users  users{ get; set; }
    }
}
