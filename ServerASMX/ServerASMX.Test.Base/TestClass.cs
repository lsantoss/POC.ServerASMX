using NUnit.Framework;
using ServerASMX.Test.Base.Extensions;

namespace ServerASMX.Test.Base
{
    public class TestClass
    {
        [Test]
        public void Teste1()
        {
            var obj = new
            {
                Nome = "Lucas",
                Idade = 26,
                Vivo = true
            };

            TestContext.WriteLine(obj.Format());

            Assert.True(obj.Vivo);
        }
    }
}