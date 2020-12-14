using System.ComponentModel.DataAnnotations;

namespace ClientBlazor.Wasm.Model
{
    public class SignInReuestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
