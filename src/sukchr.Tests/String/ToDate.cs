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
            DateTime date = "24.12.2010".ToDate().Value;
            Assert.AreEqual(2010, date.Year);
            Assert.AreEqual(12, date.Month);
            Assert.AreEqual(24, date.Day);
            Assert.AreEqual(0, date.Hour);
            Assert.AreEqual(0, date.Minute);
            Assert.AreEqual(0, date.Second);
        }

        [Test]
        public void EnglishAmerican_date()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            DateTime date = "12/24/2010".ToDate().Value;
            Assert.AreEqual(2010, date.Year);
            Assert.AreEqual(12, date.Month);
            Assert.AreEqual(24, date.Day);
            Assert.AreEqual(0, date.Hour);
            Assert.AreEqual(0, date.Minute);
            Assert.AreEqual(0, date.Second);
        }
    }
}

// ReSharper restore InconsistentNaming