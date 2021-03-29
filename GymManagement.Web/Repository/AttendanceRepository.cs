using GymManagement.Web.Models;
using GymManagement.Web.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Web.Repository
{
    public interface IAttendanceRepository
    {
        AttendanceResponseModel AddAttendance(Attendance attendanceModel);
        List<DateTime> GetUserAttendaceRepository(string selectedMonthYear, int regNumber);

    }
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly GymManagementContext _gymManagementContext;

        public AttendanceRepository(GymManagementContext gymManagementContext)
        {
            _gymManagementContext = gymManagementContext;
        }

        public AttendanceResponseModel AddAttendance(Attendance attendanceModel)
        {
            if (!_gymManagementContext.Attendance.Any(x => x.Date.ToString("MM/dd/yyyy").Equals(attendanceModel.Date.ToString("MM/dd/yyyy")) && x.RegistrationNumber == attendanceModel.RegistrationNumber))
            {
                _gymManagementContext.Attendance.Add(attendanceModel);
                _gymManagementContext.SaveChanges();
                return new AttendanceResponseModel
                {
                    Status = true,
                    Message = "Attendance Marked"
                };
            }
            else
            {
                return new AttendanceResponseModel
                {
                    Status = false,
                    Message = "Already present"
                };
            }
        }

        public List<DateTime> GetUserAttendaceRepository(string selectedMonthYear, int regNumber)
        {
            return _gymManagementContext.Attendance
                .Where(x => x.RegistrationNumber == regNumber && x.Date.ToString("yyyy-MM").Equals(selectedMonthYear))
                .Select(a => a.Date)
                .ToList();
        }
    }
}
