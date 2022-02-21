using ServerASMX.Domain.Clients.Handlers;
using ServerASMX.Domain.Clients.Interfaces.Handlers;
using ServerASMX.Domain.Clients.Interfaces.Repositories;
using ServerASMX.Domain.Clients.Repositories;
using System.ComponentModel;
using System.Web.Services;

namespace ServerASMX
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class ClientService : WebService
    {
        private readonly IClientHandler _handler;
        private readonly IClientRepository _repository;

        public ClientService()
        {
            _handler = new ClientHandler();
            _repository = new ClientRepository();
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Olá, Mundo";
        }
    }
}
