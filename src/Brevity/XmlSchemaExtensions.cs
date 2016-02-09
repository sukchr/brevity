using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Brevity
{
	public static class XmlSchemaExtensions
	{
		public static bool IsValid(this XmlSchema xsd, XmlDocument xml, Action<ValidationEventArgs> onErrorAction = null)
		{
			if (xsd == null) throw new ArgumentNullException("xsd");
			if (xml == null) throw new ArgumentNullException("xml");

			var isValid = true;

			var settings = new XmlReaderSettings();
			settings.Schemas.Add(xsd);
			settings.ValidationType = ValidationType.Schema;
			settings.ValidationEventHandler += (sender, args) =>
			{
				isValid = false;

				if (onErrorAction != null)
					onErrorAction(args);
			};

			var xmlReader = XmlReader.Create(new StringReader(xml.OuterXml), settings);

			while (xmlReader.Read()) { }

			return isValid;
		}
	}
}
