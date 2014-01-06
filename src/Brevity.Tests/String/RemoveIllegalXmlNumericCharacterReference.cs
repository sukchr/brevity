using Shouldly;
using NUnit.Framework;

namespace Brevity.Tests.String
{
	[TestFixture]
	public class RemoveIllegalXmlNumericCharacterReference
	{
		[Test]
		public void Test()
		{
			"Foo&#x9;&#x11;&#x13;Bar".RemoveIllegalXmlNumericCharacterReference().ShouldBe("Foo&#x9;Bar");
		}
	}
}