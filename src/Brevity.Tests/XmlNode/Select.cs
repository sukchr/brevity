// ReSharper disable InconsistentNaming

using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.XmlNode
{
	[TestFixture]
	public class Select
	{
		private readonly System.Xml.XmlDocument _xml = @"<data>
<foo>
	<bar>hello</bar>
</foo>
</data>".ToXml();

		[Test]
		public void Test()
		{
			var node = _xml.SelectSingleNode("//foo");
			node.Select("bar").ShouldBe("hello");
		}

		[Test]
		public void Non_existent_node_should_not_fail()
		{
			var node = _xml.SelectSingleNode("//foo");
			node.Select("noob").ShouldBe(null);
		}
	}
}

// ReSharper restore InconsistentNaming