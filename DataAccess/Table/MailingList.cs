using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace DataAccess.Table
{
    class MailingList : TableEntity
    {
        public string FromEmailAddress { get; set; }

        public string Description { get; set; }
    }
}
