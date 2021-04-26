using ChatLibrary.Constants;
using ChatLibrary.Domain;
using ChatLibrary.Helpers;
using ChatLibrary.Messaging;

namespace ChatLibrary.Messaging
{
    internal static class ChatMessageFactory
    {
        internal static ChatMessage CreateDefault(Command command, Participant sender, UserSession chatUserOrigin = null)
        {
            if (command == Command.UNKNOWN)
            {
                ThrowExHelper.ThrowValidationException($"Invalid option for command:{command}");
            }

            return new ChatMessage
            {
                UserGuid = chatUserOrigin?.UserGuid ?? default,
                UserName = chatUserOrigin?.UserName ?? default,
                Command = command,
                Sender = sender
            };
        }
    }
}