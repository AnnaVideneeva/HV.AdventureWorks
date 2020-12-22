using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using HV.AdventureWorks.Services.Interfaces;
using System.Net.Http;
using System.Net;
using System.Linq;

namespace HV.AdventureWorks.AppFunctions
{
    public class UploadDocumentToBlob
    {
        private string[] AllowedExtensions { get; } = new string[] { ".docx", ".doc" };

        private readonly IDocumentsService _documentsService;

        public UploadDocumentToBlob(IDocumentsService documentsService)
        {
            _documentsService = documentsService;
        }

        [FunctionName(nameof(UploadDocumentToBlob))]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Function is started");

            var file = req.Form?.Files?.FirstOrDefault();

            var documentNode = req.Query["documentNode"].ToString();

            if (file == null || file.Length == 0 || documentNode == null)
            {
                log.LogInformation("File is not selected or documentNode is not filled");

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var fileExtension = Path.GetExtension(file.FileName);

            if (!AllowedExtensions.Contains(fileExtension))
            {
                log.LogInformation("File is not Word document");

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            byte[] fileBytes;

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            await _documentsService.UploadToBlobAsync(file.FileName, fileBytes, MimeTypes.GetMimeType(file.FileName), documentNode, fileExtension);

            log.LogInformation($"File with '{file.FileName}' name uploaded to blob");
            log.LogInformation("Function is finished");

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
