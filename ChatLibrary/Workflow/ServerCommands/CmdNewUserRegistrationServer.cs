using ChatLibrary.Constants;
using ChatLibrary.Domain;
using ChatLibrary.Messaging;
using ChatLibrary.Networking.Brokers;
using ChatLibrary.Validation;
using NetMQ;
using NetMQ.Sockets;

namespace ChatLibrary.Workflow.Commands
{
    internal class CmdNewUserRegistrationServer : IConditionalCommand
    {
        private ServerMessageBroker messageBroker;
        private ChatMessage chatMessage;
        private string rawAddress;
        private readonly ChatValidator _validRegistration = new ChatValidator(CheckList.ValidRegistration);

        internal CmdNewUserRegistrationServer()
        { }

        public CmdNewUserRegistrationServer(ServerMessageBroker broker, ChatMessage userChatMessage, string rawAddress)
        {
            this.messageBroker = broker;
            this.chatMessage = userChatMessage;
            this.rawAddress = rawAddress;
        }

        public bool ExecuteConditionalAction()
        {
            // New user registration
            if (chatMessage.Validate(_validRegistration).IsValid)
            {
                UserSession chatUserReg = new UserSession(rawAddress.Replace("\0", ""));
                int ix = messageBroker.ChatRoom.ChatUserList.IndexOf(rawAddress);
                chatUserReg.SetUserName($"{chatMessage.UserName}{ix}");

                var msgResponseRegistration = ChatMessageFactory.CreateDefault(Command.REQ_REGISTER_USERNAME, Participant.SERVER, chatUserReg);

                var router = (RouterSocket)messageBroker.Socket;

                messageBroker.Server_SendReply(router, new NetMQFrame(rawAddress), msgResponseRegistration);

                var msgBroadcastNewUser = ChatMessageFactory.CreateDefault(Command.BROADCAST_NEW_USER, Participant.SERVER, chatUserReg);

                messageBroker.Server_BroadcastMessage(messageBroker.ChatRoom.ChatUserList, router, rawAddress, msgBroadcastNewUser);

                return true;
            }

            return false;
        }
    }
}