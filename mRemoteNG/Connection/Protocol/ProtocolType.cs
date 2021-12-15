using mRemoteNG.Resources.Language;
using mRemoteNG.Tools;

namespace mRemoteNG.Connection.Protocol;

public enum ProtocolType
{
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Rdp))]
    RDP = 0,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Vnc))]
    VNC = 1,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.SshV1))]
    SSH1 = 2,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.SshV2))]
    SSH2 = 3,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Telnet))]
    Telnet = 4,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Rlogin))]
    Rlogin = 5,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Raw))]
    RAW = 6,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Http))]
    HTTP = 7,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Https))]
    HTTPS = 8,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PowerShell))]
    PowerShell = 10,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.ExternalTool))]
    IntApp = 20
}