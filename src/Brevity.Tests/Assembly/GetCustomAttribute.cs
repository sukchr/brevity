// ReSharper disable InconsistentNaming

using System.Reflection;
using NUnit.Framework;
using Shouldly;

[assembly: AssemblyInformationalVersion("1.1.1.1")]
[assembly: AssemblyFileVersion("3.3.3.3")]

namespace Brevity.Tests.Assembly
{
    [TestFixture]
    public class GetCustomAttribute
    {
        [Test]
        public void Test()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var infoVersion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var fileVersion = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
            var version = assembly.GetCustomAttribute<AssemblyVersionAttribute>();

            infoVersion.InformationalVersion.ShouldBe("1.1.1.1");
            fileVersion.Version.ShouldBe("3.3.3.3");
            version.ShouldBe(null);
        }
    }
}

// ReSharper restore InconsistentNaming