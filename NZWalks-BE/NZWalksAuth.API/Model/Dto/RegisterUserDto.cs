using System.ComponentModel.DataAnnotations;

namespace NZWalksAuth.API.Models.DTO
{
    public class RegisterUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        public string[] Roles { get; set; }
    }
}
