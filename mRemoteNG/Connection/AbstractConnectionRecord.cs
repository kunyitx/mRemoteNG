using System.Collections.Generic;
using System.ComponentModel;
using mRemoteNG.Config.Putty;
using mRemoteNG.Connection.Protocol;
using mRemoteNG.Connection.Protocol.Http;
using mRemoteNG.Connection.Protocol.RDP;
using mRemoteNG.Connection.Protocol.VNC;
using mRemoteNG.Properties;
using mRemoteNG.Resources.Language;
using mRemoteNG.Tools;
using mRemoteNG.Tools.Attributes;

namespace mRemoteNG.Connection;

public abstract class AbstractConnectionRecord : INotifyPropertyChanged
{
    protected AbstractConnectionRecord(string uniqueId)
    {
        ConstantID = uniqueId.ThrowIfNullOrEmpty(nameof(uniqueId));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual TPropertyType GetPropertyValue<TPropertyType>(string propertyName, TPropertyType value)
    {
        return (TPropertyType)GetType().GetProperty(propertyName)?.GetValue(this, null);
    }

    protected virtual void RaisePropertyChangedEvent(object sender, PropertyChangedEventArgs args)
    {
        PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(args.PropertyName));
    }

    protected void SetField<T>(ref T field, T value, string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        RaisePropertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
    }

    #region Fields

    private string _name;
    private string _description;
    private string _icon;
    private string _panel;

    private string _hostname;
    private string _username = "";
    private string _password = "";
    private string _domain = "";
    private string _vmId = "";
    private bool _useEnhancedMode;

    private string _sshTunnelConnectionName = "";
    private ProtocolType _protocol;
    private RdpVersion _rdpProtocolVersion;
    private string _extApp;
    private int _port;
    private string _sshOptions = "";
    private string _puttySession;
    private bool _useConsoleSession;
    private AuthenticationLevel _rdpAuthenticationLevel;
    private int _rdpMinutesToIdleTimeout;
    private bool _rdpAlertIdleTimeout;
    private string _loadBalanceInfo;
    private HTTPBase.RenderingEngine _renderingEngine;
    private bool _useCredSsp;
    private bool _useVmId;

    private RDGatewayUsageMethod _rdGatewayUsageMethod;
    private string _rdGatewayHostname;
    private RDGatewayUseConnectionCredentials _rdGatewayUseConnectionCredentials;
    private string _rdGatewayUsername;
    private string _rdGatewayPassword;
    private string _rdGatewayDomain;

    private RDPResolutions _resolution;
    private bool _automaticResize;
    private RDPColors _colors;
    private bool _cacheBitmaps;
    private bool _displayWallpaper;
    private bool _displayThemes;
    private bool _enableFontSmoothing;
    private bool _enableDesktopComposition;
    private bool _disableFullWindowDrag;
    private bool _disableMenuAnimations;
    private bool _disableCursorShadow;
    private bool _disableCursorBlinking;

    private bool _redirectKeys;
    private bool _redirectDiskDrives;
    private bool _redirectPrinters;
    private bool _redirectClipboard;
    private bool _redirectPorts;
    private bool _redirectSmartCards;
    private RDPSounds _redirectSound;
    private RDPSoundQuality _soundQuality;
    private bool _redirectAudioCapture;

    private string _preExtApp;
    private string _postExtApp;
    private string _macAddress;
    private string _openingCommand;
    private string _userField;
    private string _startProgram;
    private string _startProgramWorkDir;
    private bool _favorite;

    private ProtocolVNC.Compression _vncCompression;
    private ProtocolVNC.Encoding _vncEncoding;
    private ProtocolVNC.AuthMode _vncAuthMode;
    private ProtocolVNC.ProxyType _vncProxyType;
    private string _vncProxyIp;
    private int _vncProxyPort;
    private string _vncProxyUsername;
    private string _vncProxyPassword;
    private ProtocolVNC.Colors _vncColors;
    private ProtocolVNC.SmartSizeMode _vncSmartSizeMode;
    private bool _vncViewOnly;

    #endregion

    #region Properties

