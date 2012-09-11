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
		public void SomethingIsNotSet()
		{
			var result = "$greeting$ $subject$"
				.Set("greeting", "hello")
				.Render();

			result.ShouldBe("hello ");
		}

		[Test]
		public void SetNull()
		{
			var result = "$greeting$ $subject$"
				.Set("greeting", "hello")
				.Set("subject", null)
				.Render();

			result.ShouldBe("hello ");
		}

		[Test]
		public void SetEmptyString()
		{
			var result = "$greeting$ $subject$"
				.Set("greeting", "hello")
				.Set("subject", string.Empty)
				.Render();

			result.ShouldBe("hello ");
		}

        [Test]
        public void ImplicitConversion()
        {
            string result = "$greeting$ $subject$"
                .Set("greeting", "hello")
                .Set("subject", "world");

            result.ShouldBe("hello world");
        }

        [Test]
        public void CustomDelimiters()
        {
            var result = "<greeting> <subject>"
                .Set("greeting", "hello")
                .Set("subject", "world")
                .Render(StringExtensions.Delimiter.AngleBrackets);

            result.ShouldBe("hello world");
        }
    }
}