using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HV.AdventureWorks.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HV.AdventureWorks.Api.Controllers
{
    [Route("api/documents")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private string[] AllowedExtensions { get;  } = new string[] { ".docx", ".doc" };

        private readonly IDocumentsService _documentsService;

        public DocumentsController(IDocumentsService documentsService)
        {
            _documentsService = documentsService;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file, string documentNode)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is not selected");
            }

            var fileExtension = Path.GetExtension(file.FileName);

            if (!AllowedExtensions.Contains(fileExtension))
            {
                return BadRequest("File is not Word document");
            }

            byte[] fileBytes;

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            await _documentsService.UploadToBlobAsync(file.FileName, fileBytes, MimeTypes.GetMimeType(file.FileName), documentNode, fileExtension);

            return Ok();
        }

    }
}
