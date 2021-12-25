using System;

namespace Server
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "XServer";
            Console.ForegroundColor = ConsoleColor.White;

            var server = new Server();
            server.Start();
            server.AcceptClients();
        }
    }
}