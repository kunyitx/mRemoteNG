using System;
using System.Drawing;
using System.Windows.Forms;
using mRemoteNG.App;

namespace mRemoteNG.UI.Controls;

public class HeadlessTabControl : TabControl
{
    protected override void WndProc(ref Message m)
    {
        // Hide tabs by trapping the TCM_ADJUSTRECT message
        if (m.Msg == NativeMethods.TCM_ADJUSTRECT && !DesignMode)
            m.Result = (IntPtr)1;
        else
            base.WndProc(ref m);
    }

    private void InitializeComponent()
    {
        SuspendLayout();
        // 
        // HeadlessTabControl
        // 
        Font = new Font("Segoe UI", 8.25F, FontStyle.Regular,
            GraphicsUnit.Point, 0);
        ResumeLayout(false);
    }
}