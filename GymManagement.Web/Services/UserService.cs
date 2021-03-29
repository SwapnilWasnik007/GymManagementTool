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
    public interface IUserService
    {
        bool AddUserService(UserView userView);
        List<DataTableUserView> GetUsers(UserPaginationModel paginationModel, out int recordsTotal);
        UserView GetUserByRegMobileService(int RegistrationNumber, long mobile);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool AddUserService(UserView userView)
        {
            Users users = ViewModelParser.GetUsers(userView);
            UserFees userFees = ViewModelParser.GetUserFees(users.Id, userView);
            users.RegistrationNumber = _userRepository.GetUserRegistrationNUmber() + 1;
            return _userRepository.AddUserRepository(users, userFees);
        }

        public UserView GetUserByRegMobileService(int registrationNumber, long mobile)
        {
            var data = _userRepository.GetUserByRegMobileRepository(registrationNumber, mobile);
            return ViewModelParser.GetFeeUserView(data);
        }

        public List<DataTableUserView> GetUsers(UserPaginationModel paginationModel, out int recordsTotal)
        {
            var allUsers = _userRepository.GetUsersRepository(paginationModel, out recordsTotal);
            return ViewModelParser.GetDTUserViews(allUsers);
        }
    }
}
