using System.Runtime.InteropServices;

namespace Protocol
{
    public static class ArrayExtensions
    {
        public static T ByteArrayToFixedObject<T>(this byte[] bytes)
            where T: struct 
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
 
            try
            {
                return (T) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }
    }
}