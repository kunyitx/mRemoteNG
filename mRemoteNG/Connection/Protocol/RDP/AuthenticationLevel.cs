using mRemoteNG.Resources.Language;
using mRemoteNG.Tools;

namespace mRemoteNG.Connection.Protocol.RDP;

public enum AuthenticationLevel
{
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.AlwaysConnectEvenIfAuthFails))]
    NoAuth = 0,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.DontConnectWhenAuthFails))]
    AuthRequired = 1,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.WarnIfAuthFails))]
    WarnOnFailedAuth = 2
}