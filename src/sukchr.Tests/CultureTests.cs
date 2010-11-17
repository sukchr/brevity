// ReSharper disable InconsistentNaming
using System.Globalization;
using System.Threading;
using NUnit.Framework;
using Shouldly;
namespace sukchr.Tests
{
    [TestFixture]
    public class CultureTests
    {
        private CultureInfo _preservedCulture;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _preservedCulture = Thread.CurrentThread.CurrentCulture;
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            Thread.CurrentThread.CurrentCulture = _preservedCulture;
        }

        [Test]
        public void When_changing_culture_scope_should_revert_the_changes_after_disposal()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            using(Culture.Scope("nb-NO"))
            {
                Thread.CurrentThread.CurrentCulture.Name.ShouldBe("nb-NO");
            }

            Thread.CurrentThread.CurrentCulture.Name.ShouldBe("en-US");
        }
    }
}

// ReSharper restore InconsistentNaming