using System;
using NUnit.Framework;
using Brevity.FixedRecord;
using Shouldly;

namespace Brevity.Tests.FixedRecord
{
	[FixedRecord]
	public class ValidFixedRecord
	{
		[FixedColumn(1, 3)]
		public string Column1 { get; set; }
		[FixedColumn(4, 6)]
		public string Column2 { get; set; }
		[FixedColumn(7, 9)]
		public string Column3 { get; set; }
		[FixedColumn(10)]
		public string Column4 { get; set; }
	}
	
	[FixedRecord]
	public class ColumnCollision
	{
		[FixedColumn(1, 4)]
		public string Column1 { get; set; }
		[FixedColumn(4, 6)]
		public string Column2 { get; set; }
	}

	[TestFixture]
	public class ToFixedRecord
	{
		[Test]
		public void Valid1_all_properties()
		{
			var rec = new ValidFixedRecord();
			rec.Column1 = "a";
			rec.Column2 = "b";
			rec.Column3 = "c";

			var result = rec.ToFixedRecord();

			result.ShouldBe("a  b  c   ");
		}

		[Test]
		public void Valid2_Empty_property_replaced_with_whitespace()
		{
			var rec = new ValidFixedRecord();
			rec.Column1 = "a";
			rec.Column3 = "c";
			rec.Column4 = "d";
			var result = rec.ToFixedRecord();

			result.ShouldBe("a     c  d");
		}

		[Test]
		public void Valid3_all_empty()
		{
			var rec = new ValidFixedRecord();

			var result = rec.ToFixedRecord();

			result.ShouldBe("          ");
		}

		[Test]
		public void Column_collision()
		{
			var rec = new ColumnCollision();

			var ex = Should.Throw<ArgumentException>(() => rec.ToFixedRecord());
			ex.Message.ShouldContain("Collision");
		}

		[Test]
		public void Value_overflow_throws_exception()
		{
			var rec = new ValidFixedRecord();

			rec.Column1 = "hello world";

			var ex = Should.Throw<ArgumentException>(() => rec.ToFixedRecord());
			ex.Message.ShouldContain("is longer");
		}
	}
}
