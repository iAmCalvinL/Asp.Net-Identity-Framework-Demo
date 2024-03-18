using System.ComponentModel.DataAnnotations;

namespace identity_demo.Dto
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}