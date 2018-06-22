using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySampleApplication.Models.Request;
using MySampleApplication.Models.Responses;
using MySampleApplication.Services;

namespace MySampleApplication.Controllers.Api
{
    [RoutePrefix("api/supplement")]
    public class SupplementController : ApiController
    {
        [Route,HttpPost,AllowAnonymous]
        public HttpResponseMessage InsertSupplement(SupplementAddRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                     SupplementService svc = new SupplementService();
                     int? id = svc.Insert(model);
                     ItemResponse<int?> resp = new ItemResponse<int?>();
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

        [Route("{Id:int}"), HttpPut,AllowAnonymous]
        public HttpResponseMessage UpdateSupplement(SupplementAddRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SupplementService svc = new SupplementService();
                    svc.Update(model);
                    SuccessResponse resp = new SuccessResponse();
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

        [Route("{Id:int}"), HttpGet,AllowAnonymous]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                SupplementService svc = new SupplementService();
                ItemResponse<SupplementAddRequest> resp = new ItemResponse<SupplementAddRequest>();
                resp.Item = svc.SelectById(id);

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route,HttpGet,AllowAnonymous]
        public HttpResponseMessage GetAll()
        {
            try
            {
                SupplementService svc = new SupplementService();
                ItemsResponse<SupplementAddRequest> resp = new ItemsResponse<SupplementAddRequest>();

                resp.Items = svc.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{Id:int}"),HttpDelete,AllowAnonymous]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                SupplementService svc = new SupplementService();
                svc.Delete(id);

                SuccessResponse resp = new SuccessResponse();

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
