using System;
using System.Linq;
using System.Xml;

namespace Brevity
{
	/// <summary>
	/// An XmlDocument that also contains an <see cref="XmlNamespaceManager"/> that has all namespace-aliases mapped.
	/// </summary>
	public sealed class XmlDocumentWithNamespaces : XmlDocument
	{
		private readonly XmlNamespaceManager _namespaceManager;

		internal XmlNamespaceManager NamespaceManager { get { return _namespaceManager; } }

		internal XmlDocumentWithNamespaces(XmlDocument xml)
		{
			if (xml == null) throw new ArgumentNullException("xml");

			LoadXml(xml.OuterXml);
			_namespaceManager = PopulateNamespaces(this);
		}

		/// <summary>
		/// From http://stackoverflow.com/a/4819510.
		/// </summary>
		/// <param name="document"></param>
		/// <returns></returns>
		private static XmlNamespaceManager PopulateNamespaces(XmlDocument document)
		{
			var namespaceManager = new XmlNamespaceManager(document.NameTable);
			var namespaces = (from XmlNode n in document.SelectNodes("//*|@*")
							  where n.NamespaceURI != string.Empty
							  select new
							  {
								  n.Prefix,
								  Namespace = n.NamespaceURI
							  }).Distinct();

			foreach (var @namespace in namespaces)
				namespaceManager.AddNamespace(@namespace.Prefix, @namespace.Namespace);

			return namespaceManager;
		}
	}

	/// <summary>
	/// Extension methods for <see cref="XmlDocument"/>.
	/// </summary>
	public static class XmlDocumentExtensions
	{
		/// <summary>
		/// Registers all namespacealiases in the XML document so that you can use them in <see cref="Select(System.Xml.XmlDocument,string)"/>.
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public static XmlDocumentWithNamespaces RegisterNamespaces(this XmlDocument xml)
		{
			return new XmlDocumentWithNamespaces(xml);
		}

		/// <summary>
		/// Gets the <see cref="XmlNode.InnerText"/> of the node that matches the <paramref name="xpath"/>.
		/// </summary>
		public static string Select(this XmlDocument xml, string xpath)
		{
			if (xml == null)
				return null;

			if (string.IsNullOrEmpty(xpath))
				return null;

			var node = xml.SelectSingleNode(xpath);

			if (node == null)
				return null;

			return node.InnerText;
		}

		/// <summary>
		/// Gets the <see cref="XmlNode.InnerText"/> of the node that matches the <paramref name="xpath"/>. You can use namespace aliases in the <paramref name="xpath"/> expression.
		/// </summary>
		public static string Select(this XmlDocumentWithNamespaces xml, string xpath)
		{
			if (xml == null)
				return null;

			if (string.IsNullOrEmpty(xpath))
				return null;

			var node = xml.SelectSingleNode(xpath, xml.NamespaceManager);

			if (node == null)
				return null;

			return node.InnerText;
		}
	}
}