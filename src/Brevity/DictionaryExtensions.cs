using System.Collections.Generic;
using System.Text;

namespace Brevity
{
	public static class DictionaryExtensions
	{
		public static string ToString(this Dictionary<string, string> dictionary, string keyValueSeparator, string itemPrefix = null, string itemPostfix = null, string itemSeparator = null)
		{
			if(dictionary == null)
				return null;

			if(dictionary.Count == 0)
				return string.Empty;

			var text = new StringBuilder();

			foreach (var key in dictionary.Keys)
			{
				if(!string.IsNullOrEmpty(itemSeparator) && text.Length != 0)
					text.Append(itemSeparator);

				text.AppendFormat("{0}{1}{2}{3}{4}", itemPrefix, key, keyValueSeparator, dictionary[key], itemPostfix);
			}

			return text.ToString();
		}
	}
}