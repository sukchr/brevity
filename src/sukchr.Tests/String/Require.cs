using System;
using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests.String
{
	[TestFixture]
	public class Require
	{
		[Test]
		public void Throws_when_string_is_null()
		{
			string nullString = null;
			Assert.Throws<ArgumentNullException>(() => nullString.Require("nullString")).Message.ShouldContain("nullString");
		}
		
		[Test]
		public void Throws_when_string_is_empty()
		{
			var emptyString = string.Empty;
			Assert.Throws<ArgumentException>(() => emptyString.Require("emptyString")).Message.ShouldContain("emptyString");
		}
		
		[Test]
		public void Doesnt_throw_when_string_is_defined()
		{
			var s = "foo";
			s.Require("s").ShouldBe(s);
		}
	}
}