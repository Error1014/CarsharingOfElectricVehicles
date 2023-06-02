using Chats.Service.Interfaces;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.IO;
using XAct;

namespace Chats.Api.Controllers
{
    public class FilesController : BaseApiController
    {
        readonly IBufferedFileUploadService _bufferedFileUploadService;

        public FilesController(IBufferedFileUploadService bufferedFileUploadService)
        {
            _bufferedFileUploadService = bufferedFileUploadService;
        }

        [HttpGet]
        public async Task<ActionResult> GetFiles(IFormFile file)
        {
            try
            {
                if (await _bufferedFileUploadService.UploadFile(file))
                {
                    ViewBag.Message = "File Upload Successful";
                }
                else
                {
                    ViewBag.Message = "File Upload Failed";
                }
            }
            catch (Exception ex)
            {
                //Log ex
                ViewBag.Message = "File Upload Failed";
            }
            return Ok(file);
        }

        
    }
}
