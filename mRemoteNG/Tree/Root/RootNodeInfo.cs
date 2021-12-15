using System;
using System.ComponentModel;
using mRemoteNG.Container;
using mRemoteNG.Resources.Language;
using mRemoteNG.Tools;

namespace mRemoteNG.Tree.Root;

[DefaultProperty("Name")]
public class RootNodeInfo : ContainerInfo
{
    private string _customPassword = "";
    private string _name;

    public RootNodeInfo(RootNodeType rootType, string uniqueId)
        : base(uniqueId)
    {
        _name = Language.Connections;
        Type = rootType;
    }

    public RootNodeInfo(RootNodeType rootType)
        : this(rootType, Guid.NewGuid().ToString())
    {
    }

    #region Public Properties

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Miscellaneous))]
    [Browsable(true)]
    [LocalizedAttributes.LocalizedDefaultValueAttribute(nameof(Language.Connections))]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.Name))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionName))]
    public override string Name
    {
        get => _name;
        set => _name = value;
    }

    [LocalizedAttributes.LocalizedCategoryAttribute(nameof(Language.Miscellaneous))]
    [Browsable(true)]
    [LocalizedAttributes.LocalizedDisplayNameAttribute(nameof(Language.PasswordProtect))]
    [LocalizedAttributes.LocalizedDescriptionAttribute(nameof(Language.PropertyDescriptionPasswordProtect))]
    [TypeConverter(typeof(MiscTools.YesNoTypeConverter))]
    public new bool Password { get; set; }

    [Browsable(false)]
    public string PasswordString
    {
        get => Password ? _customPassword : DefaultPassword;
        set
        {
            _customPassword = value;
            Password = !string.IsNullOrEmpty(value) && _customPassword != DefaultPassword;
        }
    }

    [Browsable(false)] public string DefaultPassword { get; } = "mR3m";

    [Browsable(false)] public RootNodeType Type { get; set; }

    public override TreeNodeType GetTreeNodeType()
    {
        return Type == RootNodeType.Connection
            ? TreeNodeType.Root
            : TreeNodeType.PuttyRoot;
    }

    #endregion
}