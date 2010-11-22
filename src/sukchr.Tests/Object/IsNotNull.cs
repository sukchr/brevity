using System;
using System.Text;
using Shouldly;
using NUnit.Framework;

namespace sukchr.Tests.Object
{
	[TestFixture]
	public class IsNotNull
	{
		[Test]
		public void Is_true_when_object_is_not_null ()
		{
			var x = new StringBuilder();
			x.IsNotNull().ShouldBe(true);
		}
		
		[Test]
		public void Is_false_when_object_is_null ()
		{
			StringBuilder x = null;
			x.IsNotNull().ShouldBe(false);
		}
	}
}