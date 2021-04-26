using System.Collections.Generic;
using System.Text;
using ChatLibrary.Domain;
using ChatLibrary.Extensions;
using ChatLibrary.Messaging;
using NetMQ;
using NetMQ.Sockets;

namespace ChatLibrary.Networking.Brokers
{
    internal class ServerMessageBroker : BaseMessageBroker
    {
        internal ServerMessageBroker(ChatRoom chatRoom, NetMQSocket socket, string port)
            : base(chatRoom, socket, port)
        {
        }

        // Way OUT: Sending information
        public void Server_SendReply(RouterSocket server, NetMQFrame clientAddress, ChatMessage message)
        {
            var messageToClient = new NetMQMessage();
            messageToClient.Append(clientAddress);
            messageToClient.AppendEmptyFrame();
            messageToClient.Append(message.ToJson(), Encoding.UTF8);
            server.SendMultipartMessage(messageToClient);
        }

        // Way OUT: Sending information
        internal void Server_BroadcastMessage(List<string> clientList, RouterSocket router,
                                                string rawAddress, ChatMessage chatMessage)
        {
            foreach (var c in clientList)
            {
                if (rawAddress != c)
                {
                    NetMQFrame anotherClientAddress = new NetMQFrame(c);
                    this.Server_SendReply(router, anotherClientAddress, chatMessage);
                }
            }
        }
    }
}