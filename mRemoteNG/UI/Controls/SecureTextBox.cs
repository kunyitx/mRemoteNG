using System;
using System.Security;
using mRemoteNG.Security;

namespace mRemoteNG.UI.Controls;

public partial class SecureTextBox : MrngTextBox
{
    public SecureTextBox()
    {
        InitializeComponent();
        TextChanged += SecureTextBox_TextChanged;
    }

    public SecureString SecString { get; private set; } = new();

    private void SecureTextBox_TextChanged(object sender, EventArgs e)
    {
        SecString = Text.ConvertToSecureString();
    }
}