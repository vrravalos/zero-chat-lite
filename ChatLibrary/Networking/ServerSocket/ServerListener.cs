using System;
using ChatLibrary.Extensions;
using ChatLibrary.Logging;
using ChatLibrary.Networking.Common;
using NetMQ;
using NetMQ.Sockets;
using static ChatLibrary.Logging.Logger;

namespace ChatLibrary.Networking.ServerSocket
{
    public class ServerListener : Listener
    {
        public override bool Start(string port = "33000")
        {
            bool hasStarted = true;

            try
            {
                using (var router = new RouterSocket($"@tcp://127.0.0.1:{port}"))
                {

                    LOG(LogLevel.VERBOSE, $"[SERVER] started at port:{port}");
                    LOG(LogLevel.DEBUG, "We are in DEBUG mode");

                    ServerInternalPublisher pub = new ServerInternalPublisher();
                    var sub = new ServerInternalSubscriber(router, port, pub);

                    // server continuous loop
                    while (true)
                    {
                        var clientMQMessage = router.ReceiveMultipartMessage();

                        if (clientMQMessage.HasExpectedFrames())
                        {
                            var envelope = clientMQMessage.GetChatEnvelope();

                            pub.Deliver(envelope);
                        }
                    }
                }
            }
            catch (AddressAlreadyInUseException)
            {
                //LOG(LogLevel.WARNING, $"[WARNING]: Address already in use");
                hasStarted = false;
            }
            catch (Exception ex)
            {
                LOG(LogLevel.ERROR, $"[EXCEPTION]: {ex.ToString()}");
                throw;
            }

            return hasStarted;
        }
    }
}