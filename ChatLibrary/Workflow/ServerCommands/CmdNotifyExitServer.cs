using ChatLibrary.Constants;
using ChatLibrary.Domain;
using ChatLibrary.Messaging;
using ChatLibrary.Networking.Brokers;
using ChatLibrary.Validation;
using NetMQ.Sockets;

namespace ChatLibrary.Workflow.Commands
{
    internal class CmdNotifyExitServer : IConditionalCommand
    {
        private ServerMessageBroker messageBroker;
        private ChatMessage chatMessage;
        private string rawAddress;

        private readonly ChatValidator validExit = new ChatValidator(CheckList.ValidExit);

        internal CmdNotifyExitServer()
        { }

        public CmdNotifyExitServer(ServerMessageBroker broker, ChatMessage userChatMessage, string rawAddress)
        {
            this.messageBroker = broker;
            this.chatMessage = userChatMessage;
            this.rawAddress = rawAddress;
        }

        public bool ExecuteConditionalAction()
        {
            // validating exit (to broadcast)
            if (chatMessage.Validate(validExit).IsValid)
            {
                UserSession chatUserReg = new UserSession(rawAddress.Replace("\0", ""));
                var router = (RouterSocket)messageBroker.Socket;

                var msgBroadcastExit = ChatMessageFactory.CreateDefault(Command.BROADCAST_END_SESSION, Participant.SERVER, chatUserReg);

                messageBroker.Server_BroadcastMessage(messageBroker.ChatRoom.ChatUserList, router, rawAddress, msgBroadcastExit);

                return true;
            }

            return false;
        }
    }
}