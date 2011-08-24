using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class Set
    {
        [Test]
        public void name()
        {
            var result = "$greeting$ $subject$"
                .Set("greeting", "hello")
                .Set("subject", "world")
                .Render();

            result.ShouldBe("hello world");
        }
    }
}