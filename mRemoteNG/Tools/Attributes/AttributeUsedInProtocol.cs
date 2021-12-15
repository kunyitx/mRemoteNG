using System;
using mRemoteNG.Connection.Protocol;

namespace mRemoteNG.Tools.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class AttributeUsedInProtocol : Attribute
{
    public AttributeUsedInProtocol(params ProtocolType[] supportedProtocolTypes)
    {
        SupportedProtocolTypes = supportedProtocolTypes;
    }

    public ProtocolType[] SupportedProtocolTypes { get; }
}