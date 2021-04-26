using System.Text;
using ChatLibrary.Messaging;
using NetMQ;

namespace ChatLibrary.Extensions
{
    internal static class NetMQExtensions
    {
        internal static string GetAddress(this NetMQFrame netMQFrame)
        {
            return netMQFrame.ConvertToString(Encoding.UTF8);
        }

        internal static bool HasExpectedFrames(this NetMQMessage netMQMessage)
        {
            // for Router-Dealer Socket we expect 3 frames
            return netMQMessage.FrameCount == 3;
        }

        internal static ChatMessage GetChatMessage(this NetMQMessage netMQMessage)
        {
            return netMQMessage[2].ConvertToString().ToChatMessage();
        }

        internal static ChatEnvelope GetChatEnvelope(this NetMQMessage netMQMessage)
        {
            return new ChatEnvelope
            {
                RawAddress = netMQMessage[0].GetAddress(),
                Message = GetChatMessage(netMQMessage),
            };
        }
    }
}