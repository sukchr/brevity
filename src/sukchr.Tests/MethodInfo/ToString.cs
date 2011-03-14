// ReSharper disable InconsistentNaming
using System.Linq;
using reflection = System.Reflection;
using NUnit.Framework;

namespace sukchr.Tests.MethodInfo
{
    [TestFixture]
    public class ToString
    {
        private reflection.MethodInfo _method1;
        private reflection.MethodInfo _method2;
        private reflection.MethodInfo _method3;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _method1 = typeof(C).GetMember("Method1").First() as reflection.MethodInfo;
            _method2 = typeof(C).GetMember("Method2").First() as reflection.MethodInfo;
            _method3 = typeof(C).GetMember("Method3").First() as reflection.MethodInfo;
        }

        [Test]
        public void Method_with_no_args()
        {
            Assert.AreEqual("Method1()", _method1.ToString(null));
        }

        [Test]
        public void Method_with_single_arg()
        {
            Assert.AreEqual("Method2('foo')", _method2.ToString(new object[] { "foo" }));
        }

        [Test]
        public void Method_with_multiple_args()
        {
            Assert.AreEqual("Method3('foo',3)", _method3.ToString(new object[] { "foo", 3 }));
        }

        [Test]
        public void Method_with_stringEmpty_arg()
        {
            Assert.AreEqual("Method3('',3)", _method3.ToString(new object[] { string.Empty, 3 }));
        }

        [Test]
        public void Method_with_null_arg()
        {
            Assert.AreEqual("Method3(null,3)", _method3.ToString(new object[] { null, 3 }));
        }

        [Test]
        public void With_parameter_names()
        {
            Assert.AreEqual("Method3(x=null,y=3)", _method3.ToString(true, new object[] { null, 3 }));
        }
    }

    public class C
    {
        public void Method1() { }
        public void Method2(string x) { }
        public void Method3(string x, int y) { }
    }
}

// ReSharper restore InconsistentNaming