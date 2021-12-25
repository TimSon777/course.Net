using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Protocol
{
    public class Packet
    {
        public byte PacketType { get; private set; }
        public byte PacketSubtype { get; private set; }
        public List<PacketField> Fields { get; set; } = new List<PacketField>();
        
        private Packet() 
        { }
 
        public static Packet Create(byte type, byte subtype)
            => new Packet 
            {
                PacketType = type,
                PacketSubtype = subtype
            };
        
        public byte[] ToPacket()
        {
            var ms = new MemoryStream();
 
            ms.Write(new byte[] {0xAF, 0xAA, 0xAF, PacketType, PacketSubtype}, 0, 5);
 
            var fields = Fields.OrderBy(field => field.Id);
 
            foreach (var field in fields)
            {
                ms.Write(new[] {field.Id, field.Size}, 0, 2);
                ms.Write(field.Contents, 0, field.Contents.Length);
            }
 
            ms.Write(new byte[] { 0xFF, 0x00 }, 0, 2);
 
            return ms.ToArray();
        }
        
        public static bool TryParse(byte[] packet, out Packet result)
        {
            result = null;
            
            if (packet.Length < 7)
            {
                return false;
            }
 
            if (packet[0] != 0xAF 
                || packet[1] != 0xAA 
                || packet[2] != 0xAF)
            {
                return false;
            }
 
            var mIndex = packet.Length - 1;
 
            if (packet[mIndex - 1] != 0xFF 
                || packet[mIndex] != 0x00)
            {
                return false;
            }
 
            var type = packet[3];
            var subtype = packet[4];
 
            var newPacket = Create(type, subtype);
            
            var fields = packet.Skip(5).ToArray();
 
            while (true)
            {
                if (fields.Length == 2)
                {
                    result = newPacket;
                    return true;
                }
 
                var id = fields[0];
                var size = fields[1];
 
                var contents = size != 0 
                    ? fields.Skip(2).Take(size).ToArray() 
                    : null;
 
                newPacket.Fields.Add(new PacketField
                {
                    Id = id,
                    Size = size,
                    Contents = contents
                });
 
                fields = fields.Skip(2 + size).ToArray();
            }
        }

        public PacketField FindField(byte id) => Fields.FirstOrDefault(field => field.Id == id);
        
        public bool HasField(byte id) => FindField(id) != null;
        
        public T GetValue<T>(byte id) where T : struct
        {
            var field = FindField(id);
 
            if (field == null)
            {
                throw new Exception($"Field with ID {id} wasn't found.");
            }
            
            var neededSize = Marshal.SizeOf(typeof(T));
 
            if (field.Size != neededSize)
            {
                throw new Exception(
                    $"Can't convert field to type {typeof(T).FullName}.\n" + $"We have {field.Size} bytes but we need exactly {neededSize}.");
            }
 
            return field.Contents.ByteArrayToFixedObject<T>();
        }
        
        public void SetValue(byte id, object structure)
        {
            if (!structure.GetType().IsValueType)
            {
                throw new Exception("Only value types are available.");
            }
 
            var field = FindField(id);
 
            if (field == null)
            {
                field = new PacketField
                {
                    Id = id
                };
 
                Fields.Add(field);
            }
 
            var bytes = structure.FixedObjectToByteArray();
 
            if (bytes.Length > byte.MaxValue)
            {
                throw new Exception("Object is too big. Max length is 255 bytes.");
            }
 
            field.Size = (byte) bytes.Length;
            field.Contents = bytes;
        }
    }
}