using System;

namespace Protocol
{
    [AttributeUsage(AttributeTargets.Field)]
    public class FieldAttribute : Attribute
    {
        public byte Id { get; }

        public FieldAttribute(byte id)
        {
            Id = id;
        }
    }
}