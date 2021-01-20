using System.Linq;
using aera_core.Domain;
using aera_core.Helpers;
using aera_core.POUIHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aera_core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessoresController : ControllerBase
    {
        private readonly ProfessoresServiço _professoresServiço;

        public ProfessoresController(ProfessoresServiço professoresServiço)
        {
            _professoresServiço = professoresServiço;
        }

        [HttpGet]
        public POUIListResponse<ClienteDTO> Get([FromQuery] int page, [FromQuery] int pageSize)
        {
            var opções = new OpçõesBusca
            {
                Página = page < 1 ? 1 : page,
                LimitePágina = pageSize == 0 ? 10 : pageSize,
            };
            var clientes = _professoresServiço.ObterTodos(opções);
            var clientesDTO = clientes.Select(cliente => new ClienteDTO
            {
                id = cliente.Id,
                nome = cliente.Nome,
                address1 =  cliente.address1,
                address2 =  cliente.address2,
                address3 =  cliente.address3,
                celular = cliente.Celular,
                cep = cliente.CEP,
                cidade = cliente.Cidade,
                cpf = cliente.CPF,
                data_nascimento = cliente.DataNascimento?.ToString("yyyy-MM-dd"),
                profissao = cliente.Profissão,
                professor = cliente.ÉProfessor,
                email = cliente.Email,
                estado = cliente.Estado,
                hora_nascimento = cliente.HorárioNascimento?.ToString(@"hh\:mm"),
                local_nascimento = cliente.LocalNascimento,
                nivel_educacao = cliente.NívelEducação,
                observacao = cliente.Observação,
                telefone = cliente.Telefone,
                telefone_comercial = cliente.TelefoneComercial

            }).ToArray();

            return new POUIListResponse<ClienteDTO>(clientesDTO, clientes.TemMaisItens);
        }
        
        [HttpGet("{id}")]
        public ClienteDTO Get(int id)
        {
            var professor = _professoresServiço.Obter(id);
            return new ClienteDTO
            {
                id = professor.Id,
                nome = professor.Nome
            };
        }
    }
}
