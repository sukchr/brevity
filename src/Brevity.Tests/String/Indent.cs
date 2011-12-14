// ReSharper disable InconsistentNaming

using NUnit.Framework;

namespace Brevity.Tests.String
{
    [TestFixture]
    public class Indent
    {
        [Test]
        public void Test()
        {
            "no indent".Write();
            using(StringExtensions.Indent())
            {
                ("1 indent\nmore at 1 indent").Write();
                using (StringExtensions.Indent()) "2 indents".Write();
                "1 indent".Write();
            }
            "no indent".Write();
        }
    }
}

// ReSharper restore InconsistentNaming