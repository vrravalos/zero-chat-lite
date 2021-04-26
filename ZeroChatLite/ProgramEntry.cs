using System;
using ChatLibrary.Networking.ClientSocket;
using ChatLibrary.Networking.ServerSocket;
using ZeroChatLite.Helpers;

namespace ZeroChatLite.UI
{
    public static class ProgramEntry
    {
        [STAThread]
        private static void Main(string[] args)
        {
            // Available Parameters (at the moment):
            // port: expected number

            var parameters = ArgsHelper.GetParams(args);

            ServerListener server = new ServerListener();

            if (!server.Start(parameters["port"]))
            {
                Console.WriteLine(":: Zero Chat Lite v1.0 ::");
                Console.WriteLine("Welcome to the chat room! To exit just type '/exit'");

                ClientListener client = new ClientListener();
                client.Start(parameters["port"]);
            }
        }
    }
}