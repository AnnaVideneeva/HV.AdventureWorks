using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HV.AdventureWorks.AzureStorage
{
    public interface IBlobService
    {
        Task<string> UploadAsync(string containerName, string fileName, byte[] file, string fileMimeType);
        Task<byte[]> DownloadAsync(string containerName, string storageFileName);
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

        public async Task<byte[]> DownloadAsync(string containerName, string storageFileName)
        {
            var cloudStorageAccount = StorageAccountFactory.Create(_connectionString);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(storageFileName);

            using (var stream = new MemoryStream())
            {
                await cloudBlockBlob.DownloadToStreamAsync(stream).ConfigureAwait(false);

                return stream.ToArray();
            }
        }
    }
}
