using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;

namespace DataAccess
{
    public static class StorageConfig
    {
        public static void SetupAzureStorage(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            var tableClient = storageAccount.CreateCloudTableClient();
            var mailingListTable = tableClient.GetTableReference("mailinglist");
            var messageTable = tableClient.GetTableReference("message");
         
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference("azuremailblobcontainer");
            
            var queueClient = storageAccount.CreateCloudQueueClient();
            var subscribeQueue = queueClient.GetQueueReference("azuremailsubscribequeue");

            var tasks = new Task[]
            {
                Task.Factory.FromAsync(
                    mailingListTable.BeginCreateIfNotExists, 
                    (Func<IAsyncResult, bool>)mailingListTable.EndCreateIfNotExists, 
                    null
                ),
                Task.Factory.FromAsync(
                    messageTable.BeginCreateIfNotExists, 
                    (Func<IAsyncResult, bool>)messageTable.EndCreateIfNotExists, 
                    null
                ),
                Task.Factory.FromAsync(
                    blobContainer.BeginCreateIfNotExists, 
                    (Func<IAsyncResult, bool>)blobContainer.EndCreateIfNotExists, 
                    null
                ),
                Task.Factory.FromAsync(
                    subscribeQueue.BeginCreateIfNotExists, 
                    (Func<IAsyncResult, bool>)subscribeQueue.EndCreateIfNotExists, 
                    null
                )
            };

            Task.WaitAll(tasks);
        }
    }
}
