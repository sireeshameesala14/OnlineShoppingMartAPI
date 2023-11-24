using BusinessLogicLayer.Models;
using DataAccessLayer.DBContext;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.UserApi
{
    public class UserLogicApi : IUserLogicApi
    {
        private readonly OSMDBContext _dbContext;
        public UserLogicApi(OSMDBContext dbContext)
        {
            _dbContext= dbContext;
        }
        public UserAuthDetails GetUserAuthDetails(string userName, string password, string userType) 
        { 
            UserAuthDetails authDetails= new UserAuthDetails();
            var loginDetails=_dbContext.LoginDetails.Where(m=>m.UserName.ToUpper()==userName.ToUpper() && m.Password.Trim() == password.Trim() && m.UserType.ToUpper() == userType.ToUpper()).FirstOrDefault();
            if (loginDetails != null && !string.IsNullOrEmpty(loginDetails.UserName)) 
            {
                authDetails.UserId = loginDetails.UserId;
                authDetails.IsAuthenticated = true;
                authDetails.UserType=loginDetails.UserType;
            }
            else
            {
                authDetails.UserId = 0;
                authDetails.IsAuthenticated = false;
            }
            return authDetails;
        }
        public User GetUserInformation(long userId)
        {
            User user = null;
            var userDetails = _dbContext.UserDetails.Where(m => m.UserId == userId).FirstOrDefault();
            if (userDetails != null)
            {
                user = new User();
                user.UserId = userDetails.UserId;
                user.FirstName = userDetails.FirstName;
                user.LastName = userDetails.LastName;
                user.Email = userDetails.Email;
                user.Mobile = userDetails.Mobile;
                user.Pin = userDetails.Pin;
                user.Address = userDetails.Address;
                user.City = userDetails.City;
                user.State = userDetails.State;
                user.Country = userDetails.Country;
                user.IsActive = userDetails.IsActive;
                user.CreatedBy = userDetails.CreatedBy;
                user.UpdatedBy = userDetails.UpdatedBy;
            }
            return user;
        }
        public UserAuthDetails SaveUserRegistration(UserProfile userDetails)
        {
            UserAuthDetails userAuthInformation = new UserAuthDetails();
            if (userDetails != null)
            {
                using(IDbContextTransaction transaction=_dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var user = new UserDetails();
                        user.FirstName = userDetails.FirstName;
                        user.LastName = userDetails.LastName;
                        user.Address = userDetails.Address;
                        user.City = userDetails.City;
                        user.State = userDetails.State;
                        user.Country = userDetails.Country;
                        user.Email = userDetails.Email;
                        user.Mobile = userDetails.Mobile;
                        user.Pin = userDetails.Pin;
                        user.IsActive = userDetails.IsActive;
                        user.CreatedBy = userDetails.UserId;
                        user.CreatedOn = DateTime.UtcNow;
                        user.UpdatedBy = userDetails.UserId;
                        user.UpdatedOn = DateTime.UtcNow;
                        _dbContext.UserDetails.Add(user);
                        _dbContext.SaveChanges();

                        var login = new LoginDetails();
                        login.UserName = userDetails.UserName;
                        login.Password = userDetails.Password;
                        login.UserType = userDetails.UserType;
                        login.UserId = user.UserId;
                        login.IsActive = userDetails.IsActive;
                        _dbContext.LoginDetails.Add(login);
                        _dbContext.SaveChanges();
                        transaction.Commit();
                        userAuthInformation.UserId = login.UserId;
                        userAuthInformation.IsAuthenticated = true;
                        userAuthInformation.UserType = login.UserType;
                       

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        userAuthInformation.UserId = 0;
                        userAuthInformation.IsAuthenticated = false;

                    }
                }
                
            }
            return userAuthInformation;
        }
        public User UpdateUserDetails(User userDetails)
        {
            User outResult = new User();
            if (userDetails != null && userDetails.UserId != 0)
            {
                var result = _dbContext.UserDetails.Where(m => m.UserId == userDetails.UserId).FirstOrDefault();
                if (result != null)
                {
                    _dbContext.UserDetails.Attach(result);
                    result.FirstName = userDetails.FirstName;
                    result.LastName = userDetails.LastName;
                    result.Address = userDetails.Address;
                    result.City = userDetails.City;
                    result.State = userDetails.State;
                    result.Country = userDetails.Country;
                    result.Email = userDetails.Email;
                    result.Mobile = userDetails.Mobile;
                    result.IsActive = true;
                    result.UpdatedOn = DateTime.UtcNow;
                    result.UpdatedBy = userDetails.UserId;
                    _dbContext.SaveChanges();
                    userDetails.IsActive = true;
                    outResult = userDetails;
                }
            }
            return outResult;
        }
        public bool IsUserExist(string userName)
        {
            bool status = false;
            if (!string.IsNullOrEmpty(userName))
            {

                var login = _dbContext.LoginDetails.Where(m => m.UserName.ToUpper() == userName.ToUpper()).FirstOrDefault();
                if (login != null && !string.IsNullOrEmpty(login.UserName))
                {
                    status = true;
                }
                else
                {
                    status = false;
                }

            }
            return status;

        }
    }     
}
