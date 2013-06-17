using System.Globalization;
using Antlr4.StringTemplate;
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

	    [Test]
	    public void CustomRenderers()
	    {
			var result = @"$greeting$ $subject; format=""upper""$"
				.Set("greeting", "hello")
				.Set("subject", "world")
				.RegisterRenderer<string>(new ToUpperRenderer())
				.Render();

			result.ShouldBe("hello WORLD");
	    }
    }

	public class ToUpperRenderer : IAttributeRenderer
	{
		public string ToString(object obj, string formatString, CultureInfo culture)
		{
			var value = obj.ToString();

			if(formatString == "upper")
				return value.ToUpper();

			return value;
		}
	}
}