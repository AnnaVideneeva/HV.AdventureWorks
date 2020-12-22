using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Queues;

namespace HV.AdventureWorks.AzureStorage
{
    public interface IQueueService
    {
        Task InsertMessageAsync(string queueName, string message);
    }

    public class QueueService : IQueueService
    {
        private readonly string _connectionString;

        public QueueService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task InsertMessageAsync(string queueName, string message)
        {
            var queue = new QueueClient(
                _connectionString,
                queueName,
                new QueueClientOptions
                {
                    MessageEncoding = QueueMessageEncoding.Base64
                });

            await queue.CreateIfNotExistsAsync();
            await queue.SendMessageAsync(message);
        }
    }
}
