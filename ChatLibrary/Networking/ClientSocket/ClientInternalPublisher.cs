using ChatLibrary.Extensions;
using ChatLibrary.Messaging;
using ChatLibrary.Networking.Common;
using NetMQ;

namespace ChatLibrary.Networking.ClientSocket
{
    /// <summary>
    /// Responsible to receive communication from Server
    /// </summary>
    internal class ClientInternalPublisher : Publisher
    {
        internal void Socket_ReceiveReady(object sender, NetMQSocketEventArgs e)
        {
            string msgJson = e.Socket.ReceiveFrameString();

            if (!string.IsNullOrEmpty(msgJson))
            {
                ChatEnvelope envelope = new ChatEnvelope
                {
                    Message = msgJson.ToChatMessage()
                };

                this.Deliver(envelope);
            }
        }
    }
}