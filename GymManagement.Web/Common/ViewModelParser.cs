using GymManagement.Web.Models;
using GymManagement.Web.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Web.Common
{
    public static class ViewModelParser
    {
        public static Users GetUsers(UserView userView)
        {
            Users users = new Users();
            if (userView != null)
            {
                users.Id = Guid.NewGuid();
                users.Name = userView.Name;
                users.Mobile = userView.Mobile;
                users.Batch = userView.Batch;
                users.Address = userView.Address;
                users.CurrentPackage = userView.Package;
                users.StartDate = userView.StartDate;
                users.EndDate = userView.EndDate;
                users.BalanceAmount = userView.BalanceAmt;
                users.CreatedDateTime = DateTime.Now;
            }
            return users;
        }

        public static UserFees GetUserFees(Guid userId, UserView userView)
        {
            UserFees userFees = new UserFees();
            if (userView != null)
            {
                userFees.Id = Guid.NewGuid();
                userFees.TotalAmount = userView.TotalAmt;
                userFees.PaidAmount = userView.PaidAmt;
                userFees.BalanceAmount = userView.BalanceAmt;
                userFees.UserId = userId;
                userFees.PaidDate = DateTime.Now;
                userFees.Package = userView.Package;
                userFees.CreatedDateTime = DateTime.Now;
            }
            return userFees;
        }

        public static List<DataTableUserView> GetDTUserViews(List<Users> users)
        {
            List<DataTableUserView> userView = new List<DataTableUserView>();
            if (users.Any())
            {
                userView = users.Select(x =>
                new DataTableUserView
                {
                    RegistrationNumber = x.RegistrationNumber.ToString(),
                    Name = x.Name,
                    Mobile = x.Mobile,
                    Package = x.CurrentPackage,
                    BalanceAmt = x.BalanceAmount,
                    StartDate = x.StartDate.ToString("dd-MM-yyyy"),
                    EndDate = x.EndDate.ToString("dd-MM-yyyy"),
                }).ToList();
            }
            return userView;
        }

        public static UserView GetFeeUserView(UserFees user)
        {
            UserView userView = new UserView();
            if (user != null)
            {
                userView.RegistrationNumber = user.Users.RegistrationNumber;
                userView.Mobile = user.Users.Mobile;
                userView.Name = user.Users.Name;
                userView.Package = user.Package;
                userView.TotalAmt = user.TotalAmount;
                userView.BalanceAmt = user.Users.BalanceAmount;
                userView.EndDate = user.Users.EndDate;
            }
            return userView;
        }


        public static List<UserAttendanceModel> userAttendances(List<DateTime> dateTimes)
        {
            List<UserAttendanceModel> userAttendances = new List<UserAttendanceModel>();
            if (dateTimes != null)
            {
                userAttendances = dateTimes.Select(x =>
                new UserAttendanceModel
                {
                    Date = x.Day,
                    Time = x.ToString("hh:mm tt", CultureInfo.InvariantCulture)
                }).ToList();
            }
            return userAttendances;
        }
    }
}
