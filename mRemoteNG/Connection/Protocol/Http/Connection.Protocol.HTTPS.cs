namespace mRemoteNG.Connection.Protocol.Http;

public class ProtocolHTTPS : HTTPBase
{
    public enum Defaults
    {
        Port = 443
    }

    public ProtocolHTTPS(RenderingEngine RenderingEngine) : base(RenderingEngine)
    {
        httpOrS = "https";
        defaultPort = (int)Defaults.Port;
    }
}