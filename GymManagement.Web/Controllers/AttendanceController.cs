using GymManagement.Web.Models;
using GymManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Web.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddAttendance()
        {
            return View();
        }

        public string GetNameByRegNumb(int regNumber)
        {
            return _attendanceService.GetUserNameByRegNumb(regNumber);
        }

        public AttendanceResponseModel AddAttendanceByRegNumb(int regNumber)
        {
            return _attendanceService.AddAttendance(regNumber);
        }

        public IActionResult ViewUserAttendance(int regNumber)
        {
            ViewData["currentRegNumber"] = regNumber;
            return View();
        }

        public List<UserAttendanceModel> GetUserAttendance(string selectedMonthYear, int regNumber)
        {
            return _attendanceService.GetUserAttendance(selectedMonthYear, regNumber);
        }

        public IActionResult ViewAttendance()
        {
            return View();
        }
    }
}