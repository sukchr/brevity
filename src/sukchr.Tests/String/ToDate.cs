// ReSharper disable InconsistentNaming
using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class ToDate
    {
        [Test]
        public void Norwegian_date()
        {
            using (Culture.Scope("nb-NO"))
            {
                var date = "24.12.2010".ToDateTime();
                Assert.AreEqual(2010, date.Year);
                Assert.AreEqual(12, date.Month);
                Assert.AreEqual(24, date.Day);
                Assert.AreEqual(0, date.Hour);
                Assert.AreEqual(0, date.Minute);
                Assert.AreEqual(0, date.Second);
            }
        }

        [Test]
        public void EnglishAmerican_date()
        {
            using (Culture.Scope("en-US"))
            {
                var date = "12/24/2010".ToDateTime();
                Assert.AreEqual(2010, date.Year);
                Assert.AreEqual(12, date.Month);
                Assert.AreEqual(24, date.Day);
                Assert.AreEqual(0, date.Hour);
                Assert.AreEqual(0, date.Minute);
                Assert.AreEqual(0, date.Second);
            }
        }

        [Test]
        public void Given_invalid_date_returns_DateTime_Min()
        {
            Assert.AreEqual(DateTime.MinValue, "foo".ToDateTime());
        }
    }
}

// ReSharper restore InconsistentNaming