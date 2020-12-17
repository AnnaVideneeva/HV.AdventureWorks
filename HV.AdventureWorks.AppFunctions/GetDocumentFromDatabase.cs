using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using HV.AdventureWorks.Services.Interfaces;

namespace HV.AdventureWorks.AppFunctions
{
    public class GetDocumentFromDatabase
    {
        private readonly IDocumentsService _documentsService;

        public GetDocumentFromDatabase(IDocumentsService documentsService)
        {
            _documentsService = documentsService;
        }

        [FunctionName(nameof(GetDocumentFromDatabase))]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Function is started");

            var guidString = req.Query["guid"];

            if (guidString.Count == 0)
            {
                log.LogInformation("Guid parameter is missed");

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            Guid guid = Guid.Parse(guidString);

            var document = _documentsService.GetByGuid(guid);

            log.LogInformation("Document is got with name " + document.FileName + document.FileExtension);

            if (document == null)
            {
                log.LogInformation("Document is not found with following giud " + guidString);

                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new MemoryStream(document.File));
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = document.FileName + document.FileExtension
            };

            log.LogInformation("Function is finished");

            return response;
        }
    }
}
