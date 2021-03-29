using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Web.Models
{
    public class AttendanceView
    {
    }

    public class AttendanceResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class UserAttendanceModel
    {
        public int Date { get; set; }
        public string Time { get; set; }
    }
}
