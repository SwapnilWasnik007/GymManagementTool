using GymManagement.Web.Common;
using GymManagement.Web.Models;
using GymManagement.Web.Models.DBModel;
using GymManagement.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Web.Services
{
    public interface IAttendanceService
    {
        string GetUserNameByRegNumb(int registrationNumber);
        AttendanceResponseModel AddAttendance(int registrationNumber);
        List<UserAttendanceModel> GetUserAttendance(string selectedMonthYear, int regNumber);
    }

    public class AttendanceService : IAttendanceService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IUserRepository userRepository, IAttendanceRepository attendanceRepository)
        {
            _userRepository = userRepository;
            _attendanceRepository = attendanceRepository;
        }

        public AttendanceResponseModel AddAttendance(int registrationNumber)
        {
            Users user = _userRepository.GetUserByRegNumb(registrationNumber);
            if (user != null)
            {
                Attendance attendance = new Attendance
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    RegistrationNumber = registrationNumber,
                    Date = DateTime.Now,
                    CreatedOn = DateTime.Now
                };
                return _attendanceRepository.AddAttendance(attendance);
            }
            return new AttendanceResponseModel
            {
                Status = false,
                Message = "Enter correct registration number / Fingerprint does not matched"
            };
        }

        public List<UserAttendanceModel> GetUserAttendance(string selectedMonthYear, int regNumber)
        {
            var response = _attendanceRepository.GetUserAttendaceRepository(selectedMonthYear, regNumber);
            List<UserAttendanceModel> result = ViewModelParser.userAttendances(response);
            return result;
        }

        public string GetUserNameByRegNumb(int registrationNumber)
        {
            Users user = _userRepository.GetUserByRegNumb(registrationNumber);
            if (user != null)
            {
                return user.Name;
            }
            return "________________________";
        }
    }
}
