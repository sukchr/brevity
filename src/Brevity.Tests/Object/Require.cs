using System;
using NUnit.Framework;
using Shouldly;
using System.Text;

namespace Brevity.Tests.Object
{
	[TestFixture]
	public class Require
	{
		[Test]
		public void Throws_when_object_is_null()
		{
			object nullObject = null;			
			Assert.Throws<ArgumentNullException>(() => nullObject.Require("nullObject")).Message.ShouldContain("nullObject");
		}
		
		[Test]
		public void Doesnt_throw_when_object_is_not_null()
		{
			var x = new StringBuilder();
			x.Require("x").ShouldBe(x);
		}
	}
}

