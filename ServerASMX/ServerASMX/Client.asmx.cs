using ServerASMX.Domain.Clients.Commands.Input;
using ServerASMX.Domain.Clients.Handlers;
using ServerASMX.Domain.Clients.Interfaces.Handlers;
using ServerASMX.Domain.Core.Commands.Interfaces;
using System.ComponentModel;
using System.Web.Services;

namespace ServerASMX
{
    [WebService(Namespace = "http://servidorasmx.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class Client : WebService
    {
        private readonly IClientHandler _handler;

        public Client()
        {
            _handler = new ClientHandler();
        }

        [WebMethod]
        public object AddClient(ClientAddCommand client)
        {
            return null;
        }
    }
}