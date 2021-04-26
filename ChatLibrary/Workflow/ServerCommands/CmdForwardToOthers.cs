using ChatLibrary.Logging;
using ChatLibrary.Messaging;
using ChatLibrary.Networking.Brokers;
using ChatLibrary.Validation;
using NetMQ.Sockets;
using static ChatLibrary.Logging.Logger;

namespace ChatLibrary.Workflow.Commands
{
    internal class CmdForwardToOthers : IConditionalCommand
    {
        private ServerMessageBroker messageBroker;
        private ChatMessage userChatMessage;
        private string rawAddress;
        private readonly ChatValidator _validRegistration = new ChatValidator(CheckList.ValidRegistration);

        internal CmdForwardToOthers()
        { }

        public CmdForwardToOthers(ServerMessageBroker broker, ChatMessage userChatMessage, string rawAddress)
        {
            this.messageBroker = broker;
            this.userChatMessage = userChatMessage;
            this.rawAddress = rawAddress;
        }

        public bool ExecuteConditionalAction()
        {
            LOG(LogLevel.VERBOSE, $"Forward to other users: {messageBroker.ChatRoom.ChatUserList.Count - 1}");

            var router = (RouterSocket)messageBroker.Socket;
            messageBroker.Server_BroadcastMessage(messageBroker.ChatRoom.ChatUserList, router, rawAddress, userChatMessage);
            return true;
        }
    }
}