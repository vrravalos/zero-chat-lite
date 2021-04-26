using System;
using ChatLibrary.Domain;
using ChatLibrary.Extensions;
using ChatLibrary.Workflow;
using ChatLibrary.Workflow.Commands;
using NetMQ;
using NetMQ.Sockets;

namespace ChatLibrary.Messaging
{
    internal struct ChatEnvelope
    {
        internal DateTime Timestamp { get; set; }
        internal string RawAddress { get; set; }
        internal ChatMessage Message { get; set; }
    }
}
