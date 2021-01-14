using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aera_core.Domain;
using aera_core.Helpers;
using aera_core.Persistencia;
using aera_core.POUIHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aera_core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {

        private readonly ILogger<ClientesController> _logger;
        private readonly ClientesServiço _clientesServiço;

        public ClientesController(ILogger<ClientesController> logger, ClientesServiço clientesServiço)
        {
            _logger = logger;
            _clientesServiço = clientesServiço;
        }

        [HttpGet("{id}")]
        public ClienteDTO Get(int id)
        {
            var cliente = _clientesServiço.Obter(id);
            return new ClienteDTO
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
                data_nascimento = cliente.DataNascimento,
                profissao = cliente.Profissão,
                professor = cliente.ÉProfessor,
                email = cliente.Email,
                estado = cliente.Estado,
                hora_nascimento = cliente.HorárioNascimento,
                local_ascimento = cliente.LocalNascimento,
                nivel_educacao = cliente.NívelEducação,
                observacao = cliente.Observação,
                telefone = cliente.Telefone,
                telefone_comercial = cliente.TelefoneComercial,
                turmas = cliente.Turmas.Select(t => new TurmaDTO
                {
                    Curso = t.Curso.name,
                    DataInicial = t.start_date,
                    DataFinal = t.end_date
                }).ToList()
            };
        }

        [HttpGet]
        public POUIListResponse<ClienteDTO> Get([FromQuery] int page, [FromQuery] int pageSize)
        {
            var opções = new OpçõesBusca
            {
                Página = page,
                LimitePágina = pageSize,
            };
            var clientes = _clientesServiço.ObterClientes(opções);
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
                data_nascimento = cliente.DataNascimento,
                profissao = cliente.Profissão,
                professor = cliente.ÉProfessor,
                email = cliente.Email,
                estado = cliente.Estado,
                hora_nascimento = cliente.HorárioNascimento,
                local_ascimento = cliente.LocalNascimento,
                nivel_educacao = cliente.NívelEducação,
                observacao = cliente.Observação,
                telefone = cliente.Telefone,
                telefone_comercial = cliente.TelefoneComercial

            }).ToArray();

            return new POUIListResponse<ClienteDTO>(clientesDTO, clientes.TemMaisItens);
        }
    }
}
