// ReSharper disable InconsistentNaming

using System;
using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.String
{
	[TestFixture]
	public class FromBase64
	{
		[Test]
		public void Test()
		{
			var data = new byte[] {0, 1, 2};
			var base64 = Convert.ToBase64String(data);
			base64.FromBase64().ShouldBe(data);
		}

		[Test]
		public void Null()
		{
			string base64 = null;
// ReSharper disable ExpressionIsAlwaysNull
			base64.FromBase64().ShouldBe(null);
// ReSharper restore ExpressionIsAlwaysNull
		}

		[Test]
		public void Empty()
		{
			string.Empty.FromBase64().ShouldBe(Convert.FromBase64String(string.Empty));
		}
	}
}

// ReSharper restore InconsistentNaming