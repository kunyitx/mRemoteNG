namespace mRemoteNG.Connection.Protocol.RAW;

public class RawProtocol : PuttyBase
{
    public enum Defaults
    {
        Port = 23
    }

    public RawProtocol()
    {
        PuttyProtocol = Putty_Protocol.raw;
    }
}