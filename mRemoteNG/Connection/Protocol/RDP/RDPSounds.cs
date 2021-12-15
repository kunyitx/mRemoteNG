using mRemoteNG.Resources.Language;
using mRemoteNG.Tools;

namespace mRemoteNG.Connection.Protocol.RDP;

public enum RDPSounds
{
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.RdpSoundBringToThisComputer))]
    BringToThisComputer = 0,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.RdpSoundLeaveAtRemoteComputer))]
    LeaveAtRemoteComputer = 1,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.DoNotPlay))]
    DoNotPlay = 2
}