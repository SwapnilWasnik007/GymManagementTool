using GymManagement.Web.Models;
using GymManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoadData()
        {
            List<DataTableUserView> data = new List<DataTableUserView>();
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                // Skip number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();

                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();

                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                sortColumn = string.IsNullOrWhiteSpace(sortColumn) ? "RegistrationNumber" : sortColumn;
                // Sort Column Direction (asc, desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10, 20, 50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;

                int skip = start != null ? Convert.ToInt32(start) : 0;

                UserPaginationModel paginationModel = new UserPaginationModel
                {
                    SortColumn = sortColumn, //"RegistrationNumber"
                    SortColumnDirection = sortColumnDirection, // "asc"
                    SearchValue = searchValue, //""
                    PageSize = pageSize, //10
                    Skip = skip //0
                };

                // getting all Customer data  
                data = _userService.GetUsers(paginationModel, out int recordsTotal);
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                UserPaginationModel paginationModel = new UserPaginationModel
                {
                    SortColumn = "RegistrationNumber",
                    SortColumnDirection = "asc",
                    SearchValue = "",
                    PageSize = 10,
                    Skip = 0
                };
                data = _userService.GetUsers(paginationModel, out int recordsTotal);
                return Json(new { draw = "1", recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
