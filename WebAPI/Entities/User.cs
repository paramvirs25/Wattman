//using System.ComponentModel.DataAnnotations;

//namespace WebApi.Entities
//{
//    public class User
//    {
//        public int Id { get; set; }

//        [Required]
//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//        public string Username { get; set; }

//        public string Password { get; set; } //Created this property as a way around for testing. In production its recommended to use PasswordHash & PasswordSalt
//        public byte[] PasswordHash { get; set; }
//        public byte[] PasswordSalt { get; set; }
//    }
//}