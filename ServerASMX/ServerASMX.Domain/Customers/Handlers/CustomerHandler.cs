using ServerASMX.Domain.Customers.Repositories;
using ServerASMX.Domain.Core.Commands.Result;
using ServerASMX.Domain.Customers.Commands.Input;
using ServerASMX.Domain.Customers.Interfaces.Handlers;
using ServerASMX.Domain.Customers.Interfaces.Repositories;
using System.Threading.Tasks;

namespace ServerASMX.Domain.Customers.Handlers
{
    public class CustomerHandler : ICustomerHandler
    {
        private readonly ICustomerRepository _repository;

        public CustomerHandler()
        {
            _repository = new CustomerRepository();
        }

        public async Task<CommandResult> Handler(CustomerAddCommand command)
        {
            //if (command == null)
            //    return new CommandResult(StatusCodes.Status400BadRequest, "Parâmentros inválidos", "Parâmetros de entrada", "Parâmetros de entrada estão nulos");

            //if (!command.ValidarCommand())
            //    return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

            //var empresa = EmpresaHelper.GerarEntidade(command);

            //if (empresa.Invalido)
            //    return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", empresa.Notificacoes);

            //var id = _repository.Salvar(empresa);
            //empresa.DefinirId(id);
            //var dadosRetorno = EmpresaHelper.GerarDadosRetornoInsert(empresa);
            //return new CommandResult(StatusCodes.Status201Created, "Empresa gravada com sucesso!", dadosRetorno);

            return null;
        }

        public async Task<CommandResult> Handler(CustomerUpdateCommand command)
        {
            //if (command == null)
            //    return new CommandResult(StatusCodes.Status400BadRequest, "Parâmetros de entrada", "Parâmetros de entrada", "Parâmetros de entrada estão nulos");

            //command.Id = id;

            //if (!command.ValidarCommand())
            //    return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

            //var empresa = EmpresaHelper.GerarEntidade(command);

            //if (empresa.Invalido)
            //    return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", empresa.Notificacoes);

            //if (!_repository.CheckId(empresa.Id))
            //    return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", "Id", "Id inválido. Este id não está cadastrado!");

            //_repository.Atualizar(empresa);
            //var dadosRetorno = EmpresaHelper.GerarDadosRetornoUpdate(empresa);
            //return new CommandResult(StatusCodes.Status200OK, "Empresa atualizada com sucesso!", dadosRetorno);

            return null;
        }

        public async Task<CommandResult> Handler(CustomerDeleteCommand command)
        {
            if (!await _repository.CheckId(command.Id))
                return new CommandResult("Inconsistencies in the data", "Id", "Invalid id. This id is not registered!");

            await _repository.Delete(command.Id);

            return new CommandResult("Customer successfully deleted!", "");
        }
    }
}