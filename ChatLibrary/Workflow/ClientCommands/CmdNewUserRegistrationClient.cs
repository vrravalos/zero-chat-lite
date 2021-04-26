using System;
using System.Collections.Generic;
using System.Threading;
using ChatLibrary.Constants;
using ChatLibrary.Helpers;
using ChatLibrary.Messaging;
using ChatLibrary.Networking.Brokers;
using ChatLibrary.UI;
using ChatLibrary.Validation;
using NetMQ.Sockets;

namespace ChatLibrary.Workflow.Commands
{
    internal class CmdNewUserRegistrationClient : IConditionalCommand
    {
        private ClientMessageBroker messageBroker;
        private readonly ChatValidator validUserName = new ChatValidator(CheckList.ValidUserName);
        private readonly ChatValidator validRegistration = new ChatValidator(CheckList.ValidRegistration);

        public CmdNewUserRegistrationClient(ClientMessageBroker messageBroker)
        {
            this.messageBroker = messageBroker;
        }

        public bool ExecuteConditionalAction()
        {
            var regUserCmd = ChatMessageFactory.CreateDefault(Command.REQ_REGISTER_USERNAME,
                                        Participant.CLIENT, messageBroker.ChatRoom.UserSession);

            ConsoleUI.ShowText(Phrase.GuestPrefix);
            regUserCmd.UserName = Console.ReadLine();
            while (!regUserCmd.Validate(validUserName).IsValid)
            {
                ConsoleUI.ShowTextLine(Phrase.InvalidUsernameFormat);
                ConsoleUI.ShowText(Phrase.GuestPrefix);
                regUserCmd.UserName = Console.ReadLine().ToLower();
            }

            IEnumerable<string> errors;
            if (!regUserCmd.TryValidate(validRegistration, out errors))
            {
                ThrowExHelper.ThrowException($"ERROR:Invalid registration, ChatMessage:{regUserCmd}");
            }

            var dealer = (DealerSocket)messageBroker.Socket;
            //messageBroker.Client_SendMsg(messageBroker.ChatRoom.UserSession.StrUserGuid, dealer, regUserCmd);
            messageBroker.Client_SendMsg(dealer, regUserCmd);

            // waiting the username update from server..
            while (string.IsNullOrWhiteSpace(messageBroker.ChatRoom.UserSession.UserName))
            {
                Thread.Sleep(100);
            }

            ConsoleUI.ShowTextLine(Phrase.ThankYouNewUsername, new[] { messageBroker.ChatRoom.UserSession.UserName });

            return true;
        }
    }
}