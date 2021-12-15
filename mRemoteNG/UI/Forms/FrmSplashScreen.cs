using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace mRemoteNG.UI.Forms;

public partial class FrmSplashScreen : Form
{
    private static FrmSplashScreen instance;

    private FrmSplashScreen()
    {
        InitializeComponent();

        Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
    }

    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            // turn on WS_EX_TOOLWINDOW style bit
            cp.ExStyle |= 0x80;
            return cp;
        }
    }

    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect, // x-coordinate of upper-left corner
        int nTopRect, // y-coordinate of upper-left corner
        int nRightRect, // x-coordinate of lower-right corner
        int nBottomRect, // y-coordinate of lower-right corner
        int nWidthEllipse, // width of ellipse
        int nHeightEllipse // height of ellipse
    );

    public static FrmSplashScreen getInstance()
    {
        if (instance == null)
            instance = new FrmSplashScreen();
        return instance;
    }
}