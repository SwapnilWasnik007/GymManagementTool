using GymManagement.Web.Common;
using GymManagement.Web.Models;
using GymManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            ViewData["Membershippackages"] = ConfigurationLoader.membershipPackageViewModels;
            return View();
        }

        [HttpPost]
        public bool RegisterUser(string data)
        {
            UserView userView = JsonConvert.DeserializeObject<UserView>(data);
            return _userService.AddUserService(userView);
        }

        public IActionResult FeeUpdate()
        {
            ViewData["Membershippackages"] = ConfigurationLoader.membershipPackageViewModels;
            return View();
        }

        public JsonResult GetUserFeesData(int registrationNumber, long mobile)
        {
            return Json(_userService.GetUserByRegMobileService(registrationNumber, mobile));
        }
    }
}