using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using mRemoteNG.App;
using mRemoteNG.Messages;
using mRemoteNG.Resources.Language;
using WeifenLuo.WinFormsUI.Docking;

namespace mRemoteNG.UI.Window;

public class UltraVNCWindow : BaseWindow
{
    #region Form Init

    internal ToolStrip tsMain;
    internal Panel pnlContainer;
    internal ToolStripButton btnDisconnect;

    private void InitializeComponent()
    {
        var resources =
            new ComponentResourceManager(typeof(UltraVNCWindow));
        tsMain = new ToolStrip();
        btnDisconnect = new ToolStripButton();
        pnlContainer = new Panel();
        tsMain.SuspendLayout();
        SuspendLayout();
        // 
        // tsMain
        // 
        tsMain.GripStyle = ToolStripGripStyle.Hidden;
        tsMain.Items.AddRange(new ToolStripItem[]
        {
            btnDisconnect
        });
        tsMain.Location = new Point(0, 0);
        tsMain.Name = "tsMain";
        tsMain.Size = new Size(446, 25);
        tsMain.TabIndex = 0;
        tsMain.Text = "ToolStrip1";
        // 
        // btnDisconnect
        // 
        btnDisconnect.DisplayStyle = ToolStripItemDisplayStyle.Text;
        btnDisconnect.Image = (Image)resources.GetObject("btnDisconnect.Image");
        btnDisconnect.ImageTransparentColor = Color.Magenta;
        btnDisconnect.Name = "btnDisconnect";
        btnDisconnect.Size = new Size(70, 22);
        btnDisconnect.Text = "Disconnect";
        btnDisconnect.Click += btnDisconnect_Click;
        // 
        // pnlContainer
        // 
        pnlContainer.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Bottom
            | AnchorStyles.Left
            | AnchorStyles.Right;
        pnlContainer.Location = new Point(0, 27);
        pnlContainer.Name = "pnlContainer";
        pnlContainer.Size = new Size(446, 335);
        pnlContainer.TabIndex = 1;
        // 
        // UltraVNCWindow
        // 
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(446, 362);
        Controls.Add(pnlContainer);
        Controls.Add(tsMain);
        Font = new Font("Segoe UI", 8.25F, FontStyle.Regular,
            GraphicsUnit.Point, 0);
        Name = "UltraVNCWindow";
        TabText = "UltraVNC SC";
        Text = "UltraVNC SC";
        Load += UltraVNCSC_Load;
        tsMain.ResumeLayout(false);
        tsMain.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    #region Declarations

    //Private WithEvents vnc As AxCSC_ViewerXControl

    #endregion

    #region Public Methods

    public UltraVNCWindow()
    {
        WindowType = WindowType.UltraVNCSC;
        DockPnl = new DockContent();
        InitializeComponent();
    }

    #endregion

    #region Private Methods

    private void UltraVNCSC_Load(object sender, EventArgs e)
    {
        ApplyLanguage();

        StartListening();
    }

    private void ApplyLanguage()
    {
        btnDisconnect.Text = Language.Disconnect;
    }

    private void StartListening()
    {
        try
        {
            //If vnc IsNot Nothing Then
            //    vnc.Dispose()
            //    vnc = Nothing
            //End If

            //vnc = New AxCSC_ViewerXControl()
            //SetupLicense()

            //vnc.Parent = pnlContainer
            //vnc.Dock = DockStyle.Fill
            //vnc.Show()

            //vnc.StretchMode = ViewerX.ScreenStretchMode.SSM_ASPECT
            //vnc.ListeningText = Language.InheritListeningForIncomingVNCConnections & " " & Settings.UVNCSCPort

            //vnc.ListenEx(Settings.UVNCSCPort)
        }
        catch (Exception ex)
        {
            Runtime.MessageCollector.AddMessage(MessageClass.ErrorMsg,
                "StartListening (UI.Window.UltraVNCSC) failed" +
                Environment.NewLine + ex.Message);
            Close();
        }
    }

#if false
        private void SetupLicense()
		{
			try
			{
				//Dim f As System.Reflection.FieldInfo
				//f = GetType(AxHost).GetField("licenseKey", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
				//f.SetValue(vnc, "{072169039103041044176252035252117103057101225235137221179204110241121074}")
			}
			catch (Exception ex)
			{
				Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg, "VNC SetupLicense failed (UI.Window.UltraVNCSC)" + Environment.NewLine + ex.Message, true);
			}
		}
#endif

    //Private Sub vnc_ConnectionAccepted(ByVal sender As Object, ByVal e As AxViewerX._ISmartCodeVNCViewerEvents_ConnectionAcceptedEvent) Handles vnc.ConnectionAccepted
    //    mC.AddMessage(Messages.MessageClass.InformationMsg, e.bstrServerAddress & " is now connected to your UltraVNC SingleClick panel!")
    //End Sub

    //Private Sub vnc_Disconnected(ByVal sender As Object, ByVal e As System.EventArgs) Handles vnc.Disconnected
    //    StartListening()
    //End Sub

    private void btnDisconnect_Click(object sender, EventArgs e)
    {
        //vnc.Dispose()
        Dispose();
        Windows.Show(WindowType.UltraVNCSC);
    }

    #endregion
}