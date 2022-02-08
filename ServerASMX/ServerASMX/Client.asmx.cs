using System.ComponentModel;
using System.Web.Services;

namespace ServerASMX
{
    [WebService(Namespace = "http://servidorasmx.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class Client : WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Olá, Mundo";
        }
    }
}