    #region Display

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Display))]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Name))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionName))]
    public virtual string Name
    {
        get => _name;
        set => SetField(ref _name, value, "Name");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Display))]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Description))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionDescription))]
    public virtual string Description
    {
        get => GetPropertyValue("Description", _description);
        set => SetField(ref _description, value, "Description");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Display))]
    [TypeConverter(typeof(ConnectionIcon))]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Icon))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionIcon))]
    public virtual string Icon
    {
        get => GetPropertyValue("Icon", _icon);
        set => SetField(ref _icon, value, "Icon");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Display))]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Panel))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionPanel))]
    public virtual string Panel
    {
        get => GetPropertyValue("Panel", _panel);
        set => SetField(ref _panel, value, "Panel");
    }

    #endregion

    #region Connection

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 2)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.HostnameIp))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionHostnameIp))]
    [AttributeUsedInAllProtocolsExcept]
    public virtual string Hostname
    {
        get => _hostname.Trim();
        set => SetField(ref _hostname, value?.Trim(), "Hostname");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 2)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Port))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionPort))]
    [AttributeUsedInAllProtocolsExcept]
    public virtual int Port
    {
        get => GetPropertyValue("Port", _port);
        set => SetField(ref _port, value, "Port");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 2)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Username))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionUsername))]
    [AttributeUsedInAllProtocolsExcept(ProtocolType.VNC, ProtocolType.Telnet, ProtocolType.Rlogin, ProtocolType.RAW)]
    public virtual string Username
    {
        get => GetPropertyValue("Username", _username);
        set => SetField(ref _username, Settings.Default.DoNotTrimUsername ? value : value?.Trim(), "Username");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 2)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Password))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionPassword))]
    [PasswordPropertyText(true)]
    [AttributeUsedInAllProtocolsExcept(ProtocolType.Telnet, ProtocolType.Rlogin, ProtocolType.RAW)]
    public virtual string Password
    {
        get => GetPropertyValue("Password", _password);
        set => SetField(ref _password, value, "Password");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 2)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Domain))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionDomain))]
    [AttributeUsedInProtocol(ProtocolType.RDP, ProtocolType.IntApp, ProtocolType.PowerShell)]
    public string Domain
    {
        get => GetPropertyValue("Domain", _domain).Trim();
        set => SetField(ref _domain, value?.Trim(), "Domain");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 2)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.VmId))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionVmId))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public string VmId
    {
        get => GetPropertyValue("VmId", _vmId).Trim();
        set => SetField(ref _vmId, value?.Trim(), "VmId");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 2)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.SshTunnel))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionSshTunnel))]
    [TypeConverter(typeof(SshTunnelTypeConverter))]
    [AttributeUsedInAllProtocolsExcept]
    public string SSHTunnelConnectionName
    {
        get => GetPropertyValue("SSHTunnelConnectionName", _sshTunnelConnectionName).Trim();
        set => SetField(ref _sshTunnelConnectionName, value?.Trim(), "SSHTunnelConnectionName");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Miscellaneous), 7)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.OpeningCommand))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionOpeningCommand))]
    [AttributeUsedInProtocol(ProtocolType.SSH1, ProtocolType.SSH2)]
    public virtual string OpeningCommand
    {
        get => GetPropertyValue("OpeningCommand", _openingCommand);
        set => SetField(ref _openingCommand, value, "OpeningCommand");
    }

    #endregion

    #region Protocol

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 3)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Protocol))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionProtocol))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    public virtual ProtocolType Protocol
    {
        get => GetPropertyValue("Protocol", _protocol);
        set => SetField(ref _protocol, value, "Protocol");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 3)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.RdpVersion))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRdpVersion))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public virtual RdpVersion RdpVersion
    {
        get => GetPropertyValue("RdpVersion", _rdpProtocolVersion);
        set => SetField(ref _rdpProtocolVersion, value, nameof(RdpVersion));
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 3)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.ExternalTool))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionExternalTool))]
    [TypeConverter(typeof(ExternalToolsTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.IntApp)]
    public string ExtApp
    {
        get => GetPropertyValue("ExtApp", _extApp);
        set => SetField(ref _extApp, value, "ExtApp");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 3)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.PuttySession))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionPuttySession))]
    [TypeConverter(typeof(PuttySessionsManager.SessionList))]
    [AttributeUsedInProtocol(ProtocolType.SSH1, ProtocolType.SSH2, ProtocolType.Telnet,
        ProtocolType.RAW, ProtocolType.Rlogin)]
    public virtual string PuttySession
    {
        get => GetPropertyValue("PuttySession", _puttySession);
        set => SetField(ref _puttySession, value, "PuttySession");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 3)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.SshOptions))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionSshOptions))]
    [AttributeUsedInProtocol(ProtocolType.SSH1, ProtocolType.SSH2)]
    public virtual string SSHOptions
    {
        get => GetPropertyValue("SSHOptions", _sshOptions);
        set => SetField(ref _sshOptions, value, "SSHOptions");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 3)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.UseConsoleSession))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionUseConsoleSession))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool UseConsoleSession
    {
        get => GetPropertyValue("UseConsoleSession", _useConsoleSession);
        set => SetField(ref _useConsoleSession, value, "UseConsoleSession");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 3)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.AuthenticationLevel))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionAuthenticationLevel))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public AuthenticationLevel RDPAuthenticationLevel
    {
        get => GetPropertyValue("RDPAuthenticationLevel", _rdpAuthenticationLevel);
        set => SetField(ref _rdpAuthenticationLevel, value, "RDPAuthenticationLevel");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 3)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.MinutesToIdleTimeout))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRDPMinutesToIdleTimeout))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public virtual int RDPMinutesToIdleTimeout
    {
        get => GetPropertyValue("RDPMinutesToIdleTimeout", _rdpMinutesToIdleTimeout);
        set
        {
            if (value < 0)
                value = 0;
            else if (value > 240)
                value = 240;
            SetField(ref _rdpMinutesToIdleTimeout, value, "RDPMinutesToIdleTimeout");
        }
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 3)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.MinutesToIdleTimeout))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRDPAlertIdleTimeout))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool RDPAlertIdleTimeout
    {
        get => GetPropertyValue("RDPAlertIdleTimeout", _rdpAlertIdleTimeout);
        set => SetField(ref _rdpAlertIdleTimeout, value, "RDPAlertIdleTimeout");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 3)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.LoadBalanceInfo))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionLoadBalanceInfo))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public string LoadBalanceInfo
    {
        get => GetPropertyValue("LoadBalanceInfo", _loadBalanceInfo).Trim();
        set => SetField(ref _loadBalanceInfo, value?.Trim(), "LoadBalanceInfo");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 3)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.RenderingEngine))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRenderingEngine))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.HTTP, ProtocolType.HTTPS)]
    public HTTPBase.RenderingEngine RenderingEngine
    {
        get => GetPropertyValue("RenderingEngine", _renderingEngine);
        set => SetField(ref _renderingEngine, value, "RenderingEngine");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 3)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.UseCredSsp))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionUseCredSsp))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool UseCredSsp
    {
        get => GetPropertyValue("UseCredSsp", _useCredSsp);
        set => SetField(ref _useCredSsp, value, "UseCredSsp");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 3)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.UseVmId))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionUseVmId))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool UseVmId
    {
        get => GetPropertyValue("UseVmId", _useVmId);
        set => SetField(ref _useVmId, value, "UseVmId");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 3)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.UseEnhancedMode))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionUseEnhancedMode))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool UseEnhancedMode
    {
        get => GetPropertyValue("UseEnhancedMode", _useEnhancedMode);
        set => SetField(ref _useEnhancedMode, value, "UseEnhancedMode");
    }

    #endregion

    #region RD Gateway

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Gateway), 4)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.RdpGatewayUsageMethod))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRdpGatewayUsageMethod))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public RDGatewayUsageMethod RDGatewayUsageMethod
    {
        get => GetPropertyValue("RDGatewayUsageMethod", _rdGatewayUsageMethod);
        set => SetField(ref _rdGatewayUsageMethod, value, "RDGatewayUsageMethod");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Gateway), 4)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.RdpGatewayHostname))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRDGatewayHostname))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public string RDGatewayHostname
    {
        get => GetPropertyValue("RDGatewayHostname", _rdGatewayHostname).Trim();
        set => SetField(ref _rdGatewayHostname, value?.Trim(), "RDGatewayHostname");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Gateway), 4)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.RdpGatewayUseConnectionCredentials))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(
        nameof(Language.PropertyDescriptionRDGatewayUseConnectionCredentials))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public RDGatewayUseConnectionCredentials RDGatewayUseConnectionCredentials
    {
        get => GetPropertyValue("RDGatewayUseConnectionCredentials", _rdGatewayUseConnectionCredentials);
        set => SetField(ref _rdGatewayUseConnectionCredentials, value, "RDGatewayUseConnectionCredentials");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Gateway), 4)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.RdpGatewayUsername))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRDGatewayUsername))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public string RDGatewayUsername
    {
        get => GetPropertyValue("RDGatewayUsername", _rdGatewayUsername).Trim();
        set => SetField(ref _rdGatewayUsername, value?.Trim(), "RDGatewayUsername");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Gateway), 4)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.RdpGatewayPassword))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRdpGatewayPassword))]
    [PasswordPropertyText(true)]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public string RDGatewayPassword
    {
        get => GetPropertyValue("RDGatewayPassword", _rdGatewayPassword);
        set => SetField(ref _rdGatewayPassword, value, "RDGatewayPassword");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Gateway), 4)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.RdpGatewayDomain))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRDGatewayDomain))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public string RDGatewayDomain
    {
        get => GetPropertyValue("RDGatewayDomain", _rdGatewayDomain).Trim();
        set => SetField(ref _rdGatewayDomain, value?.Trim(), "RDGatewayDomain");
    }

    #endregion

    #region Appearance

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Resolution))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionResolution))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public RDPResolutions Resolution
    {
        get => GetPropertyValue("Resolution", _resolution);
        set => SetField(ref _resolution, value, "Resolution");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.AutomaticResize))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionAutomaticResize))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool AutomaticResize
    {
        get => GetPropertyValue("AutomaticResize", _automaticResize);
        set => SetField(ref _automaticResize, value, "AutomaticResize");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Colors))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionColors))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public RDPColors Colors
    {
        get => GetPropertyValue("Colors", _colors);
        set => SetField(ref _colors, value, "Colors");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.CacheBitmaps))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionCacheBitmaps))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool CacheBitmaps
    {
        get => GetPropertyValue("CacheBitmaps", _cacheBitmaps);
        set => SetField(ref _cacheBitmaps, value, "CacheBitmaps");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.DisplayWallpaper))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionDisplayWallpaper))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool DisplayWallpaper
    {
        get => GetPropertyValue("DisplayWallpaper", _displayWallpaper);
        set => SetField(ref _displayWallpaper, value, "DisplayWallpaper");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.DisplayThemes))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionDisplayThemes))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool DisplayThemes
    {
        get => GetPropertyValue("DisplayThemes", _displayThemes);
        set => SetField(ref _displayThemes, value, "DisplayThemes");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.FontSmoothing))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionEnableFontSmoothing))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool EnableFontSmoothing
    {
        get => GetPropertyValue("EnableFontSmoothing", _enableFontSmoothing);
        set => SetField(ref _enableFontSmoothing, value, "EnableFontSmoothing");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.EnableDesktopComposition))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionEnableDesktopComposition))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool EnableDesktopComposition
    {
        get => GetPropertyValue("EnableDesktopComposition", _enableDesktopComposition);
        set => SetField(ref _enableDesktopComposition, value, "EnableDesktopComposition");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.DisableFullWindowDrag))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionDisableFullWindowDrag))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool DisableFullWindowDrag
    {
        get => GetPropertyValue("DisableFullWindowDrag", _disableFullWindowDrag);
        set => SetField(ref _disableFullWindowDrag, value, "DisableFullWindowDrag");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.DisableMenuAnimations))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionDisableMenuAnimations))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool DisableMenuAnimations
    {
        get => GetPropertyValue("DisableMenuAnimations", _disableMenuAnimations);
        set => SetField(ref _disableMenuAnimations, value, "DisableMenuAnimations");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.DisableCursorShadow))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionDisableCursorShadow))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool DisableCursorShadow
    {
        get => GetPropertyValue("DisableCursorShadow", _disableCursorShadow);
        set => SetField(ref _disableCursorShadow, value, "DisableCursorShadow");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.DisableCursorShadow))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionDisableCursorShadow))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool DisableCursorBlinking
    {
        get => GetPropertyValue("DisableCursorBlinking", _disableCursorBlinking);
        set => SetField(ref _disableCursorBlinking, value, "DisableCursorBlinking");
    }

    #endregion

    #region Redirect

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 6)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.RedirectKeys))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRedirectKeys))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool RedirectKeys
    {
        get => GetPropertyValue("RedirectKeys", _redirectKeys);
        set => SetField(ref _redirectKeys, value, "RedirectKeys");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 6)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.DiskDrives))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRedirectDrives))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool RedirectDiskDrives
    {
        get => GetPropertyValue("RedirectDiskDrives", _redirectDiskDrives);
        set => SetField(ref _redirectDiskDrives, value, "RedirectDiskDrives");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 6)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Printers))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRedirectPrinters))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool RedirectPrinters
    {
        get => GetPropertyValue("RedirectPrinters", _redirectPrinters);
        set => SetField(ref _redirectPrinters, value, "RedirectPrinters");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 6)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Clipboard))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRedirectClipboard))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool RedirectClipboard
    {
        get => GetPropertyValue("RedirectClipboard", _redirectClipboard);
        set => SetField(ref _redirectClipboard, value, "RedirectClipboard");
    }


    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 6)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Ports))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRedirectPorts))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool RedirectPorts
    {
        get => GetPropertyValue("RedirectPorts", _redirectPorts);
        set => SetField(ref _redirectPorts, value, "RedirectPorts");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 6)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.SmartCard))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRedirectSmartCards))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool RedirectSmartCards
    {
        get => GetPropertyValue("RedirectSmartCards", _redirectSmartCards);
        set => SetField(ref _redirectSmartCards, value, "RedirectSmartCards");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 6)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Sounds))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRedirectSounds))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public RDPSounds RedirectSound
    {
        get => GetPropertyValue("RedirectSound", _redirectSound);
        set => SetField(ref _redirectSound, value, "RedirectSound");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 6)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.SoundQuality))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionSoundQuality))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public RDPSoundQuality SoundQuality
    {
        get => GetPropertyValue("SoundQuality", _soundQuality);
        set => SetField(ref _soundQuality, value, "SoundQuality");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 6)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.AudioCapture))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRedirectAudioCapture))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public bool RedirectAudioCapture
    {
        get => GetPropertyValue("RedirectAudioCapture", _redirectAudioCapture);
        set => SetField(ref _redirectAudioCapture, value, nameof(RedirectAudioCapture));
    }

    #endregion

    #region Misc

    [Browsable(false)] public string ConstantID { get; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Miscellaneous), 7)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.ExternalToolBefore))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionExternalToolBefore))]
    [TypeConverter(typeof(ExternalToolsTypeConverter))]
    public virtual string PreExtApp
    {
        get => GetPropertyValue("PreExtApp", _preExtApp);
        set => SetField(ref _preExtApp, value, "PreExtApp");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Miscellaneous), 7)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.ExternalToolAfter))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionExternalToolAfter))]
    [TypeConverter(typeof(ExternalToolsTypeConverter))]
    public virtual string PostExtApp
    {
        get => GetPropertyValue("PostExtApp", _postExtApp);
        set => SetField(ref _postExtApp, value, "PostExtApp");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Miscellaneous), 7)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.MacAddress))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionMACAddress))]
    public virtual string MacAddress
    {
        get => GetPropertyValue("MacAddress", _macAddress);
        set => SetField(ref _macAddress, value, "MacAddress");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Miscellaneous), 7)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.UserField))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionUser1))]
    public virtual string UserField
    {
        get => GetPropertyValue("UserField", _userField);
        set => SetField(ref _userField, value, "UserField");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Miscellaneous), 7)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Favorite))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionFavorite))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public virtual bool Favorite
    {
        get => GetPropertyValue("Favorite", _favorite);
        set => SetField(ref _favorite, value, "Favorite");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Miscellaneous), 7)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.StartProgram))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionStartProgram))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public virtual string StartProgram
    {
        get => GetPropertyValue("StartProgram", _startProgram);
        set => SetField(ref _startProgram, value, "StartProgram");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Miscellaneous), 7)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.RDPStartProgramWorkDir))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionRDPStartProgramWorkDir))]
    [AttributeUsedInProtocol(ProtocolType.RDP)]
    public virtual string StartProgramWorkDir
    {
        get => GetPropertyValue("StartProgramWorkDir", _startProgramWorkDir);
        set => SetField(ref _startProgramWorkDir, value, "StartProgramWorkDir");
    }

    #endregion

    #region VNC

    // TODO: it seems all these VNC properties were added and serialized but
    // never hooked up to the VNC protocol or shown to the user
    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Compression))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionCompression))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.VNC)]
    [Browsable(false)]
    public ProtocolVNC.Compression VNCCompression
    {
        get => GetPropertyValue("VNCCompression", _vncCompression);
        set => SetField(ref _vncCompression, value, "VNCCompression");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Encoding))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionEncoding))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.VNC)]
    [Browsable(false)]
    public ProtocolVNC.Encoding VNCEncoding
    {
        get => GetPropertyValue("VNCEncoding", _vncEncoding);
        set => SetField(ref _vncEncoding, value, "VNCEncoding");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 2)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.AuthenticationMode))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionAuthenticationMode))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.VNC)]
    [Browsable(false)]
    public ProtocolVNC.AuthMode VNCAuthMode
    {
        get => GetPropertyValue("VNCAuthMode", _vncAuthMode);
        set => SetField(ref _vncAuthMode, value, "VNCAuthMode");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Proxy), 7)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.ProxyType))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionVNCProxyType))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.VNC)]
    [Browsable(false)]
    public ProtocolVNC.ProxyType VNCProxyType
    {
        get => GetPropertyValue("VNCProxyType", _vncProxyType);
        set => SetField(ref _vncProxyType, value, "VNCProxyType");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Proxy), 7)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.ProxyAddress))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionVNCProxyAddress))]
    [AttributeUsedInProtocol(ProtocolType.VNC)]
    [Browsable(false)]
    public string VNCProxyIP
    {
        get => GetPropertyValue("VNCProxyIP", _vncProxyIp);
        set => SetField(ref _vncProxyIp, value, "VNCProxyIP");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Proxy), 7)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.ProxyPort))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionVNCProxyPort))]
    [AttributeUsedInProtocol(ProtocolType.VNC)]
    [Browsable(false)]
    public int VNCProxyPort
    {
        get => GetPropertyValue("VNCProxyPort", _vncProxyPort);
        set => SetField(ref _vncProxyPort, value, "VNCProxyPort");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Proxy), 7)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.ProxyUsername))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionVNCProxyUsername))]
    [AttributeUsedInProtocol(ProtocolType.VNC)]
    [Browsable(false)]
    public string VNCProxyUsername
    {
        get => GetPropertyValue("VNCProxyUsername", _vncProxyUsername);
        set => SetField(ref _vncProxyUsername, value, "VNCProxyUsername");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Proxy), 7)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.ProxyPassword))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionVNCProxyPassword))]
    [PasswordPropertyText(true)]
    [AttributeUsedInProtocol(ProtocolType.VNC)]
    [Browsable(false)]
    public string VNCProxyPassword
    {
        get => GetPropertyValue("VNCProxyPassword", _vncProxyPassword);
        set => SetField(ref _vncProxyPassword, value, "VNCProxyPassword");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Colors))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionColors))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.VNC)]
    [Browsable(false)]
    public ProtocolVNC.Colors VNCColors
    {
        get => GetPropertyValue("VNCColors", _vncColors);
        set => SetField(ref _vncColors, value, "VNCColors");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.SmartSizeMode))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionSmartSizeMode))]
    [TypeConverter(typeof(MiscTools.EnumTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.VNC)]
    public ProtocolVNC.SmartSizeMode VNCSmartSizeMode
    {
        get => GetPropertyValue("VNCSmartSizeMode", _vncSmartSizeMode);
        set => SetField(ref _vncSmartSizeMode, value, "VNCSmartSizeMode");
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 5)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.ViewOnly))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionViewOnly))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [AttributeUsedInProtocol(ProtocolType.VNC)]
    public bool VNCViewOnly
    {
        get => GetPropertyValue("VNCViewOnly", _vncViewOnly);
        set => SetField(ref _vncViewOnly, value, "VNCViewOnly");
    }

    #endregion

    #endregion
}