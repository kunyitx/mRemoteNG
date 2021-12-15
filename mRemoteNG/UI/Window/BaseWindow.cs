using System.Drawing;
using mRemoteNG.Themes;
using WeifenLuo.WinFormsUI.Docking;

// ReSharper disable UnusedAutoPropertyAccessor.Global


namespace mRemoteNG.UI.Window;

public class BaseWindow : DockContent
{
    #region Private Variables

    //private WindowType _WindowType;
    //private DockContent _DockPnl;
    private ThemeManager _themeManager;

    #endregion

    #region Public Methods

    public void SetFormText(string t)
    {
        Text = t;
        TabText = t;
    }

    #endregion

    internal void ApplyTheme()
    {
        _themeManager = ThemeManager.getInstance();
        if (!_themeManager.ActiveAndExtended) return;
        BackColor = _themeManager.ActiveTheme.ExtendedPalette.getColor("Dialog_Background");
        ForeColor = _themeManager.ActiveTheme.ExtendedPalette.getColor("Dialog_Foreground");
    }

    private void InitializeComponent()
    {
        SuspendLayout();
        // 
        // BaseWindow
        // 
        ClientSize = new Size(284, 261);
        Font = new Font("Segoe UI", 8.25F, FontStyle.Regular,
            GraphicsUnit.Point, 0);
        Name = "BaseWindow";
        ResumeLayout(false);
    }

    #region Public Properties

    protected WindowType WindowType { get; set; }

    protected DockContent DockPnl { get; set; }

    #endregion


    #region Private Methods

    /*
            private void Base_Load(object sender, EventArgs e)
            {
                FrmMain.Default.ShowHidePanelTabs();
            }
    */

    /*
            private void Base_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
            {
                FrmMain.Default.ShowHidePanelTabs(this);
            }
    */

    #endregion
}