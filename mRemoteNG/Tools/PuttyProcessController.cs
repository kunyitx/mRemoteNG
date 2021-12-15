using mRemoteNG.App.Info;
using mRemoteNG.Properties;
using mRemoteNG.Tools.Cmdline;

namespace mRemoteNG.Tools;

public class PuttyProcessController : ProcessController
{
    public bool Start(CommandLineArguments arguments = null)
    {
        var filename = Settings.Default.UseCustomPuttyPath
            ? Settings.Default.CustomPuttyPath
            : GeneralAppInfo.PuttyPath;
        return Start(filename, arguments);
    }
}