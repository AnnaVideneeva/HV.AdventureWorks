using System;
using HV.AdventureWorks.AzureStorage;
using HV.AdventureWorks.Services.Interfaces;
using HV.AdventureWorks.Services.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AutoMapper;
using HV.AdventureWorks.Data.Entities;
using HV.AdventureWorks.Data.Interfaces;

namespace HV.AdventureWorks.Services.Services
{
    public class DocumentsService : IDocumentsService
    {
        private const string ContainerName = "documents";
        private const string QueueName = "documents";

        private readonly IBlobService _blobService;
        private readonly IQueueService _queueService;
        private readonly IDocumentsProvider _documentsProvider;
        private readonly IMapper _mapper;

        public DocumentsService(
            IBlobService blobService,
            IQueueService queueService,
            IDocumentsProvider documentsProvider,
            IMapper mapper)
        {
            _blobService = blobService;
            _queueService = queueService;
            _documentsProvider = documentsProvider;
            _mapper = mapper;
        }

        public Document GetByGuid(Guid guid)
        {
            var entity = _documentsProvider.GetByGuid(guid);

            return _mapper.Map<Document>(entity);
        }

        public async Task UploadToBlobAsync(string fileName, byte[] file, string fileMimeType, string documentNode, string fileExtension)
        {
            var blobFileName = await _blobService.UploadAsync(ContainerName, fileName, file, fileMimeType);

            var documentMessage = new DocumentMessage()
            {
                FileName = fileName,
                BlobFileName = blobFileName,
                FileMimeType = fileMimeType,
                DocumentNode = documentNode,
                FileExtension = fileExtension
            };

            var message = JsonConvert.SerializeObject(documentMessage);

            await _queueService.InsertMessageAsync(QueueName, message);
        }

        public async Task UploadToDatabaseAsync(string fileName, byte[] file, string documentNode, string fileExtension)
        {
            var document = new Document()
            {
                DocumentNode = documentNode,
                Title = "Title",
                Owner = 1,
                FolderFlag = false,
                FileName = fileName,
                FileExtension = fileExtension,
                Revision = "0",
                ChangeNumber = 0,
                Status = 2,
                File = file,
                RowGuid = Guid.NewGuid(),
                ModifiedDate = DateTime.UtcNow
            };
            var documentEntity = _mapper.Map<DocumentEntity>(document);

            _documentsProvider.Create(documentEntity);
        }
    }
}
