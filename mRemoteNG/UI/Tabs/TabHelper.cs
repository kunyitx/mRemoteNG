using System;
using mRemoteNG.App;
using mRemoteNG.Messages;
using mRemoteNG.UI.Window;

namespace mRemoteNG.UI.Tabs;

internal class TabHelper
{
    private static readonly Lazy<TabHelper> lazyHelper = new(() => new TabHelper());

    private ConnectionWindow currentPanel;

    private ConnectionTab currentTab;

    private TabHelper()
    {
    }

    public static TabHelper Instance => lazyHelper.Value;

    public ConnectionTab CurrentTab
    {
        get => currentTab;
        set
        {
            currentTab = value;
            findCurrentPanel();
            Runtime.MessageCollector.AddMessage(MessageClass.DebugMsg,
                "Tab got focused: " + currentTab.TabText);
        }
    }

    public ConnectionWindow CurrentPanel
    {
        get => currentPanel;
        set
        {
            currentPanel = value;
            Runtime.MessageCollector.AddMessage(MessageClass.DebugMsg,
                "Panel got focused: " + currentPanel.TabText);
        }
    }

    private void findCurrentPanel()
    {
        var currentForm = currentTab.Parent;
        while (currentForm != null && !(currentForm is ConnectionWindow)) currentForm = currentForm.Parent;

        if (currentForm != null)
            CurrentPanel = (ConnectionWindow)currentForm;
    }
}