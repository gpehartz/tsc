using Microsoft.AspNet.Mvc;
using Tsc.Application;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    public class FileController
    {
        private TscApplication _application;

        public FileController()
        {
            _application = new TscApplication();
        }
        
        [HttpPost]
        public void Post([FromBody]string file)
        {
            _application.UploadFile(file);
        }
    }
}
