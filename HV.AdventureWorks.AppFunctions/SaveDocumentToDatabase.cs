using HV.AdventureWorks.AzureStorage;
using HV.AdventureWorks.Services.Interfaces;
using HV.AdventureWorks.Services.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HV.AdventureWorks.AppFunctions
{
    public class SaveDocumentToDatabase
    {
        private readonly IDocumentsService _documentsService;
        private readonly IBlobService _blobService;

        public SaveDocumentToDatabase(
            IDocumentsService documentsService,
            IBlobService blobService)
        {
            _documentsService = documentsService;
            _blobService = blobService;
        }

        [FunctionName(nameof(SaveDocumentToDatabase))]
        public async Task Run(
            [QueueTrigger("documents", Connection = "AzureStorageConnectionString")]string myQueueItem,
            ILogger log)
        {
            log.LogInformation("Function is started");
            log.LogInformation("Queue item is picked up: " + myQueueItem);

            var documentMessage = JsonConvert.DeserializeObject<DocumentMessage>(myQueueItem);

            var bytes = await _blobService.DownloadAsync("documents", documentMessage.BlobFileName);

            log.LogInformation("File is downloaded from blob");

            await _documentsService.UploadToDatabaseAsync(documentMessage.FileName, bytes, documentMessage.DocumentNode, documentMessage.FileExtension);

            log.LogInformation("Function is finished");
        }
    }
}
