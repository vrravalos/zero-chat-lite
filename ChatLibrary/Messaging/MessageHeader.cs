using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLibrary.Extensions;

namespace ChatLibrary.Messaging
{
    [Serializable]
    public class MessageHeader
    {
        /// <summary>
        /// For new messages, we set id and time stamp,
        /// For recovered messages, the id and time stamp will be re-set
        /// </summary>

        public MessageHeader()
        {
            MessageId = Guid.NewGuid();

            Timestamp = DateTime.Now;
        }

        public Guid MessageId { get; set; }

        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return $"MessageId:{MessageId},DateTime:{Timestamp.ToTimestampString()},";
        }
    }
}
