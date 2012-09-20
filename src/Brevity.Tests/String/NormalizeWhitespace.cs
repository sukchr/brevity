// ReSharper disable InconsistentNaming

using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.String
{
	[TestFixture]
	public class NormalizeWhitespace
	{
		[Test]
		public void Normalize()
		{
			"        foo    bar     baz     ".NormalizeWhitespace().ShouldBe(" foo bar baz ");
		}
	}
}

// ReSharper restore InconsistentNaming