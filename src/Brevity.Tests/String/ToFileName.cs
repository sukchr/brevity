// ReSharper disable InconsistentNaming

using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.String
{
	[TestFixture]
	public class ToFileName
	{
		[Test]
		public void Valid_string_untouched()
		{
			"hello world".ToFileName().ShouldBe("hello world");
		}

		[Test]
		public void Spaces_are_trimmed()
		{
			"   hello world    ".ToFileName().ShouldBe("hello world");
		}

		[Test]
		public void Invalid_chars_replaced_with_default_char()
		{
			"hello:world:".ToFileName().ShouldBe("hello_world_");
		}

		[Test]
		public void Invalid_chars_replaced_with_specified_char()
		{
			"hello:world:".ToFileName('X').ShouldBe("helloXworldX");
		}

		[Test]
		public void When_replaceChar_is_invalid_underscore_is_used()
		{
			"hello:world:".ToFileName('*').ShouldBe("hello_world_");
		}
	}
}

// ReSharper restore InconsistentNaming