using NUnit.Framework;
using ServerASMX.Test.Base.Base;
using ServerASMX.Test.Base.Extensions;

namespace ServerASMX.Test.Unit
{
    public class TestClass : DatabaseTest
    {
        public TestClass() { }

        [Test]
        public void Teste1()
        {
            var teste = MocksTest;

            var obj = new
            {
                Nome = "Lucas",
                Idade = 26,
                Vivo = true
            };

            TestContext.WriteLine(obj.Format());

            Assert.True(obj.Vivo);
        }

        [Test]
        public void Teste2()
        {
            Assert.True(true);
        }

        [Test]
        public void Teste3()
        {
            Assert.True(true);
        }
    }
}