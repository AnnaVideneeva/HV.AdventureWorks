using Microsoft.WindowsAzure.Storage;

namespace HV.AdventureWorks.AzureStorage
{
    public static class StorageAccountFactory
    {
        public static CloudStorageAccount Create(string storageConnectionString)
        {
            return CloudStorageAccount.Parse(storageConnectionString);
        }
    }
}
