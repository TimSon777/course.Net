using System;
using XProtocol.Serializator;

namespace XProtocol
{
    public class XPacketLobbyCreated
    {
        [XField(3)]public Guid LobbyGuid;
    }
}