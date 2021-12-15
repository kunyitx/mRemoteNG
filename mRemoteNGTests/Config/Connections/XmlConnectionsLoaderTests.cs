using System.IO;
using mRemoteNG.Config.Connections;
using mRemoteNGTests.TestHelpers;
using NUnit.Framework;

namespace mRemoteNGTests.Config.Connections;

internal class XmlConnectionsLoaderTests
{
    [Test]
    public void ThrowsFileNotFound()
    {
        Assert.Throws<FileNotFoundException>(() => new XmlConnectionsLoader(FileTestHelpers.NewTempFilePath()).Load());
    }
}