using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WholesaleManagmentSys.Models.DTO;
using WholesaleManagmentSys.Repositrories.Abstract;

namespace WholesaleManagmentSys.Controllers
{
    public class UserAuth : Controller
    {
        private readonly IUserAuthService _service;
        public UserAuth(IUserAuthService service) 
        {
            this._service = service;
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationMod model)
        {
            if(!ModelState.IsValid)
                return View(model);
            model.Role = "User";
            var result = await _service.RegistrationAsync(model);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registration));
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginMod model)
        {
            if (!ModelState.IsValid) 
            {
            return View(model);
            }

            var result = await _service.LoginAsync(model);
            if(result.StatusCode==1) 
            {
                return RedirectToAction("Display", "Dashboard");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        [Authorize]
        public async Task Logout()
        {
            await _service.LogoutAsync();
        }

        public async Task<IActionResult> Reg()
        {
            var model = new RegistrationMod
            {
                UserName = "admin",
                ShopName = "Prabin",
                Email = "neupanep89@gmail.com",
                Password = "Admin@123!"
            };
            model.Role = "admin";
            var result = await _service.RegistrationAsync(model);
            result Ok(result);
        }
    }
}
