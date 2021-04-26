using ChatLibrary.Domain;
using ChatLibrary.Extensions;
using ChatLibrary.Logging;
using ChatLibrary.Messaging;
using ChatLibrary.Networking.Brokers;
using ChatLibrary.Networking.Common;
using ChatLibrary.Workflow;
using ChatLibrary.Workflow.Commands;
using NetMQ.Sockets;
using static ChatLibrary.Logging.Logger;

namespace ChatLibrary.Networking.ServerSocket
{
    /// <summary>
    /// Responsible to "think" and send commands to Client
    /// </summary>
    internal class ServerInternalSubscriber : Subscriber
    {
        private ServerMessageBroker broker;

        public ServerInternalSubscriber(RouterSocket socket, string port, Publisher pub) : base(pub, null)
        {
            userSession = UserSession.GetServerUser();
            chatRoom = new ChatRoom(userSession);
            broker = new ServerMessageBroker(chatRoom, socket, port);
        }

        internal override void HandleEnvelope(object sender, EnvelopeEventArgs e)

        {
            ChatEnvelope chatEnvelope = e.Envelope;

            LOG(LogLevel.VERBOSE, $"Received this message: {chatEnvelope.Message.ToJson()}");

            if (broker.ChatRoom.UpdateUsers(chatEnvelope.RawAddress))
                return;

            // recovering chat message
            var rawAddress = chatEnvelope.RawAddress;
            var userChatMessage = chatEnvelope.Message;

            // if registration successful, skip
            if (new CommandsInvoker(new CmdNewUserRegistrationServer(broker, userChatMessage, rawAddress)).Invoke())
                return;

            // if notification exit, skip
            if (new CommandsInvoker(new CmdNotifyExitServer(broker, userChatMessage, rawAddress)).Invoke())
                return;

            // report number of knowing connected users
            broker.ChatRoom.ReportUsers();

            // forward all messages to other participants
            new CommandsInvoker(new CmdForwardToOthers(broker, userChatMessage, rawAddress)).Invoke();
        }
    }
}