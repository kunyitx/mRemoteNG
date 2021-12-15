using mRemoteNG.Resources.Language;
using mRemoteNG.Tools;

namespace mRemoteNG.Connection.Protocol.RDP;

public enum RDGatewayUseConnectionCredentials
{
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.UseDifferentUsernameAndPassword))]
    No = 0,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.UseSameUsernameAndPassword))]
    Yes = 1,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.UseSmartCard))]
    SmartCard = 2
}