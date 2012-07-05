using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.String
{
	[TestFixture]
	public class Format
	{
		[Test]
		public void FormatWith()
		{
			"{0}-{1}".FormatWith("foo", "bar").ShouldBe("foo-bar");
		}

	    [Test]
	    public void FormatShouldNotFailWithNullArgs()
	    {
	        Should.NotThrow( () => "foo{0}".FormatWith(null));
	    }
	}
}