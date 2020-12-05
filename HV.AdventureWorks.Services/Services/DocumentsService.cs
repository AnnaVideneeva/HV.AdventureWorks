using HV.AdventureWorks.AzureStorage;
using HV.AdventureWorks.Services.Interfaces;
using HV.AdventureWorks.Services.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HV.AdventureWorks.Services.Services
{
    public class DocumentsService : IDocumentsService
    {
        private const string ContainerName = "documents";
        private const string QueueName = "documents";

        private readonly IBlobService _blobService;
        private readonly IQueueService _queueService;

        public DocumentsService(
            IBlobService blobService,
            IQueueService queueService)
        {
            _blobService = blobService;
            _queueService = queueService;
        }

        public async Task UploadAsync(string fileName, byte[] file, string fileMimeType)
        {
            var blobFileName = await _blobService.UploadAsync(ContainerName, fileName, file, fileMimeType);

            var documentMessage = new DocumentMessage()
            {
                FileName = fileName,
                BlobFileName = blobFileName,
                FileMimeType = fileMimeType
            };

            await _queueService.InsertMessageAsync(QueueName, JsonConvert.SerializeObject(documentMessage));
        }
    }
}
