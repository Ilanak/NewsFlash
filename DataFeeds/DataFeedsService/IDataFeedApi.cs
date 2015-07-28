﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFeedsService
{
    interface IDataFeedApi
    {
        Task<DataFeed[]> GetFeedsAsync(Topic topic, int maxResults, DateTime queryStartTime);
    }
}
