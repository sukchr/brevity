using Brevity.Velocity;
using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.String
{
    [TestFixture]
    public class VelocitySet
    {
        [Test]
        public void TemplateSet()
        {
            var result = "$greeting #if($subject) $subject #else???#end"
                .Set("greeting", "hello")
				//.Set("subject", "world")
                .Render();

            result.ShouldBe("hello ???");
        }
	}
}