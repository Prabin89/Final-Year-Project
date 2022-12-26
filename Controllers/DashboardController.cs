﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WholesaleManagmentSys.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Display()
        {
            return View();
        }
    }
}
