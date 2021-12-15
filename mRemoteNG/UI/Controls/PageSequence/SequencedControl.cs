using System;
using System.Drawing;
using System.Windows.Forms;
using mRemoteNG.Themes;

namespace mRemoteNG.UI.Controls.PageSequence;

public class SequencedControl : UserControl, ISequenceChangingNotifier
{
    public SequencedControl()
    {
        ThemeManager.getInstance().ThemeChanged += ApplyTheme;
        InitializeComponent();
    }

    public event EventHandler Next;
    public event EventHandler Previous;
    public event SequencedPageReplcementRequestHandler PageReplacementRequested;

    protected virtual void RaiseNextPageEvent()
    {
        Next?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void ApplyTheme()
    {
        if (!ThemeManager.getInstance().ActiveAndExtended) return;
        BackColor = ThemeManager.getInstance().ActiveTheme.ExtendedPalette.getColor("Dialog_Background");
        ForeColor = ThemeManager.getInstance().ActiveTheme.ExtendedPalette.getColor("Dialog_Foreground");
    }

    protected virtual void RaisePreviousPageEvent()
    {
        Previous?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void RaisePageReplacementEvent(SequencedControl control, RelativePagePosition pagetoReplace)
    {
        PageReplacementRequested?.Invoke(this, new SequencedPageReplcementRequestArgs(control, pagetoReplace));
    }

    private void InitializeComponent()
    {
        SuspendLayout();
        // 
        // SequencedControl
        // 
        Font = new Font("Segoe UI", 8.25F, FontStyle.Regular,
            GraphicsUnit.Point, 0);
        Name = "SequencedControl";
        ResumeLayout(false);
    }
}