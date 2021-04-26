using System;
using System.Threading;
using ChatLibrary.Constants;
using ChatLibrary.Domain;
using ChatLibrary.Messaging;
using ChatLibrary.Networking.Brokers;
using ChatLibrary.Networking.Common;
using ChatLibrary.UI;
using ChatLibrary.Workflow;
using ChatLibrary.Workflow.Commands;
using NetMQ.Sockets;

namespace ChatLibrary.Networking.ClientSocket
{
    /// <summary>
    /// Responsible to "think" and send commands to Server
    /// </summary>
    internal class ClientInternalSubscriber : Subscriber
    {
        private ClientMessageBroker broker;
        private DealerSocket dealer;

        public ClientInternalSubscriber(DealerSocket socket, string port, Publisher pub) : base(pub, null)
        {
            dealer = socket;
            userSession = UserSession.GetServerUser();
            chatRoom = new ChatRoom(userSession);
            broker = new ClientMessageBroker(chatRoom, socket, port);
        }

        internal override void HandleEnvelope(object sender, EnvelopeEventArgs e)
        {
            ChatEnvelope chatEnvelope = e.Envelope;
            ChatMessage chatMessage = chatEnvelope.Message;

            var cmdInvoker = new CommandsInvoker();
            var chatUserSession = base.chatRoom.UserSession;
            cmdInvoker.AddCommand(new CmdChatRegister(chatUserSession, chatMessage));
            cmdInvoker.AddCommand(new CmdReceiveChat(chatUserSession, chatMessage));
            cmdInvoker.AddCommand(new CmdReceiveBroadcastNewUser(chatUserSession, chatMessage));
            cmdInvoker.AddCommand(new CmdReceiveBroadcastEndSession(chatUserSession, chatMessage));
            cmdInvoker.InvokeAll();
        }

        internal void HandleConsoleLoop()
        {
            PingServer();

            RegisterUser();

            ConsoleLoop();
        }

        #region Private

        private void ConsoleLoop()
        {
            ConsoleUI.ShowText(Phrase.UsernamePrefix, new[] { userSession.UserName });
            string msgChat = Console.ReadLine();
            while (msgChat != "/exit")
            {
                var chatToServer = ChatMessageFactory.CreateDefault(Command.REQ_CHAT, Participant.CLIENT, userSession);
                chatToServer.Content = msgChat;

                broker.Client_SendMsg(dealer, chatToServer);

                ConsoleUI.ShowText(Phrase.UsernamePrefix, new[] { userSession.UserName });
                msgChat = Console.ReadLine();
            }

            // exit notification
            new CommandsInvoker(new CmdNotifyExitClient(broker)).Invoke();

            Thread.Sleep(2000);
        }

        private void PingServer()
        {
            new CommandsInvoker(new CmdFirstPing(this.broker)).Invoke();
        }

        private void RegisterUser()
        {
            new CommandsInvoker(new CmdNewUserRegistrationClient(this.broker)).Invoke();
        }

        #endregion Private
    }
}