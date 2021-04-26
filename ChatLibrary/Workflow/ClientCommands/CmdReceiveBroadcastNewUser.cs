using System;
using ChatLibrary.Constants;
using ChatLibrary.Domain;
using ChatLibrary.Messaging;
using ChatLibrary.UI;

namespace ChatLibrary.Workflow.Commands
{
    internal class CmdReceiveBroadcastNewUser : IConditionalCommand
    {
        private readonly UserSession _chatUserSession;
        private ChatMessage _chatMessage;

        internal CmdReceiveBroadcastNewUser(UserSession chatSession, ChatMessage chatMessage)
        {
            _chatUserSession = chatSession;
            _chatMessage = chatMessage;
        }

        public bool ExecuteConditionalAction()
        {
            if (_chatMessage.Command == Command.BROADCAST_NEW_USER
                //&& _chatMessage.UserName != _chatUserSession.UserName)
                && _chatMessage.UserGuid != _chatUserSession.UserGuid)
            {
                // erasing current user message input (current usernamer or '@{GUEST}'
                int numChars = _chatUserSession.HasUserName() ? _chatUserSession.UserName.Length + 3 : 10;
                for (int i = 0; i < numChars; i++)
                {
                    // backspace
                    Console.Write("\b");
                }

                ConsoleUI.ShowTextLine(Phrase.BroadcastOtherUserHasJoined, new[] { _chatMessage.UserName });
                if (_chatUserSession.HasUserName())
                    ConsoleUI.ShowText(Phrase.UsernamePrefix, new[] { _chatUserSession.UserName });
                else
                    ConsoleUI.ShowText(Phrase.GuestPrefix);

                return true;
            }

            return false;
        }
    }
}