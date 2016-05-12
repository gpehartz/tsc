using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using Microsoft.Net.Http;
using Tsc.Application;
using Microsoft.Net.Http.Server;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private TscApplication _application;

        public FileController()
        {
            _application = new TscApplication();
        }
        
        [HttpPost]
        public void Post()
        {
            Request.Form.Files[0].SaveAsAsync(@"c:\Temp\sdf.jpg");
            
            
            //string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads");
            //var provider = new MultipartFormDataStreamProvider(root);

            //var task = request.Content.ReadAsMultipartAsync(provider).
            //    ContinueWith<HttpResponseMessage>(o =>
            //    {

            //        string file1 = provider.BodyPartFileNames.First().Value;
            //// this is the file name on the server where the file was saved 

            //return new HttpResponseMessage()
            //        {
            //            Content = new StringContent("File uploaded.")
            //        };
            //    }
            //);

            //_application.UploadFile(files);
        }
    }
}
