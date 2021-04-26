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
    internal class ClientMessageBroker : BaseMessageBroker
    {
        internal ClientMessageBroker(ChatRoom chatRoom, NetMQSocket socket, string port)
           : base(chatRoom, socket, port)
        {
        }

        public ClientMessageBroker(ChatRoom chatRoom) : base(chatRoom)
        {
        }

        // Way OUT: Sending information
        public void Client_SendMsg(DealerSocket client, ChatMessage chatMessage)
        {
            var messageToServer = new NetMQMessage();
            messageToServer.AppendEmptyFrame();
            messageToServer.Append(chatMessage.ToJson(), Encoding.UTF8);

            client.SendMultipartMessage(messageToServer);
        }

        // Way IN: Receiving information
        public void Client_ReceiveReady(object sender, NetMQSocketEventArgs e)
        {
            string msgJson = e.Socket.ReceiveFrameString();

            if (!string.IsNullOrEmpty(msgJson))
            {
                ChatMessage chatMessage = msgJson.ToChatMessage();

                var cmdInvoker = new CommandsInvoker();
                var chatUserSession = this.ChatRoom.UserSession;
                cmdInvoker.AddCommand(new CmdChatRegister(chatUserSession, chatMessage));
                cmdInvoker.AddCommand(new CmdReceiveChat(chatUserSession, chatMessage));
                cmdInvoker.AddCommand(new CmdReceiveBroadcastNewUser(chatUserSession, chatMessage));
                cmdInvoker.AddCommand(new CmdReceiveBroadcastEndSession(chatUserSession, chatMessage));
                cmdInvoker.InvokeAll();
            }
        }
    }
}