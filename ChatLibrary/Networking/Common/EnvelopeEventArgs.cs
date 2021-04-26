using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLibrary.Messaging;

namespace ChatLibrary.Networking.Common
{
    // Store chat envelope
    internal class EnvelopeEventArgs : EventArgs
    {
        internal EnvelopeEventArgs(ChatEnvelope chatEnvelope)
        {
            Envelope = chatEnvelope;
        }

        internal ChatEnvelope Envelope { get; set; }
    }
}
