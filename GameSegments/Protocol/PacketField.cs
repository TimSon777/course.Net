namespace Protocol
{
    public class PacketField
    {
        public byte Id { get; set; }
        public byte Size { get; set; }
        public byte[] Contents { get; set; }
    }
}