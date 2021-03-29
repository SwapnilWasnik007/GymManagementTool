using GymManagement.Web.Models;
using GymManagement.Web.Models.DBModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GymManagement.Web.Repository
{
    public interface IUserRepository
    {
        bool AddUserRepository(Users userModel, UserFees userFees);
        int GetUserRegistrationNUmber();
        List<Users> GetUsersRepository(UserPaginationModel paginationModel, out int recordsTotal);

        UserFees GetUserByRegMobileRepository(int registrationNumber, long mobile);

        Users GetUserByRegNumb(int registrationNumber);
    }

    public class UserRepository : IUserRepository
    {
        private readonly GymManagementContext _gymManagementContext;

        public UserRepository(GymManagementContext gymManagementContext)
        {
            _gymManagementContext = gymManagementContext;
        }

        public bool AddUserRepository(Users userModel, UserFees userFeeModel)
        {
            userModel.UpdatedDateTime = null;
            _gymManagementContext.Users.Add(userModel);
            _gymManagementContext.UserFees.Add(userFeeModel);
            var rowCount = _gymManagementContext.SaveChanges();
            if (rowCount > 0)
            {
                return true;
            }
            return false;
        }

        public UserFees GetUserByRegMobileRepository(int registrationNumber, long mobile)
        {
            if (registrationNumber > 0)
            {
                return _gymManagementContext.UserFees.Include(x => x.Users)
                    .Where(a => a.Users.RegistrationNumber == registrationNumber).OrderByDescending(a => a.PaidDate).FirstOrDefault();
            }
            else
            {
                return _gymManagementContext.UserFees.Include(x => x.Users)
                    .Where(a => a.Users.Mobile == mobile).OrderByDescending(a => a.PaidDate).FirstOrDefault();
            }
        }

        public Users GetUserByRegNumb(int registrationNumber)
        {
            return _gymManagementContext.Users.Where(x => x.RegistrationNumber == registrationNumber).FirstOrDefault();
        }

        public int GetUserRegistrationNUmber()
        {
            if (_gymManagementContext.Users.Count() > 0)
            {
                return _gymManagementContext.Users.Max(x => x.RegistrationNumber);
            }
            else
            {
                return 0;
            }
        }

        public List<Users> GetUsersRepository(UserPaginationModel paginationModel, out int recordsTotal)
        {
            var userData = (from tempUsers in _gymManagementContext.Users
                            select tempUsers);

            userData = userData.OrderBy(paginationModel.SortColumn + " " + paginationModel.SortColumnDirection);

            //Search  
            if (!string.IsNullOrEmpty(paginationModel.SearchValue))
            {
                userData = userData.Where(m => m.Name.StartsWith(paginationModel.SearchValue));
            }

            //Total Records 
            recordsTotal = userData.Count();

            //Paging   
            var data = userData.Skip(paginationModel.Skip).Take(paginationModel.PageSize).ToList();
            return data;
        }
    }
}
