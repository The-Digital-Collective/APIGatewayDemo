using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPI.Authentication
{
    /// <summary>
    /// Decode the bearer token (authToken), extract the username and password
    /// and confirm this is an authorised user. If not return a 401 to the calling application
    /// </summary>
    public class BearerAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                var authToken = actionContext.Request.Headers.Authorization.Parameter;
                var decodeAuthToken = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                var arrUserNameandPassword = decodeAuthToken.Split(':');
                if (!IsAuthorisedUser(arrUserNameandPassword[0], arrUserNameandPassword[1]))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
        private bool IsAuthorisedUser(string Username, string Password2)
        {
            //To do: Remove hard coded validation data use either a
            //       database lookup or an authentication server. 

            return Username == "TestUserID" && Password2 == "@Test123";
        }
    }
}