using System.ComponentModel.DataAnnotations;

namespace WholesaleManagmentSys.Models.DTO
{
    public class LoginMod
    {
        [Required]
        public String UserName{ get; set; }

        [Required]
        public String Password { get; set; }

    }
}
