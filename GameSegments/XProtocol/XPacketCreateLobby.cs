using System;
using XProtocol.Serializator;
using XClient;

namespace XProtocol
{
    public class XPacketCreateLobby
    { 
        [XField(2)]public Client HostUser;
    }
}