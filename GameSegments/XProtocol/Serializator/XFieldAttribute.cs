using System;

namespace XProtocol.Serializator
{
    [AttributeUsage(AttributeTargets.Field)]
    public class XFieldAttribute : Attribute
    {
        public byte FieldId { get; }

        public XFieldAttribute(byte fieldId) 
            => FieldId = fieldId;
    }
}
