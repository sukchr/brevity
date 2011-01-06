// ReSharper disable InconsistentNaming
using NUnit.Framework;
using System.Linq;
using Shouldly;

namespace sukchr.Tests.Object
{
    [TestFixture]
    public class TryGet
    {
        class Person
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        [Test]
        public void Object_is_not_null()
        {
            var people = new[] {new Person{Id = "1", Name = "foo"}, new Person { Id = "2", Name= "bar"}};
            people.FirstOrDefault(person => person.Id == "2").TryGet(person => person.Name).ShouldBe("bar");
        }

        [Test]
        public void Object_is_null()
        {
            var people = new[] { new Person { Id = "1", Name = "foo" }, new Person { Id = "2", Name = "bar" } };
            people.FirstOrDefault(person => person.Id == "3").TryGet(person => person.Name).ShouldBe(null);
        }
    }
}

// ReSharper restore InconsistentNaming