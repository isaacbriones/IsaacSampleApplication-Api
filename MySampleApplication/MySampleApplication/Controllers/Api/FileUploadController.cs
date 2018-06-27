using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using MySampleApplication.Models.Request;
using MySampleApplication.Models.Responses;
using MySampleApplication.Models.ViewModel;
using MySampleApplication.Services;

namespace MySampleApplication.Controllers.Api
{
    [RoutePrefix("api/fileupload")]
    public class FileUploadController : ApiController
    {
        private string bucketname = "sabio-training/Test";
        private IAmazonS3 awsS3Client = new AmazonS3Client(Amazon.RegionEndpoint.USWest2);

        [Route, HttpPost]
        public HttpResponseMessage UploadFile()
        {
            var httpPostedFile = HttpContext.Current.Request.Files[0];
            string fileName = Path.GetFileNameWithoutExtension(httpPostedFile.FileName);
            string extension = Path.GetExtension(httpPostedFile.FileName);
            var newGuid = Guid.NewGuid().ToString("");
            var newfileName = fileName + "_" + newGuid + extension;
            Stream st = httpPostedFile.InputStream;
            try
            {
                if (httpPostedFile != null)
                {
                    TransferUtility utility = new TransferUtility(awsS3Client);
                    TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();
                    request.BucketName = bucketname;
                    request.Key = newfileName;
                    request.InputStream = st;
                    utility.Upload(request); //File Streamed to AWS
                }
                if (ModelState.IsValid)
                {
                    FileUploadService svc = new FileUploadService();
                    FileUploadAddRequest model = new FileUploadAddRequest();
                    model.FileTypeId = 1;
                    model.UserFileName = fileName;
                    model.SystemFileName = newfileName;
                    model.Location = "https://sabio-training.s3.us-west-2.amazonaws.com/Test/" + newfileName;
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

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                FileUploadService svc = new FileUploadService();
                ItemResponse<FileUploadAddRequest> resp = new ItemResponse<FileUploadAddRequest>();
                resp.Item = svc.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, ex);
            }
        }

        [Route,HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                FileUploadService svc = new FileUploadService(); 
                ItemsResponse<FileUploadViewModel> resp = new ItemsResponse<FileUploadViewModel>();
                resp.Items = svc.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{id}"),HttpDelete]
        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            try
            {
                FileUploadService svc = new FileUploadService();
                FileUploadAddRequest model = new FileUploadAddRequest();
                model = svc.SelectById(id);
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/upload"), model.SystemFileName);
                try
                {
                    var deleteObjectRequest = new DeleteObjectRequest
                    {
                        BucketName = bucketname,
                        Key = model.SystemFileName
                    };
                    await awsS3Client.DeleteObjectAsync(deleteObjectRequest);
                }
                catch (AmazonS3Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
                }

                if (File.Exists(fileSavePath))
                {
                    File.Delete(fileSavePath);
                }
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
