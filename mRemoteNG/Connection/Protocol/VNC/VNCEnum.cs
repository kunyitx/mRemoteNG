using System.ComponentModel;
using mRemoteNG.Resources.Language;
using mRemoteNG.Tools;

namespace mRemoteNG.Connection.Protocol.VNC;

public enum Defaults
{
    Port = 5900
}

public enum SpecialKeys
{
    CtrlAltDel,
    CtrlEsc
}

public enum Compression
{
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.NoCompression))]
    CompNone = 99,
    [Description("0")] Comp0 = 0,
    [Description("1")] Comp1 = 1,
    [Description("2")] Comp2 = 2,
    [Description("3")] Comp3 = 3,
    [Description("4")] Comp4 = 4,
    [Description("5")] Comp5 = 5,
    [Description("6")] Comp6 = 6,
    [Description("7")] Comp7 = 7,
    [Description("8")] Comp8 = 8,
    [Description("9")] Comp9 = 9
}

public enum Encoding
{
    [Description("Raw")] EncRaw,
    [Description("RRE")] EncRRE,
    [Description("CoRRE")] EncCorre,
    [Description("Hextile")] EncHextile,
    [Description("Zlib")] EncZlib,
    [Description("Tight")] EncTight,
    [Description("ZlibHex")] EncZLibHex,
    [Description("ZRLE")] EncZRLE
}

public enum AuthMode
{
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Vnc))]
    AuthVNC,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Windows))]
    AuthWin
}

public enum ProxyType
{
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.None))]
    ProxyNone,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Http))]
    ProxyHTTP,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Socks5))]
    ProxySocks5,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.UltraVncRepeater))]
    ProxyUltra
}

public enum Colors
{
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Normal))]
    ColNormal,
    [Description("8-bit")] Col8Bit
}

public enum SmartSizeMode
{
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.NoSmartSize))]
    SmartSNo,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Free))]
    SmartSFree,

    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.Aspect))]
    SmartSAspect
}