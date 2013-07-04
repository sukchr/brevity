// ReSharper disable InconsistentNaming

using System;
using Company.Product;
using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.XmlDocument
{
	[TestFixture]
	public class Transform
	{
		[Test]
		public void Given_no_xml_does_nothing()
		{
			System.Xml.XmlDocument xml = null;
			xml.Transform(new System.Xml.XmlDocument()).ShouldBe(null);
		}

		[Test]
		public void Given_no_xsl_does_nothing()
		{
			var xml = @"<hello>world</hello>".ToXml();
			System.Xml.XmlDocument xsl = null;
			xml.Transform(xsl).ShouldBe(xml);
		}

		[Test]
		public void Given_xml_and_xsl_then_performs_transformation()
		{
			var xml = @"<hello>world</hello>".ToXml();
			var xsl =
@"<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='1.0'>

	<xsl:template match='/'>
		<goodbye><xsl:value-of select='//hello' /></goodbye>
	</xsl:template>
</xsl:stylesheet>".ToXml();

			var transformedXml = xml.Transform(xsl);

			transformedXml.Select("/goodbye").ShouldBe("world");
		}

		[Test]
		public void Works_with_xsltargs()
		{
			var xml = @"<hello>world</hello>".ToXml();
			var xsl =
@"<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='1.0'>

	<xsl:param name='greeting' />

	<xsl:template match='/'>
		<xsl:element name='{$greeting}' >
			<xsl:value-of select='//hello' />
		</xsl:element>
	</xsl:template>
</xsl:stylesheet>".ToXml();

			var transformedXml = xml.Transform(xsl, xsltArguments: new[] { new Tuple<string, object>("greeting", "bonjour") });

			transformedXml.Select("/bonjour").ShouldBe("world");
		}

		[Test]
		public void Works_with_extension_objects()
		{
			var xml = @"<hello>world</hello>".ToXml();
			var xsl =
@"<xsl:stylesheet version='1.0'
	xmlns:xsl='http://www.w3.org/1999/XSL/Transform' 
	xmlns:Company.Product='Company.Product'>

	<xsl:template match='/'>
		<goodbye>
			<xsl:value-of select='Company.Product:ToUpper(//hello)' />
		</goodbye>
	</xsl:template>
</xsl:stylesheet>".ToXml();

			var transformedXml = xml.Transform(xsl, extensionObjects: new object[] { new ToUpperExtensionObject() });

			transformedXml.Select("/goodbye").ShouldBe("WORLD");
		}
	}
}

namespace Company.Product
{
	public class ToUpperExtensionObject
	{
		public string ToUpper(string text)
		{
			return string.IsNullOrEmpty(text) ? text : text.ToUpper();
		}
	}
}

// ReSharper restore InconsistentNaming