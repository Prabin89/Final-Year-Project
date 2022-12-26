using Microsoft.AspNetCore.Identity;

namespace WholesaleManagmentSys.Models.Database
{
    public class AppUser:IdentityUser
    {
        public string ShopName { get; set; }
        public int PanNumber{ get; set; }

        public string ? ProfilePic{ get; set; }
    }
}
