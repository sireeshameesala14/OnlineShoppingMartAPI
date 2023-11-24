using BusinessLogicLayer.Models;
using BusinessLogicLayer.UserApi;
using DataAccessLayer.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using OnlineShoppingMartAPI.Filters;
using OnlineShoppingMartAPI.Utility;


namespace OnlineShoppingMartAPI.Controllers
{
    [OsmAuth]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        
        private readonly ILogger<UserController> _logger;
        private readonly IUserLogicApi _userApi;        
        public UserController(ILogger<UserController> logger, IUserLogicApi userApi)
        {
            _logger = logger;
            _userApi = userApi;
            
        }
        [HttpGet]
        [Route("GetUserAuthStatus")]
        public IActionResult GetUserAuthInformation(string userName,string password,string userType) 
        {
            IActionResult response = null;
            _logger.LogInformation("GetUserAuthStatus is called.");
            try
            {
                
                var pass = Cryptography.Encrypt(password, Configuration.CryptoKey);
                var userAuthDetails= _userApi.GetUserAuthDetails(userName, pass, userType);
                response = Ok(userAuthDetails);
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception in GetUserAuthStatus : " + ex.Message);
                response = StatusCode(500, "There is some issue with application.Please try after sometime.");
            }
            return response;
            
        }

        [HttpGet]
        [Route("GetUserDetail")]
        public IActionResult GetUserInformation(long userId)
        {
            IActionResult response = null;
            try
            { 
                var userDetails= _userApi.GetUserInformation(userId);
                response = Ok(userDetails);
            }
            catch(Exception ex) 
            {
                _logger.LogError("Exception in GetUserDetails : " + ex.Message);
                response = StatusCode(500, "There is some issue with application.Please try after sometime.");
            }
            return response;
        }

        [HttpGet]
        [Route("GetEncryption")]
        public IActionResult GetEncryptionDetails(string text)
        {
            IActionResult response = null;
            try
            {
                var encrypt = Cryptography.Encrypt(text, Configuration.CryptoKey);
                response = Ok(encrypt);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetEncryption : " + ex.Message);
                response = StatusCode(500, "There is some issue with application.Please try after sometime.");
            }
            return response;
        }

        [HttpGet]
        [Route("GetDecryption")]
        public IActionResult GetDecryptionDetails(string text)
        {
            IActionResult response = null;
            try
            {
                var decrypt = Cryptography.Decrypt(text, Configuration.CryptoKey);
                response = Ok(decrypt);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetDecryption : " + ex.Message);
                response = StatusCode(500, "There is some issue with application.Please try after sometime.");
            }
            return response;
        }

        [Route("SaveUserRegistration")]
        [HttpPost]
        public IActionResult SaveUserRegistration([FromBody]UserProfile userProfile)
        {
            IActionResult actionResult = null;
            try
            {
                userProfile.Password = Cryptography.Encrypt(userProfile.Password, Configuration.CryptoKey);
                var UserInformation = _userApi.SaveUserRegistration(userProfile);
                actionResult = Ok(UserInformation);
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception in SaveUserRegistration : " + ex.Message);
                actionResult = StatusCode(500, "There is some technical issue.Please try after sometime");

            }
            return actionResult;
           
        }

        [Route("UpdateUser")]
        [HttpPost]
        public IActionResult UpdateUserDetail([FromBody]User userProfile)
        {
            IActionResult actionResult = null;
            try
            {
                var UserInformation = _userApi.UpdateUserDetails(userProfile);
                actionResult = Ok(UserInformation);
            }
            catch( Exception ex)
            {
                _logger.LogError("Exception in UpdateUser : " + ex.Message);
                actionResult= StatusCode(500, "There is some technical issue.Please try after sometime");

            }
            return actionResult;
        }

        [HttpGet]
        [Route("IsUserExist")]
        public IActionResult IsUserAvailable(string userName)
        {
            IActionResult response = null;
            try
            {
                var status = _userApi.IsUserExist(userName);
                response = Ok(status);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in IsUserAvailable : " + ex.Message);
                response = StatusCode(500, "There is some issue with application.Please try after some time.");

            }
            return response;
        }

    }
    }

