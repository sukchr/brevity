// ReSharper disable InconsistentNaming
using System.Security.Cryptography;
using NUnit.Framework;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class ComputeHash
    {
        [Test]
        public void ComputeMD5()
        {
            "foo".ComputeHash().Write();
        }

        [Test]
        public void ComputeSHA256()
        {
            "foo".ComputeHash(SHA256.Create()).Write();
        }
    }
}

// ReSharper restore InconsistentNaming