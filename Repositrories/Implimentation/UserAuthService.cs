using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WholesaleManagmentSys.Models.Database;
using WholesaleManagmentSys.Models.DTO;
using WholesaleManagmentSys.Repositrories.Abstract;

namespace WholesaleManagmentSys.Repositrories.Implimentation
{
    public class UserAuthService : IUserAuthService
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserAuthService(RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            this.signInManager= signInManager;
            this.userManager= userManager;
            this.roleManager= roleManager;
        }
        public async Task<Status> LoginAsync(LoginMod model)
        {
            var status = new Status(); 
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid UserName!!";
                return status;
            }
            // Matching the password
            if(!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid Password!!";
                return status; 
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                // Assigning Roles
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                foreach(var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role,userRole));
                }
                status.StatusCode = 1;
                status.Message = "logged in Successfully";
                return status;
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "The User is Locked Out!!";
                return status;
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "There is an Error on Logging in!!";
                return status;
            }

        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<Status> RegistrationAsync(RegistrationMod model)
        {
            var status = new Status();
            var UserExists = await userManager.FindByNameAsync(model.UserName);
            if(UserExists != null) 
            {
                status.StatusCode = 0;
                status.Message= "The user already exists.";
                return status;
            }
            AppUser user = new AppUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                ShopName = model.ShopName,
                Email = model.Email,
                UserName = model.UserName,
                PanNumber = model.PanNumber,
                EmailConfirmed = true,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded) 
            {
                status.StatusCode = 0;
                status.Message = "Unable to create user";
                return status;
            }
            //Managing the role
            if (!await roleManager.RoleExistsAsync(model.Role))
                await roleManager.CreateAsync(new IdentityRole(model.Role));

            if(await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            status.StatusCode = 1;
            status.Message = "The user is Registered successfully";
            return status; 

        }

        Task<Status> IUserAuthService.LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
