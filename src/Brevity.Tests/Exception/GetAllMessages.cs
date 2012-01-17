// ReSharper disable InconsistentNaming

using System;
using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.Exception
{
    [TestFixture]
    public class GetAllMessages
    {
        [Test]
        public void No_inner_exceptions()
        {
            try
            {
                throw new ArgumentException("ex1");
            }
            catch (System.Exception ex1)
            {
                ex1.GetAllMessages().ShouldContain("System.ArgumentException: ex1");
            }
        }

        [Test]
        public void Exception_with_inner_exceptions()
        {
            try
            {
                throw new ArgumentException("ex1");
            }
            catch (System.Exception ex1)
            {
                try
                {
                    throw new ApplicationException("ex2", ex1);
                }
                catch (System.Exception ex2)
                {
                    try
                    {
                        throw new InvalidOperationException("ex3", ex2);
                    }
                    catch (System.Exception ex3)
                    {
                        ex3.GetAllMessages().ShouldBe("System.InvalidOperationException: ex3 ---> System.ApplicationException: ex2 ---> System.ArgumentException: ex1");   
                    }
                }
            }
        }
    }
}

// ReSharper restore InconsistentNaming