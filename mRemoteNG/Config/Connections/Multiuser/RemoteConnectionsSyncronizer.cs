using System;
using System.Timers;
using mRemoteNG.App;

// ReSharper disable ArrangeAccessorOwnerBody

namespace mRemoteNG.Config.Connections.Multiuser;

public class RemoteConnectionsSyncronizer : IConnectionsUpdateChecker
{
    private readonly IConnectionsUpdateChecker _updateChecker;
    private readonly Timer _updateTimer;

    public RemoteConnectionsSyncronizer(IConnectionsUpdateChecker updateChecker)
    {
        _updateChecker = updateChecker;
        _updateTimer = new Timer(3000);
        SetEventListeners();
    }

    public double TimerIntervalInMilliseconds
    {
        get { return _updateTimer.Interval; }
    }

    public bool IsUpdateAvailable()
    {
        return _updateChecker.IsUpdateAvailable();
    }

    public void IsUpdateAvailableAsync()
    {
        _updateChecker.IsUpdateAvailableAsync();
    }

    public event EventHandler UpdateCheckStarted;
    public event UpdateCheckFinishedEventHandler UpdateCheckFinished;
    public event ConnectionsUpdateAvailableEventHandler ConnectionsUpdateAvailable;


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void SetEventListeners()
    {
        _updateChecker.UpdateCheckStarted += OnUpdateCheckStarted;
        _updateChecker.UpdateCheckFinished += OnUpdateCheckFinished;
        _updateChecker.ConnectionsUpdateAvailable +=
            (sender, args) => ConnectionsUpdateAvailable?.Invoke(sender, args);
        _updateTimer.Elapsed += (sender, args) => _updateChecker.IsUpdateAvailableAsync();
        ConnectionsUpdateAvailable += Load;
    }

    private void Load(object sender, ConnectionsUpdateAvailableEventArgs args)
    {
        Runtime.ConnectionsService.LoadConnections(true, false, "");
        args.Handled = true;
    }

    public void Enable()
    {
        _updateTimer.Start();
    }

    public void Disable()
    {
        _updateTimer.Stop();
    }


    private void OnUpdateCheckStarted(object sender, EventArgs eventArgs)
    {
        _updateTimer.Stop();
        UpdateCheckStarted?.Invoke(sender, eventArgs);
    }

    private void OnUpdateCheckFinished(object sender, ConnectionsUpdateCheckFinishedEventArgs eventArgs)
    {
        _updateTimer.Start();
        UpdateCheckFinished?.Invoke(sender, eventArgs);
    }

    private void Dispose(bool itIsSafeToAlsoFreeManagedObjects)
    {
        if (!itIsSafeToAlsoFreeManagedObjects) return;
        _updateTimer.Dispose();
        _updateChecker.Dispose();
    }
}