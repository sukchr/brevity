// ReSharper disable InconsistentNaming

using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.String
{
	[TestFixture]
	public class Parse
	{
		[Test]
		public void DefaultSeparators()
		{
			var dictionary =
				@"x=1
foo=bar".ToDictionary();

			dictionary.Count.ShouldBe(2);

			dictionary.ContainsKey("x").ShouldBe(true);
			dictionary["x"].ShouldBe("1");
	
			dictionary.ContainsKey("foo").ShouldBe(true);
			dictionary["foo"].ShouldBe("bar");
		}

		[Test(Description = "Uses -> instead of equal sign and pipe instead of newline.")]
		public void CustomSeparators()
		{
			var dictionary =
				@"x->1|foo->bar".ToDictionary("->", "|");

			dictionary.Count.ShouldBe(2);
			
			dictionary.ContainsKey("x").ShouldBe(true);
			dictionary["x"].ShouldBe("1");
			
			dictionary.ContainsKey("foo").ShouldBe(true);
			dictionary["foo"].ShouldBe("bar");
		}

		[Test]
		public void WhitespaceIsIgnored()
		{
			var dictionary =
				@"

    x   =    1     

    foo    =    bar     

".ToDictionary();

			dictionary.Count.ShouldBe(2);
			
			dictionary.ContainsKey("x").ShouldBe(true);
			dictionary["x"].ShouldBe("1");
			
			dictionary.ContainsKey("foo").ShouldBe(true);
			dictionary["foo"].ShouldBe("bar");			
		}

		[Test]
		public void Tolerance()
		{
			var dictionary =
				@"=
=123
x
y=
z=     ".ToDictionary();

			dictionary.Count.ShouldBe(4);

			dictionary.ContainsKey(string.Empty).ShouldBe(true);
			dictionary[string.Empty].ShouldBe("123");

			dictionary.ContainsKey("x").ShouldBe(true);
			dictionary["x"].ShouldBe(null);

			dictionary.ContainsKey("y").ShouldBe(true);
			dictionary["y"].ShouldBe(string.Empty);

			dictionary.ContainsKey("z").ShouldBe(true);
			dictionary["z"].ShouldBe(string.Empty);
		}

		[Test]
		public void DefaultComment()
		{
			var dictionary =
				@"x=1 #this is a comment
#foo=bar".ToDictionary();

			dictionary.Count.ShouldBe(1);

			dictionary.ContainsKey("x").ShouldBe(true);
			dictionary["x"].ShouldBe("1");
		}

		[Test]
		public void CustomComment()
		{
			var dictionary =
				@"x=1 //this is a comment
//foo=bar".ToDictionary(lineComment: "//");

			dictionary.Count.ShouldBe(1);

			dictionary.ContainsKey("x").ShouldBe(true);
			dictionary["x"].ShouldBe("1");
		}
	}
}

// ReSharper restore InconsistentNaming