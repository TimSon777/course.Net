using System;
using System.Collections.Generic;

namespace Protocol
{
    public class PacketTypeManager
    {
        private static readonly Dictionary<PacketType, Tuple<byte, byte>> Dictionary 
            = new Dictionary<PacketType, Tuple<byte, byte>>();
        
        public static void RegisterType(PacketType type, byte bType, byte bSubtype)
        {
            if (Dictionary.ContainsKey(type))
            {
                throw new Exception($"Packet type {type:G} is already registered.");
            }
 
            Dictionary.Add(type, Tuple.Create(bType, bSubtype));
        }
        

        public static Tuple<byte, byte> GetType(PacketType type)
        {
            if (!Dictionary.ContainsKey(type))
            {
                throw new Exception($"Packet type {type:G} is not registered.");
            }
 
            return Dictionary[type];
        }
        
        public static PacketType GetTypeFromPacket(Packet packet)
        {
            var type = packet.PacketType;
            var subtype = packet.PacketSubtype;
 
            foreach (var tuple in Dictionary)
            {
                var value = tuple.Value;
 
                if (value.Item1 == type && value.Item2 == subtype)
                {
                    return tuple.Key;
                }
            }
 
            return PacketType.Unknown;
        }
    }
}