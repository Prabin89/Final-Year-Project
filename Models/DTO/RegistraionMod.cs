using System.ComponentModel.DataAnnotations;

namespace WholesaleManagmentSys.Models.DTO
{
    public class RegistrationMod
    {
        [Required]
        public String ShopName { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        public int PanNumber{ get; set; }

        [Required]
        public String UserName { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[!@#$&()&^+]){.6,}$", ErrorMessage ="The password must be of minimum lent 6 and must contain 1 Uppercase, 1 Lowercase, 1 special charecter and 1 digit.")]
        public String Password { get; set; }

        [Required]
        [Compare("Password")]
        public String PasswordConfrim { get; set; }

        public String? Role { get; set; }
    }
}
