using System.Collections.Generic;
using System.Text;
using ChatLibrary.Domain;
using ChatLibrary.Extensions;
using ChatLibrary.Messaging;
using ChatLibrary.Workflow;
using ChatLibrary.Workflow.Commands;
using NetMQ;
using NetMQ.Sockets;

namespace ChatLibrary.Networking.Brokers
{
    internal abstract class BaseMessageBroker
    {
        private string _port;
        public string Port => _port;

        private ChatRoom _chatRoom;
        public ChatRoom ChatRoom => _chatRoom;

        private NetMQSocket _socket;
        public NetMQSocket Socket => _socket;

        public BaseMessageBroker(ChatRoom chatRoom)
        {
            _chatRoom = chatRoom;
        }

        public BaseMessageBroker(ChatRoom chatRoom, NetMQSocket socket, string port)
        {
            _chatRoom = chatRoom;
            SetNetwork(socket, port);
        }

        public void SetNetwork(NetMQSocket socket, string port)
        {
            _socket = socket;
            _port = port;
        }

        
    }
}