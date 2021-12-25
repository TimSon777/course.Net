using System;
using System.Threading;
using Protocol;

namespace Client
{
    internal class Program
    {
        private static int _handshakeMagic;

        private static void Main()
        {
            Console.Title = "XClient";
            Console.ForegroundColor = ConsoleColor.White;
            
            var client = new Client();
            client.OnPacketRecieve += OnPacketRecieve;
            client.Connect("127.0.0.1", 4910);

            var rand = new Random();
            _handshakeMagic = rand.Next();

            Thread.Sleep(1000);
            
            Console.WriteLine("Sending handshake packet..");

            client.QueuePacketSend(
                PacketConverter.Serialize(
                        PacketType.Handshake,
                        new XPacketHandshake
                        {
                            MagicHandshakeNumber = _handshakeMagic
                        })
                    .ToPacket());

            while(true) {}
        }

        private static void OnPacketRecieve(byte[] packet)
        {
            var isParsed = Packet.TryParse(packet, out var parsed);

            if (isParsed)
            {
                ProcessIncomingPacket(parsed);
            }
        }

        private static void ProcessIncomingPacket(Packet packet)
        {
            var type = PacketTypeManager.GetTypeFromPacket(packet);

            switch (type)
            {
                case PacketType.Handshake:
                    ProcessHandshake(packet);
                    break;
                case PacketType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void ProcessHandshake(Packet packet)
        {
            var handshake = PacketConverter.Deserialize<XPacketHandshake>(packet);

            if (_handshakeMagic - handshake.MagicHandshakeNumber == 15)
            {
                Console.WriteLine("Handshake successful!");
            }
        }
    }
}