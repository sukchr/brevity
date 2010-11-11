// ReSharper disable InconsistentNaming
using NUnit.Framework;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class FromJson
    {
        class Credentials
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        [Test]
        public void Deserializes_json_into_object()
        {
            var o = new Credentials
                        {
                            Username = "foo",
                            Password = "bar",
                        };
            var json = o.ToJson();

            var credentials = json.FromJson<Credentials>();

            Assert.AreEqual("foo", credentials.Username);
            Assert.AreEqual("bar", credentials.Password);
        }
    }
}

// ReSharper restore InconsistentNaming