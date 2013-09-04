﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IMailingListRepository
    {
        IEnumerable<MailingList> ListMailingLists();

        void AddMailingList(MailingList mailingList);
    }
}
