using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Brevity.FixedRecord
{
	public static class FixedRecordExtensions
	{
		public static string ToFixedRecord(this object recordObject)
		{
			var type = recordObject.GetType();

			var attributes = type.GetCustomAttributes(typeof(FixedRecordAttribute), false);

			if (attributes == null || attributes.Length == 0)
				throw new ArgumentException(type.Name + " is not decorated with " + typeof(FixedRecordAttribute).Name, "recordObject");

			var columns = new List<Tuple<string, FixedColumnAttribute>>();

			//first pass to validate some basist stuff (collision, any fixed columns)
			foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
			{
				attributes = property.GetCustomAttributes(typeof (FixedColumnAttribute), false);

				if (attributes.Length == 0)
					continue;

				var fixedColumnAttribute = (FixedColumnAttribute) attributes.First();

				//check for collision
				if (columns.Count != 0)
				{
					foreach (var column in columns)
					{
						if(IsBetween(fixedColumnAttribute.From, column.Item2.From, column.Item2.To))
							throw new ArgumentException("Collision, check from/to on properties {0} and {1}.".FormatWith(property.Name, column.Item1));
						if(IsBetween(fixedColumnAttribute.To, column.Item2.From, column.Item2.To))
							throw new ArgumentException("Collision, check from/to on properties {0} and {1}.".FormatWith(property.Name, column.Item1));
					}
				}

				columns.Add(new Tuple<string, FixedColumnAttribute>(property.Name, fixedColumnAttribute));
			}

			if(columns.Count == 0)
				throw new ArgumentException(type.Name + " has no properties decorated with " + typeof(FixedColumnAttribute).Name, "recordObject");

			var result = new StringBuilder();
			
			//second pass to actually build the string
			foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
			{
				attributes = property.GetCustomAttributes(typeof (FixedColumnAttribute), false);

				if (attributes.Length == 0)
					continue;

				var fixedColumnAttribute = (FixedColumnAttribute)attributes.First();

				//validate column attribute
				if (fixedColumnAttribute.To < fixedColumnAttribute.From)
				{
					throw new ArgumentException(
						"{0}.{1} has invalid From/To values: From={2}, To={3}. To must be greater than or equal to From.".FormatWith(
							type.Name, 
							property.Name,
							fixedColumnAttribute.From,
							fixedColumnAttribute.To),
						"recordObject");
				}

				result.EnsureCapacity(fixedColumnAttribute.To);

				//get value, check that value is within bounds
				var value = property.GetValue(recordObject, null) as string; //TODO: support other types as well.

				if (string.IsNullOrWhiteSpace(value))
				{
					result.Insert(fixedColumnAttribute.From - 1, string.Empty.PadRight(fixedColumnAttribute.Length));
					continue;
				}

				if(value.Length > fixedColumnAttribute.Length)
					throw new ArgumentException(
						"Value of {0}.{1} is longer than allowed From/To: From={2}, To={3} (Length = {4}), actual value length = {5}.".FormatWith(
							type.Name,
							property.Name,
							fixedColumnAttribute.From,
							fixedColumnAttribute.To,
							fixedColumnAttribute.Length,
							value.Length),
						"recordObject");

				result.Insert(fixedColumnAttribute.From - 1, value.PadRight(fixedColumnAttribute.Length));
			}

			return result.ToString();
		}

		private static bool IsBetween(int test, int from, int to)
		{
			return test >= from && test <= to;
		}
	}
}
