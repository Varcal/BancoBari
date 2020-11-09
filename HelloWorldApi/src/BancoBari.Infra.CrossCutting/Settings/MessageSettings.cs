using System.Collections.Generic;
using System.Linq;

namespace BancoBari.Infra.CrossCutting
{
    public class MessageSettings
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public IList<string> Queues { get; set; }


        public string GetQueueName(string queueName)
        {
            return Queues.FirstOrDefault(x => x == queueName);
        }
    }
}
