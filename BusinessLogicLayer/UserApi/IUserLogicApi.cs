using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;


namespace BusinessLogicLayer.UserApi
{
    public interface IUserLogicApi
    {
        UserAuthDetails GetUserAuthDetails(string userName, string password, string userType);
        User GetUserInformation(long userId);
        UserAuthDetails SaveUserRegistration(UserProfile userDetails);
        User UpdateUserDetails(User userDetails);
        bool IsUserExist(string userName);
    }
}
