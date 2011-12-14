// ReSharper disable InconsistentNaming

using NUnit.Framework;

namespace Brevity.Tests.String
{
    [TestFixture]
    public class Post
    {
        [Test]
        public void Test()
        {
            "http://www.snee.com/xml/crud/posttest.cgi".Post("fname=foo&lname=bar"); 
        }
    }
}

// ReSharper restore InconsistentNaming