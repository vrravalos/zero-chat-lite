using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatLibrary.Logging;
using ChatLibrary.Networking.Common;
using NetMQ;
using NetMQ.Sockets;
using static ChatLibrary.Logging.Logger;

namespace ChatLibrary.Networking.ClientSocket
{
    public class ClientListener : Listener
    {
        public override bool Start(string port)
        {
            bool hasStarted = true;

            Guid chatUserId = Guid.NewGuid();
            var clientSocketPerThread = new ThreadLocal<DealerSocket>();

            using (var poller = new NetMQPoller())
            {
                List<Task> taskList = new List<Task>();
                var t = Task.Factory.StartNew(state =>
                {
                    try
                    {

                        string strChatUserId = state.ToString();

                        ClientInternalPublisher pub = new ClientInternalPublisher();

                        DealerSocket socket = null;
                        if (!clientSocketPerThread.IsValueCreated)
                        {
                            socket = new DealerSocket();
                            socket.Options.Identity = Encoding.Unicode.GetBytes(strChatUserId);
                            socket.Connect($"tcp://127.0.0.1:{port}");
                            socket.ReceiveReady += pub.Socket_ReceiveReady;
                            clientSocketPerThread.Value = socket;
                            poller.Add(socket);
                        }
                        else
                        {
                            socket = clientSocketPerThread.Value;
                        }

                        LOG(LogLevel.VERBOSE, $"[CLIENT] started at port:{port}");
                        LOG(LogLevel.DEBUG, "We are in DEBUG mode");

                        var sub = new ClientInternalSubscriber(socket, port, pub);

                        sub.HandleConsoleLoop();

                    }
                    catch (Exception ex)
                    {
                        LOG(LogLevel.ERROR, $"[EXCEPTION]: {ex.ToString()}" );
                        throw;
                    }

                }, chatUserId.ToString(), TaskCreationOptions.LongRunning);

                taskList.Add(t);

                // start the poller
                poller.RunAsync();
                Task.WaitAll(taskList.ToArray());
            }

            return hasStarted;
        }
    }
}