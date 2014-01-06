using Shouldly;
using NUnit.Framework;

namespace Brevity.Tests.String
{
	[TestFixture]
	public class RemoveIllegalXmlEntityRefs
	{
		[Test]
		public void Test()
		{
			"Foo&#x9;&#x11;&#x13;Bar".RemoveIllegalXmlEntityRefs().ShouldBe("Foo&#x9;Bar");
		}
	}
}