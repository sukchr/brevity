using System;
using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests
{
	[TestFixture]
	public class Format
	{
		[Test]
		public void Format_string()
		{
			"{0}-{1}".FormatString("foo", "bar").ShouldBe("foo-bar");
		}
	}
}