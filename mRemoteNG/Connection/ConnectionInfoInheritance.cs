using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using mRemoteNG.Resources.Language;
using mRemoteNG.Tools;
using mRemoteNG.Tree.Root;

namespace mRemoteNG.Connection;

public class ConnectionInfoInheritance
{
    private ConnectionInfoInheritance _tempInheritanceStorage;


    public ConnectionInfoInheritance(ConnectionInfo parent, bool ignoreDefaultInheritance = false)
    {
        Parent = parent;
        if (!ignoreDefaultInheritance)
            SetAllValues(DefaultConnectionInheritance.Instance);
    }


    public ConnectionInfoInheritance Clone(ConnectionInfo parent)
    {
        var newInheritance = (ConnectionInfoInheritance)MemberwiseClone();
        newInheritance.Parent = parent;
        return newInheritance;
    }

    public void EnableInheritance()
    {
        if (_tempInheritanceStorage != null)
            UnstashInheritanceData();
    }

    private void UnstashInheritanceData()
    {
        SetAllValues(_tempInheritanceStorage);
        _tempInheritanceStorage = null;
    }

    public void DisableInheritance()
    {
        StashInheritanceData();
        TurnOffInheritanceCompletely();
    }

    private void StashInheritanceData()
    {
        _tempInheritanceStorage = Clone(Parent);
    }

    public void TurnOnInheritanceCompletely()
    {
        SetAllValues(true);
    }

    public void TurnOffInheritanceCompletely()
    {
        SetAllValues(false);
    }

    private bool EverythingIsInherited()
    {
        var inheritanceProperties = GetProperties();
        var everythingInherited = inheritanceProperties.All(p => (bool)p.GetValue(this, null));
        return everythingInherited;
    }

    public IEnumerable<PropertyInfo> GetProperties()
    {
        var properties = typeof(ConnectionInfoInheritance).GetProperties();
        var filteredProperties = properties.Where(FilterProperty);
        return filteredProperties;
    }

    /// <summary>
    ///     Gets the name of all properties where inheritance is turned on
    ///     (set to True).
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetEnabledInheritanceProperties()
    {
        return InheritanceActive
            ? GetProperties()
                .Where(property => (bool)property.GetValue(this))
                .Select(property => property.Name)
                .ToList()
            : Enumerable.Empty<string>();
    }

    private bool FilterProperty(PropertyInfo propertyInfo)
    {
        var exclusions = new[]
        {
            nameof(EverythingInherited),
            nameof(Parent),
            nameof(InheritanceActive)
        };
        var valueShouldNotBeFiltered = !exclusions.Contains(propertyInfo.Name);
        return valueShouldNotBeFiltered;
    }

    private void SetAllValues(bool value)
    {
        var properties = GetProperties();
        foreach (var property in properties)
            if (property.PropertyType.Name == typeof(bool).Name)
                property.SetValue(this, value, null);
    }

    private void SetAllValues(ConnectionInfoInheritance otherInheritanceObject)
    {
        var properties = GetProperties();
        foreach (var property in properties)
        {
            var newPropertyValue = property.GetValue(otherInheritanceObject, null);
            property.SetValue(this, newPropertyValue, null);
        }
    }

    #region Public Properties

