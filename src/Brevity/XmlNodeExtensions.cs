using System.Xml;

namespace Brevity
{
	/// <summary>
	/// Extension methods for <see cref="XmlNode"/>.
	/// </summary>
	public static class XmlNodeExtensions
	{
		/// <summary>
		/// Gets the <see cref="XmlNode.InnerText"/> of the node that matches the <paramref name="xpath"/>.
		/// </summary>
		public static string Select(this XmlNode xmlNode, string xpath)
		{
			if (xmlNode == null)
				return null;

			if (string.IsNullOrEmpty(xpath))
				return null;

			var node = xmlNode.SelectSingleNode(xpath);

			if (node == null)
				return null;

			return node.InnerText;
		}
	}
}