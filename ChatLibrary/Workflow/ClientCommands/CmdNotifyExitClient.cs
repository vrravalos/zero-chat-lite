using System;
using ChatLibrary.Constants;
using ChatLibrary.Messaging;
using ChatLibrary.Networking.Brokers;
using ChatLibrary.UI;
using ChatLibrary.Validation;
using NetMQ.Sockets;

namespace ChatLibrary.Workflow.Commands
{
    internal class CmdNotifyExitClient : IConditionalCommand
    {
        private ClientMessageBroker messageBroker;

        private readonly ChatValidator _validUserName = new ChatValidator(CheckList.ValidUserName);
        private readonly ChatValidator _validRegistration = new ChatValidator(CheckList.ValidRegistration);

        public CmdNotifyExitClient(ClientMessageBroker messageBroker)
        {
            this.messageBroker = messageBroker;
        }

        public bool ExecuteConditionalAction()
        {
            // last ping and good bye!
            var msgBye = ChatMessageFactory.CreateDefault(Command.REQ_END_SESSION, Participant.CLIENT, this.messageBroker.ChatRoom.UserSession);
            var dealer = (DealerSocket)messageBroker.Socket;
            
            messageBroker.Client_SendMsg(dealer, msgBye);

            int numChars = this.messageBroker.ChatRoom.UserSession.HasUserName() ? this.messageBroker.ChatRoom.UserSession.UserName.Length + 3 : 10;
            for (int i = 0; i < numChars; i++)
            {
                // backspace
                Console.Write("\b");
            }

            ConsoleUI.ShowTextLine(Phrase.EndSessionUsername);

            return true;
        }
    }
}