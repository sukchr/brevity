using System;

namespace Brevity.FixedRecord
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class FixedColumnAttribute : Attribute
	{
		internal int From { get; private set; }
		internal int To { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="position">1-based position (i.e. position starts at 1, NOT 0).</param>
		public FixedColumnAttribute(int position)
		{
			From = position;
			To = position;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="from">1-based position (i.e. position starts at 1, NOT 0).</param>
		/// <param name="to">To is inclusive.</param>
		public FixedColumnAttribute(int from, int to)
		{
			From = from;
			To = to;
		}

		/// <summary>
		/// Calculates the length as <see cref="From"/> + 1 - <see cref="To"/>. If From = 1 and To = 1, length is 1. If from = 1 and To = 2, length is 2, and so forth.
		/// </summary>
		internal int Length { get { return To + 1 - From; } }
	}
}
