// ReSharper disable InconsistentNaming

using NUnit.Framework;
using Shouldly;
using System.Linq;

namespace Brevity.Tests.String
{
    [TestFixture]
    public class Capture
    {
        [Test]
        public void Capture_example()
        {
            var captures = "foo{bar}{  baz }{}{  }<bah> {".Capture();
            captures.Count().ShouldBe(2);  //don't count empty elements
            captures.ShouldContain("bar");
            captures.ShouldContain("baz");
        }

        [Test]
        public void Should_not_capture_empty_strings()
        {
            var captures = "()  (   )".Capture(StringExtensions.Delimiter.RoundBrackets);
            captures.ShouldBeEmpty();
        }

        [Test]
        public void Captures_CurlyBrackets()
        {
            var captures = "(foo){bar}".Capture(StringExtensions.Delimiter.CurlyBrackets);
            captures.Count().ShouldBe(1);
            captures.ShouldContain("bar");
        }

        [Test]
        public void Captures_AngleBrackets()
        {
            var captures = "(foo)<bar>".Capture(StringExtensions.Delimiter.AngleBrackets);
            captures.Count().ShouldBe(1);
            captures.ShouldContain("bar");
        }

        [Test]
        public void Captures_Dollar()
        {
            var captures = "(foo)$bar$".Capture(StringExtensions.Delimiter.Dollar);
            captures.Count().ShouldBe(1);
            captures.ShouldContain("bar");
        }

        [Test]
        public void Captures_RoundBrackets()
        {
            var captures = "<foo>(bar)".Capture(StringExtensions.Delimiter.RoundBrackets);
            captures.Count().ShouldBe(1);
            captures.ShouldContain("bar");
        }

        [Test]
        public void Captures_SquareBrackets()
        {
            var captures = "<foo>[bar]".Capture(StringExtensions.Delimiter.SquareBrackets);
            captures.Count().ShouldBe(1);
            captures.ShouldContain("bar");
        }

        [Test]
        public void Capture_is_trimmed()
        {
            var captures = "(  foo  )".Capture(StringExtensions.Delimiter.RoundBrackets);
            captures.Count().ShouldBe(1);
            captures.ShouldContain("foo");
        }

        [Test]
        public void Empty_string_should_not_fail()
        {
            var captures = string.Empty.Capture(StringExtensions.Delimiter.RoundBrackets);
            captures.ShouldBeEmpty();
        }
    }
}

// ReSharper restore InconsistentNaming