﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.Storage.Table;

namespace DataAccess
{
    public class AzureMailingListRepository : IMailingListRepository
    {
        private readonly Lazy<CloudTable> _mailingListTable;

        public AzureMailingListRepository(string storageConnectionString)
        {
            _mailingListTable = new Lazy<CloudTable>(() =>
            {
                var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
                var tableClient = storageAccount.CreateCloudTableClient();
                return tableClient.GetTableReference("mailinglist");
            });
        }

        public IEnumerable<MailingList> ListMailingLists()
        {
            var reqOptions = new TableRequestOptions()
            {
                MaximumExecutionTime = TimeSpan.FromSeconds(1.5),
                RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3)
            };
            try
            {
                var query = new TableQuery<Table.MailingList>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, "mailinglist"));
                var lists = _mailingListTable.Value.ExecuteQuery(query, reqOptions).Select(l => new MailingList
                {
                    Description = l.Description,
                    FromEmailAddress = l.FromEmailAddress,
                    ListName = l.PartitionKey
                }).ToList();
                return lists;
            }
            catch (StorageException se)
            {
                Trace.TraceError(se.Message);
                throw;
            }
        }

        public void AddMailingList(MailingList mailingList)
        {
            var tableObject = new Table.MailingList
            {
                RowKey = "mailinglist",
                PartitionKey = mailingList.ListName,
                Description = mailingList.Description,
                FromEmailAddress = mailingList.FromEmailAddress
            };
            var insertOperation = TableOperation.Insert(tableObject);
            _mailingListTable.Value.Execute(insertOperation);
        }
    }
}
