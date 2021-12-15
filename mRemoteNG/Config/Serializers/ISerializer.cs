using System;

namespace mRemoteNG.Config.Serializers;

public interface ISerializer<in TIn, out TOut>
{
    Version Version { get; }
    TOut Serialize(TIn model);
}