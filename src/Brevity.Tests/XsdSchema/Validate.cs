using System.Xml.Schema;
using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.XsdSchema
{
	[TestFixture]
	public class Validate
	{
		[Test]
		public void Test_valid()
		{
			var xsd = XmlSchema.Read(@"Brevity.Tests.XsdSchema.schema.xsd".OpenEmbeddedResource(), null);

			var xml = @"Brevity.Tests.XsdSchema.valid.xml".OpenEmbeddedResource().ToXml();

			var isValid = xsd.IsValid(xml);

			isValid.ShouldBe(true);
		}

		[Test]
		public void Test_invalid()
		{
			var xsd = XmlSchema.Read(@"Brevity.Tests.XsdSchema.schema.xsd".OpenEmbeddedResource(), null);

			var xml = @"Brevity.Tests.XsdSchema.invalid.xml".OpenEmbeddedResource().ToXml();

			var isValid = xsd.IsValid(xml);

			isValid.ShouldBe(false);
		}
	}
}
