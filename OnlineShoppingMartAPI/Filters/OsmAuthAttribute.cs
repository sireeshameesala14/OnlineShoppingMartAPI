using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text;
using OnlineShoppingMartAPI.Utility;

namespace OnlineShoppingMartAPI.Filters
{
    public class OsmAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if(filterContext != null) 
            {
                Microsoft.Extensions.Primitives.StringValues authTokens;
                filterContext.HttpContext.Request.Headers.TryGetValue("Authorization",out authTokens);
                var _token = authTokens.FirstOrDefault();
                if(_token != null)
                {
                    if (IsValidateToken(_token))
                    {
                        filterContext.HttpContext.Response.Headers.Add("authToken", _token);
                        filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");
                        return;
                    }
                    else
                    {
                        filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");
                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                        filterContext.Result = new JsonResult("Please provide auth token")
                        {
                            Value = new
                            {
                                Status = "Error",
                                Message = "Invalid Token"
                            }
                        };
                    }

                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please provide auth Token";
                    filterContext.Result = new JsonResult("Please provide auth token")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Please provide auth token"
                        }
                    };
                       

                }
            }
        }
        public bool IsValidateToken(string authToken)
        {
            var token=authToken.Split(' ');
            var credential = Encoding.ASCII.GetString(Convert.FromBase64String(token[1])).Split(':');
            if (credential[0].ToUpper()==Configuration.ApiUserName.ToUpper() && credential[1].ToUpper() == Configuration.ApiPassword.ToUpper())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