    #region General

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.General))]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.All))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionAll))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool EverythingInherited
    {
        get => EverythingIsInherited();
        set => SetAllValues(value);
    }

    #endregion

    #region Display

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Display), 2)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Description))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionDescription))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool Description { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Display), 2)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Icon))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionIcon))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool Icon { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Display), 2)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Panel))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionPanel))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool Panel { get; set; }

    #endregion

    #region Connection

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 3)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Username))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionUsername))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [Browsable(true)]
    public bool Username { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 3)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.VmId))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionVmId))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [Browsable(true)]
    public bool VmId { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 3)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Password))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionPassword))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [Browsable(true)]
    public bool Password { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 3)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Domain))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionDomain))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [Browsable(true)]
    public bool Domain { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 3)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Port))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionPort))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool Port { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 3)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.SshTunnel))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionSshTunnel))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [Browsable(true)]
    public bool SSHTunnelConnectionName { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 3)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.OpeningCommand))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionOpeningCommand))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    [Browsable(true)]
    public bool OpeningCommand { get; set; }

    #endregion

    #region Protocol

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 4)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Protocol))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionProtocol))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool Protocol { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 4)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.RdpVersion))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionRdpVersion))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RdpVersion { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 4)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.ExternalTool))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionExternalTool))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool ExtApp { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 4)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.PuttySession))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionPuttySession))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool PuttySession { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 4)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.SshOptions))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionSshOptions))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool SSHOptions { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 4)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.AuthenticationLevel))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionAuthenticationLevel))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RDPAuthenticationLevel { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 4)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.MinutesToIdleTimeout))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(
        nameof(Language.PropertyDescriptionRDPMinutesToIdleTimeout))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RDPMinutesToIdleTimeout { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 4)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.MinutesToIdleTimeout))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionRDPAlertIdleTimeout))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RDPAlertIdleTimeout { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 4)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.LoadBalanceInfo))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionLoadBalanceInfo))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool LoadBalanceInfo { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 4)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.RenderingEngine))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionRenderingEngine))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RenderingEngine { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 4)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.UseConsoleSession))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionUseConsoleSession))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool UseConsoleSession { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 4)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.UseCredSsp))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionUseCredSsp))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool UseCredSsp { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 4)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.UseVmId))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionUseVmId))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool UseVmId { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Protocol), 4)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.UseEnhancedMode))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionUseEnhancedMode))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool UseEnhancedMode { get; set; }

    #endregion

    #region RD Gateway

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Gateway), 5)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.RdpGatewayUsageMethod))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(
        nameof(Language.PropertyDescriptionRdpGatewayUsageMethod))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RDGatewayUsageMethod { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Gateway), 5)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.RdpGatewayHostname))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionRDGatewayHostname))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RDGatewayHostname { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Gateway), 5)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.RdpGatewayUseConnectionCredentials))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(
        nameof(Language.PropertyDescriptionRDGatewayUseConnectionCredentials))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RDGatewayUseConnectionCredentials { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Gateway), 5)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.RdpGatewayUsername))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionRDGatewayUsername))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RDGatewayUsername { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Gateway), 5)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.RdpGatewayPassword))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionRdpGatewayPassword))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RDGatewayPassword { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Gateway), 5)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.RdpGatewayDomain))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionRDGatewayDomain))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RDGatewayDomain { get; set; }

    #endregion

    #region Appearance

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 6)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Resolution))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionResolution))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool Resolution { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 6)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.AutomaticResize))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionAutomaticResize))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool AutomaticResize { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 6)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Colors))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionColors))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool Colors { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 6)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.CacheBitmaps))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionCacheBitmaps))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool CacheBitmaps { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 6)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.DisplayWallpaper))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionDisplayWallpaper))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool DisplayWallpaper { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 6)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.DisplayThemes))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionDisplayThemes))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool DisplayThemes { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 6)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.FontSmoothing))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionEnableFontSmoothing))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool EnableFontSmoothing { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 6)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.EnableDesktopComposition))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(
        nameof(Language.PropertyDescriptionEnableDesktopComposition))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool EnableDesktopComposition { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 6)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.DisableFullWindowDrag))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(
        nameof(Language.PropertyDescriptionDisableFullWindowDrag))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool DisableFullWindowDrag { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 6)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.DisableMenuAnimations))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(
        nameof(Language.PropertyDescriptionDisableMenuAnimations))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool DisableMenuAnimations { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 6)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.DisableCursorShadow))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionDisableCursorShadow))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool DisableCursorShadow { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 6)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.DisableCursorBlinking))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(
        nameof(Language.PropertyDescriptionDisableCursorBlinking))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool DisableCursorBlinking { get; set; }

    #endregion

    #region Redirect

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 7)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.RedirectKeys))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionRedirectKeys))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RedirectKeys { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 7)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.DiskDrives))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionRedirectDrives))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RedirectDiskDrives { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 7)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Printers))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionRedirectPrinters))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RedirectPrinters { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 7)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Clipboard))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionRedirectClipboard))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RedirectClipboard { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 7)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Redirect))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionRedirectPorts))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RedirectPorts { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 7)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Redirect))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionRedirectSmartCards))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RedirectSmartCards { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 7)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Sounds))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionRedirectSounds))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RedirectSound { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 7)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.SoundQuality))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionSoundQuality))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool SoundQuality { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Redirect), 7)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.AudioCapture))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionRedirectAudioCapture))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool RedirectAudioCapture { get; set; }

    #endregion

    #region Misc

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Miscellaneous), 8)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.ExternalToolBefore))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionExternalToolBefore))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool PreExtApp { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Miscellaneous), 8)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.ExternalToolAfter))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionExternalToolAfter))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool PostExtApp { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Miscellaneous), 8)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.MacAddress))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionMACAddress))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool MacAddress { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Miscellaneous), 8)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.UserField))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionUser1))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool UserField { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Miscellaneous), 8)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Favorite))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionFavorite))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool Favorite { get; set; }

    #endregion

    #region VNC

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 9)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Compression))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionCompression))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool VNCCompression { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 9)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Encoding))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionEncoding))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool VNCEncoding { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Connection), 9)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.AuthenticationMode))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionAuthenticationMode))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool VNCAuthMode { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Proxy), 9)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.ProxyType))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionVNCProxyType))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool VNCProxyType { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Proxy), 9)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.ProxyAddress))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionVNCProxyAddress))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool VNCProxyIP { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Proxy), 9)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.ProxyPort))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionVNCProxyPort))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool VNCProxyPort { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Proxy), 9)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.ProxyUsername))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionVNCProxyUsername))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool VNCProxyUsername { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Proxy), 9)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.ProxyPassword))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionVNCProxyPassword))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool VNCProxyPassword { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 9)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.Colors))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionColors))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool VNCColors { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 9)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.SmartSizeMode))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionSmartSizeMode))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool VNCSmartSizeMode { get; set; }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Appearance), 9)]
    [LocalizedAttributes.LocalizedDisplayNameInheritAttribute(nameof(Language.ViewOnly))]
    [LocalizedAttributes.LocalizedDescriptionInheritAttribute(nameof(Language.PropertyDescriptionViewOnly))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public bool VNCViewOnly { get; set; }

    #endregion

    [Browsable(false)] public ConnectionInfo Parent { get; private set; }

    /// <summary>
    ///     Indicates whether this inheritance object is enabled.
    ///     When false, users of this object should not respect inheritance
    ///     settings for individual properties.
    /// </summary>
    [Browsable(false)]
    public bool InheritanceActive => !(Parent is RootNodeInfo || Parent?.Parent is RootNodeInfo);

    #endregion
}