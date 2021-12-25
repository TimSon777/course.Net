using System.Runtime.InteropServices;

namespace Protocol
{
    public static class ObjectExtensions
    {
        public static byte[] FixedObjectToByteArray(this object value)
        {
            var rawSize = Marshal.SizeOf(value);
            var rawData = new byte[rawSize];

            var handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);

            Marshal.StructureToPtr(value, handle.AddrOfPinnedObject(), false);

            handle.Free();

            return rawData;
        }
    }
}