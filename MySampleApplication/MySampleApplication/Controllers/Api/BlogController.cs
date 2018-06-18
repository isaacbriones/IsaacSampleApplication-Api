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
    [RoutePrefix("api/blog")]
    public class BlogController : ApiController
    {
        [Route,HttpPost]
        public HttpResponseMessage AddBlog(BlogAddRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BlogService svc = new BlogService();

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

        [Route("{Id:int}"),HttpPut]
        public HttpResponseMessage UpdateBlog(BlogAddRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BlogService svc = new BlogService();
                    svc.Update(model);
                    SuccessResponse resp = new SuccessResponse();
                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{Id:int}"), HttpDelete]
        public HttpResponseMessage DeleteBlog(int Id)
        {
            try
            {
                BlogService svc = new BlogService();

                svc.Delete(Id);

                SuccessResponse resp = new SuccessResponse();

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{Id:int}"), HttpGet]
        public HttpResponseMessage GetBlogById(int id)
        {
            try
            {
                BlogService svc = new BlogService();
                ItemResponse<BlogAddRequest> resp = new ItemResponse<BlogAddRequest>();
                resp.Item= svc.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
                
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex);
            }

        }

        [Route,HttpGet]
        public HttpResponseMessage GetAllBlogs()
        {
            try
            {
                BlogService svc = new BlogService();
                ItemsResponse<BlogAddRequest> resp = new ItemsResponse<BlogAddRequest>();
                resp.Items = svc.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
