using Shouldly;
// ReSharper disable InconsistentNaming
using NUnit.Framework;

namespace Brevity.Tests.Dictionary
{
	[TestFixture]
	public class ToString
	{
		[Test]
		public void Test1()
		{
			var dictionary = new System.Collections.Generic.Dictionary<string, string>
			{
				{"foo", "a"},
				{"bar", "b"},
			};

			dictionary.ToString("=", itemSeparator: "\n").ShouldBe("foo=a\nbar=b");
		}

		[Test]
		public void Test2()
		{
			var dictionary = new System.Collections.Generic.Dictionary<string, string>
			{
				{"foo", "a"},
				{"bar", "b"},
			};

			dictionary.ToString("=", "{", "}", ",").ShouldBe("{foo=a},{bar=b}");
		}

		[Test]
		public void Test3()
		{
			var dictionary = new System.Collections.Generic.Dictionary<string, string>
			{
				{"foo", "a"},
				{"bar", "b"},
			};

			dictionary.ToString("\": \"", "{\"", "\"}", ",\n").ShouldBe("{\"foo\": \"a\"},\n{\"bar\": \"b\"}");
		}
	}
}

// ReSharper restore InconsistentNaming