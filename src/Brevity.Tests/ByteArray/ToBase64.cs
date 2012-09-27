// ReSharper disable InconsistentNaming

using System;
using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.ByteArray
{
	[TestFixture]
	public class ToBase64
	{
		[Test]
		public void Test()
		{
			var binary = new byte[] { 1, 2, 3 };
			binary.ToBase64().ShouldBe(Convert.ToBase64String(binary));
		}

		[Test]
		public void Null()
		{
			byte[] binary = null;
// ReSharper disable ExpressionIsAlwaysNull
			binary.ToBase64().ShouldBe(null);
// ReSharper restore ExpressionIsAlwaysNull
		}

		[Test]
		public void Empty()
		{
			var binary = new byte[] { };
			binary.ToBase64().ShouldBe(Convert.ToBase64String(binary));
		}
	}
}

// ReSharper restore InconsistentNaming