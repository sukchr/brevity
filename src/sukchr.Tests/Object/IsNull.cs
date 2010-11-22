using System;
using System.Text;
using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests.Object
{
	[TestFixture]
	public class IsNull
	{	
		[Test]
		public void True_when_object_is_null()
		{
			StringBuilder x = null;
			x.IsNull().ShouldBe(true);
		}
		
		[Test]
		public void False_when_object_is_not_null ()
		{
			var x = new StringBuilder ();
			x.IsNull ().ShouldBe (false);
		}	
		
		[Test]
		public void Lambda_executed_when_object_is_null ()
		{
			StringBuilder x = null;
			var run = false;
			Action setRun = () => run = true;
			x.IsNull (setRun);
			run.ShouldBe (true);
		}
		
		[Test]
		public void Lambda_not_executed_when_object_is_not_null ()
		{
			var x = new StringBuilder();
			var run = false;
			Action setRun = () => run = true;
			x.IsNull (setRun);
			run.ShouldBe (false);
		}	
	}
}
