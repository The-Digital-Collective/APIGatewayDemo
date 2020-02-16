using LoginServiceDemo.Models;
using LoginServiceDemo.Repositories;
using LoginServiceDemo.UserData;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LoginServiceDemo.Controllers
{
    public class LogInController : ApiController
    {
        /// <summary>
        /// Authenticate the bearer token, if the token is okay then add generate a JWT token
        /// add the application ID and return to the calling application
        /// </summary>

        [HttpPost]
        [WebAPI.Authentication.BearerAuthentication]
        public HttpResponseMessage Login()
        {
            string secret = ConfigurationManager.AppSettings["KeyValue"];
            var applicationID = "APIGateway";
            return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(applicationID, secret));
        }
    }
}
