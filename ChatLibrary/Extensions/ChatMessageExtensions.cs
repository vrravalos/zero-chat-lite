using ChatLibrary.Messaging;

namespace ChatLibrary.Extensions
{
    public static class ChatMessageExtensions
    {
        public static string ToJson(this ChatMessage chatMessage)
        {
            return Utf8Json.JsonSerializer.ToJsonString<ChatMessage>(chatMessage);
        }

        public static ChatMessage ToChatMessage(this string jsonChatMessage)
        {
            return Utf8Json.JsonSerializer.Deserialize<ChatMessage>(jsonChatMessage);
        }
    }
}