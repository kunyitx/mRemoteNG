using mRemoteNG.Resources.Language;
using mRemoteNG.Tools;

namespace mRemoteNG.Connection.Protocol.RDP;

public enum RDPSoundQuality
{
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Dynamic))]
    Dynamic = 0,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Medium))]
    Medium = 1,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.High))]
    High = 2
}