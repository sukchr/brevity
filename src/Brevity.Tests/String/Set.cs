using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.String
{
    [TestFixture]
    public class Set
    {
        [Test]
        public void TemplateSet()
        {
            var result = "$greeting$ $subject$"
                .Set("greeting", "hello")
                .Set("subject", "world")
                .Render();

            result.ShouldBe("hello world");
        }

        [Test]
        public void ImplicitConversion()
        {
            string result = "$greeting$ $subject$"
                .Set("greeting", "hello")
                .Set("subject", "world");

            result.ShouldBe("hello world");
        }
    }
}