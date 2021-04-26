using ChatLibrary.Constants;
using ChatLibrary.Domain;
using ChatLibrary.Messaging;

namespace ChatLibrary.Workflow.Commands
{
    internal class CmdChatRegister : IConditionalCommand
    {
        private readonly UserSession _chatSession;
        private ChatMessage _chatMessage;

        internal CmdChatRegister(UserSession chatSession, ChatMessage chatMessage)
        {
            _chatSession = chatSession;
            _chatMessage = chatMessage;
        }


        public bool ExecuteConditionalAction()
        {
            if (_chatMessage.Command == Command.REQ_REGISTER_USERNAME && _chatMessage.UserName != default)
            {
                _chatSession.SetUserName(_chatMessage.UserName);
                return true;
            }

            return false;
        }
    }
}