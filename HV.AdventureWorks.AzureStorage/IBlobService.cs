using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HV.AdventureWorks.AzureStorage
{
    public interface IBlobService
    {
        Task<string> UploadAsync(string containerName, string fileName, byte[] file, string fileMimeType);
    }

    public class BlobService : IBlobService
    {
        private readonly string _connectionString;

        public BlobService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<string> UploadAsync(string containerName, string fileName, byte[] file, string fileMimeType)
        {
            var cloudStorageAccount = StorageAccountFactory.Create(_connectionString);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            var storageFileName = fileName + '_' + Guid.NewGuid().ToString();
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(storageFileName);
            cloudBlockBlob.Properties.ContentType = fileMimeType;
            await cloudBlockBlob.UploadFromByteArrayAsync(file, 0, file.Length);

            return storageFileName;
        }
    }
}
