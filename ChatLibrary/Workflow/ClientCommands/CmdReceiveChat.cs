using System;
using ChatLibrary.Constants;
using ChatLibrary.Domain;
using ChatLibrary.Messaging;
using ChatLibrary.UI;

namespace ChatLibrary.Workflow.Commands
{
    public class CmdReceiveChat : IConditionalCommand
    {
        private readonly UserSession _chatUserSession;
        private ChatMessage _chatMessage;

        public CmdReceiveChat(UserSession chatSession, ChatMessage chatMessage)
        {
            _chatUserSession = chatSession;
            _chatMessage = chatMessage;
        }

        public bool ExecuteConditionalAction()
        {
            if (_chatMessage.Command == Command.REQ_CHAT && _chatMessage.UserName != _chatUserSession.UserName)
            {
                //// erasing current user message input
                //for (int i = 0; i < _chatUserSession.UserName.Length + 3; i++)
                //{
                //    // backspace
                //    Console.Write("\b");
                //}

                // erasing current user message input (current usernamer or '@{GUEST}'
                int numChars = _chatUserSession.HasUserName() ? _chatUserSession.UserName.Length + 3 : 10;
                for (int i = 0; i < numChars; i++)
                {
                    // backspace
                    Console.Write("\b");
                }

                ConsoleUI.ShowText(Phrase.OtherUserPrefix, new[] { _chatMessage.UserName });
                ConsoleUI.ShowTextLine(Phrase.OtherUserMessage, new[] { _chatMessage.Content });
                
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