// ReSharper disable InconsistentNaming

using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.XmlDocument
{
	[TestFixture]
	public class Select
	{
		[Test]
		public void Test()
		{
			var xml = "Brevity.Tests.Data.saml-assertion.xml"
				.OpenEmbeddedResource()
				.ToXml()
				.RegisterNamespaces();

			xml.Select("//saml:Assertion/@IssueInstant").ShouldBe("2012-07-16T12:33:01Z");
			xml.Select("//ds:DigestValue").ShouldBe("0MBtzQPiCf5RfWLmLpMw7arF76M=");
		}

		[Test]
		public void Non_existent_node_should_not_fail()
		{
			var xml = "Brevity.Tests.Data.saml-assertion.xml"
				.OpenEmbeddedResource()
				.ToXml()
				.RegisterNamespaces();

			xml.Select("//saml:FOO").ShouldBe(null);
		}

		[Test]
		public void Non_existent_node_should_not_fail_2()
		{
			var xml = "Brevity.Tests.Data.saml-assertion.xml"
				.OpenEmbeddedResource()
				.ToXml()
				.RegisterNamespaces();

			xml.Select("//saml:Assertion/@IssueInstant").ShouldBe("2012-07-16T12:33:01Z");
		}
	}
}

// ReSharper restore InconsistentNaming