using System;
using System.Net.Sockets;

namespace TCPServer
{
    public class GameSession
    {
        public readonly Guid Guid;
        public readonly TcpClient HostUser;
        public TcpClient ConnectedUser { get; set; }
        public GameSession(Guid guid, TcpClient hostUser, TcpClient connectedUser)
        {
            Guid = guid;
            HostUser = hostUser;
            ConnectedUser = connectedUser;
        }
    }
}