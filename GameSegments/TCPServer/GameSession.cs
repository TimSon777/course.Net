using System;
using XClient;

namespace TCPServer
{
    public class GameSession
    {
        public readonly Guid Guid;
        public readonly Client HostUser;
        public Client ConnectedUser { get; set; }
        public GameSession(Guid guid, Client hostUser)
        {
            Guid = guid;
            HostUser = hostUser;
        }
    }
}