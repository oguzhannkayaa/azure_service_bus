using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusApp.Common
{
    public static class Constants
    {
        public const string ConnectionString = "";//azure queue connection string
        public const string OrderCreatedQueue = "OrderCreatedQueue";
        public const string OrderDeletedQueue = "OrderDeletedQueue";
        public const string OrderTopic = "OrderTopic";


    }
}
