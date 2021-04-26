using ChatLibrary.Constants;
using ChatLibrary.Messaging;
using ChatLibrary.Networking.Brokers;
using ChatLibrary.UI;
using ChatLibrary.Validation;
using NetMQ.Sockets;

namespace ChatLibrary.Workflow.Commands
{
    internal class CmdFirstPing : IConditionalCommand
    {
        //private ChatRoom _chatRoom;

        private ClientMessageBroker messageBroker;
        private readonly ChatValidator _validRegistration = new ChatValidator(CheckList.ValidRegistration);

        internal CmdFirstPing()
        { }

        public CmdFirstPing(ClientMessageBroker messageBroker)
        {
            this.messageBroker = messageBroker;
        }

        public bool ExecuteConditionalAction()
        {
            // first ping
            var msgPing = ChatMessageFactory.CreateDefault(Command.REQ_PING, Participant.CLIENT);
            var dealer = (DealerSocket)messageBroker.Socket;
            
            messageBroker.Client_SendMsg(dealer, msgPing);
            ConsoleUI.ShowTextLine(Phrase.AskUsername);

            return true;
        }
    }
}