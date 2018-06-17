using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySampleApplication.Models.Request;
using MySampleApplication.Models.Responses;
using MySampleApplication.Models.ViewModel;
using MySampleApplication.Services;

namespace MySampleApplication.Controllers.Api
{
     [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [Route("register"), HttpPost, AllowAnonymous]
        public HttpResponseMessage Register(PersonAddRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PersonService svc = new PersonService();
                    int id = svc.Insert(model);

                    ItemResponse<int> resp = new ItemResponse<int>();
                    resp.Item = id;

                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("login"),HttpPost,AllowAnonymous]
        public HttpResponseMessage Login(LoginAddRequest model)
        {
            try
            {
                PersonService svc = new PersonService();
                LoginViewModel success = svc.Login(model);
                if (success != null)
                {
                    LoginResponse<LoginViewModel> resp = new LoginResponse<LoginViewModel>();
                    resp.Item = success;
                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
                else
                {
                    ErrorResponse resp = new ErrorResponse("Uncessful Login Attempt");
                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
